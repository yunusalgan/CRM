using CRM.Data.EF.Helpers;
using CRM.Data.Interfaces;
using CRM.Service.Enums;
using CRM.Service.Imp.Helpers;
using CRM.Service.Models;
using CRM.Service.Models.Inputs.ApplicationErrors;
using CRM.Service.Models.ReturnModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;

namespace CRM.Data.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IConfiguration _config;
        private readonly ILogger _logger;
        public ISampleRepository Sample { get; }

        public UnitOfWork(
            IConfiguration config,
            ILogger<UnitOfWork> logger,

            ISampleRepository sampleRepository
            )
        {
            _config = config;
            _logger = logger;

            Sample = sampleRepository;
        }


        public T Run<T>(Func<T, T> action, TimeSpan? transactionTimeout = null, IsolationLevel? isolationLevel = null) where T : Result
        {
            var transactionOptions = new TransactionOptions();

            if (transactionTimeout.HasValue)
            {
                transactionOptions.Timeout = transactionTimeout.Value;
            }

            if (isolationLevel.HasValue)
            {
                transactionOptions.IsolationLevel = isolationLevel.Value;
            }

            var result = default(T);

            Exception exception = null;

            using (var transactionScope = transactionTimeout.HasValue ? new TransactionScope(TransactionScopeOption.Required, transactionOptions) : new TransactionScope())
            {
                try
                {
                    result = ObjectHelper.GetActivator<T>(typeof(T).GetConstructors().First())();

                    result = action(result);

                    if (result.StatusCode == (int)ResultStatuses.Success)
                    {
                        transactionScope.Complete();
                    }
                }
                catch (Exception e)
                {
                    exception = e;

                    result.StatusCode = (int)ResultStatuses.Error;
                    result.Message = MLValueHelper.GetMultilingualValue("FAILED", CultureInfo.CurrentCulture.Name);
                }
            }

            if (result.StatusCode != 1 && exception != null)
            {
                using (var transactionScope2 = transactionTimeout.HasValue ? new TransactionScope(TransactionScopeOption.Required, transactionOptions) : new TransactionScope())
                {
                    var message = string.Empty;
                    var stackTrace = string.Empty;

                    var oldEx = exception;

                    while (exception != null)
                    {
                        message += exception.Message + " | ";
                        stackTrace += exception.StackTrace + " | ";
                        exception = exception.InnerException;
                    }

                    var insertErrorInput = new InsertErrorInput { ErrorMessage = message, ErrorStack = stackTrace };

                    var appName = ApplicationOptions.ApplicationName;


                    // DB loglamak istenirse buraya yazılacak
                    //var applicationErrorEntitiy = new ApplicationErrors
                    //{
                    //    ApplicationName = appName,
                    //    ErrorDateUtc = DateTime.UtcNow,
                    //    ErrorMessage = insertErrorInput.ErrorMessage,
                    //    ErrorStack = insertErrorInput.ErrorStack
                    //};

                    try
                    {
                        //if (ApplicationErrors.Insert(applicationErrorEntitiy) == 0)
                        //{
                        try
                        {
                            _logger.LogError(oldEx, appName + ": " + message);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                                string filePath = fileName;

                                using (StreamWriter writer = new StreamWriter(filePath, true))
                                {
                                    writer.WriteLine("Message :" + message + "<br/>" + Environment.NewLine + "StackTrace :" + stackTrace +
                                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        //}
                        //else
                        //{
                        //    transactionScope2.Complete();
                        //}
                    }
                    catch (Exception)
                    {
                        try
                        {
                            _logger.LogError(oldEx, message);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                                string filePath = fileName;

                                using (StreamWriter writer = new StreamWriter(filePath, true))
                                {
                                    writer.WriteLine("Message :" + message + "<br/>" + Environment.NewLine + "StackTrace :" + stackTrace +
                                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            return result;
        }

    }
}

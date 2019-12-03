using CRM.Service.Models.ReturnModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;


namespace CRM.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ISampleRepository Sample { get; }


        T Run<T>(Func<T, T> action, TimeSpan? transactionTimeout = null, IsolationLevel? isolationLevel = null) where T : Result;
    }
}

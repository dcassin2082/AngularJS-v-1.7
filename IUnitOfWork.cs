using CustomerCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerCrud.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }

        void Commit();
    }
}
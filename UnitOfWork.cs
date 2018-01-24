using CustomerCrud.Models;

namespace CustomerCrud.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private CustomersEntities dbContext;
        private IRepository<Customer> customerRepo;

        public UnitOfWork(CustomersEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<Customer> CustomerRepository => customerRepo ?? (customerRepo = new Repository<Customer>(dbContext));

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
using Domain.StoreContext.Entities;
using Domain.StoreContext.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
        bool CheckDocument(string document);
        bool CheckEmail(string email);
        bool CheckCustomer(Guid id);
        void Save(Customer customer);
        void Delete(Guid id);
        Task<CustomerOrdersCountResult> GetCustomerOrdersCount(string document);
        Task<IEnumerable<ListCustomerQueryResult>> Get();
        Task<GetCustomerQueryResult> Get(Guid id);
        Task<IEnumerable<ListCustomerOrdersQueryResult>> GetOrders(Guid id);
        void EditCustomer(Guid id, string document, string firstName, string lastName, string email);
    }
}

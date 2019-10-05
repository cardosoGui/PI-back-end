using Domain.StoreContext.Entities;
using Domain.StoreContext.Queries.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.StoreContext.Repositories
{
    public interface IProductRepository
    {

        Task<IEnumerable<ListProductQueryResult>> Get();
        Task<IEnumerable<GetProductQueryResult>> Get(Guid id);
        void Save(Product product);
        void Delete(Guid id);
        void DeleteById(Guid id);
        void EditProduct(Guid id, string title, string description, string image, decimal price, int quantityOnHand);
        bool CheckProduct(Guid id);

        //Products
        //Task<IEnumerable<ListCustomerQueryResult>> Get();

        //Products
        //Task<GetCustomerQueryResult> Get(Guid id);

    }
}

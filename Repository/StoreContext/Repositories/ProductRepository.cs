using Dapper;
using Domain.StoreContext.Entities;
using Domain.StoreContext.Queries.Product;
using Domain.StoreContext.Repositories;
using Repository.DataContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository.StoreContext.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckProduct(Guid id) =>
        _context.Connection.QueryFirstOrDefault<bool>(
                "spCheckProduct",
                new { Id = id },
                 commandType: CommandType.StoredProcedure);

        public void Delete(Guid id) =>
        _context.Connection.Execute(
                "spDeleteProduct",
                new
                {
                    Id = id
                },
                 commandType: CommandType.StoredProcedure);

        public void EditProduct(Product product) =>
            _context.Connection.Execute(
               "spEditProduct",
               new
               {
                   Id = product.Id,
                   Title = product.Title,
                   Description = product.Description,
                   Image = product.Image,
                   Price = product.Price,
                   QuantityOnHand = product.QuantityOnHand,
                   AlterationDate = DateTime.Now

               },
                commandType: CommandType.StoredProcedure);

        public async Task<IEnumerable<ListProductQueryResult>> Get() =>
        await _context.Connection.QueryAsync<ListProductQueryResult>("spListProduct", commandType: CommandType.StoredProcedure);

        public async Task<IEnumerable<GetProductQueryResult>> Get(Guid id) =>
            await _context.Connection.QueryAsync<GetProductQueryResult>("spListProductId",
                new { Id = id }, commandType: CommandType.StoredProcedure);


        public void Save(Product product) =>
            _context.Connection.Execute(
               "spCreateProduct",
               new
               {
                   product.Id,
                   product.Title,
                   product.Description,
                   product.Image,
                   product.Price,
                   product.QuantityOnHand,
                   RegisterDate = DateTime.Now,
                   AlterationDate = DateTime.Now

               },
                commandType: CommandType.StoredProcedure);
    }
}

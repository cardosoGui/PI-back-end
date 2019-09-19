using Dapper;
using Domain.StoreContext.Entities;
using Domain.StoreContext.Queries;
using Domain.StoreContext.Repositories;
using Repository.DataContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }
        public bool CheckDocument(string document) =>
        _context.Connection.QueryFirstOrDefault<bool>(
                    "spCheckDocument",
                    new { Document = document },
                    commandType: CommandType.StoredProcedure);

        public bool CheckEmail(string email) =>
        _context.Connection.QueryFirstOrDefault<bool>(
                "spCheckEmail",
                new { Email = email },
                 commandType: CommandType.StoredProcedure);

        public async Task<IEnumerable<ListCustomerQueryResult>> Get() =>
        await _context.Connection.QueryAsync<ListCustomerQueryResult>("spListCustomer", commandType: CommandType.StoredProcedure);

        public async Task<CustomerOrdersCountResult> GetCustomerOrdersCount(string document) => await
        _context.Connection.QueryFirstOrDefaultAsync<CustomerOrdersCountResult>(
                "spGetCustomerOrdersCount",
                new { Document = document },
                 commandType: CommandType.StoredProcedure);

        public async Task<GetCustomerQueryResult> Get(Guid id) =>
            await _context.Connection.QueryFirstOrDefaultAsync<GetCustomerQueryResult>(
                "spListCustomerId",
                new { Id = id },
                 commandType: CommandType.StoredProcedure);

        //TODO : Fazer Procedure para listar os pedidos do Cliente.
        public async Task<IEnumerable<ListCustomerOrdersQueryResult>> GetOrders(Guid id) => await
            _context.Connection.QueryAsync<ListCustomerOrdersQueryResult>("spCustomerGetOrders", commandType: CommandType.StoredProcedure);

        public void Save(Customer customer)
        {
            _context.Connection.Execute(
               "spCreateCustomer",
               new
               {
                   customer.Id,
                   customer.Name.FirstName,
                   customer.Name.LastName,
                   Document = customer.Document.ToString(),
                   Email = customer.Email.Address,
                   customer.Phone,
                   RegisterDate = DateTime.Now,
                   AlterationDate = DateTime.Now

               },
                commandType: CommandType.StoredProcedure);

            foreach (var address in customer.Addresses)
            {
                _context.Connection.Execute("spCreateAddress", new
                {
                    address.Id,
                    CustomerId = customer.Id,
                    address.Number,
                    address.Complement,
                    address.District,
                    address.City,
                    address.State,
                    address.Country,
                    address.ZipCode,
                    RegisterDate = DateTime.Now,
                    address.Type,
                }, commandType: CommandType.StoredProcedure);
            }

        }

        public void EditCustomer(Customer customer) =>
            _context.Connection.Execute(
               "spEditCustomer",
               new
               {
                   Id = customer.Id,
                   Document = customer.Document,
                   FirstName = customer.Name.FirstName,
                   LastName = customer.Name.LastName,
                   Email = customer.Email.Address,
                   AlterationDate = DateTime.Now
               },
                commandType: CommandType.StoredProcedure);

        public void Delete(Guid id) => _context.Connection.Execute(
                "spDeleteCustomer",
                new { Id = id },
                 commandType: CommandType.StoredProcedure);

        public bool CheckCustomer(Guid id) =>
            _context.Connection.QueryFirstOrDefault<bool>(
                "spCheckCustomer",
                new { Id = id },
                 commandType: CommandType.StoredProcedure);

    }
}


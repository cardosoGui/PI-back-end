using Domain.StoreContext.Commands.ProductsCommands.Inputs;
using Domain.StoreContext.Handlers;
using Domain.StoreContext.Queries.Product;
using Domain.StoreContext.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Wine.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductHandler handler;
        private readonly IProductRepository repository;
        public ProductController(ProductHandler handler, IProductRepository repository)
        {
            this.handler = handler;
            this.repository = repository;
        }

        [HttpPost]
        [Route("products")]
        public ICommandResult Post([FromBody]CreateProductCommand command) => handler.Handle(command);

        [HttpPut]
        [Route("products")]
        public ICommandResult Put([FromBody]EditProductCommand command) => handler.Handle(command);

        [HttpDelete]
        [Route("products")]
        public ICommandResult Delete([FromBody]DeleteProductCommand command) => handler.Handle(command);

        [HttpGet]
        [Route("products")]
        public async Task<IEnumerable<ListProductQueryResult>> Get() => await repository.Get();

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IEnumerable<GetProductQueryResult>> GetById(Guid id) => await repository.Get(id);
    }
}

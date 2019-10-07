namespace E_Wine.Controllers
{
    using Domain.StoreContext.Commands.CustommerCommands.Inputs;
    using Domain.StoreContext.Handlers;
    using Domain.StoreContext.Queries;
    using Domain.StoreContext.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Commands;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace GuilhermeStore.Api.Controllers
    {
        public class CustomerController : Controller
        {

            //Para versionamento de api's, pode-se colocar v1, v2, v3 etc nas rotas.
            private readonly ICustomerRepository repository;
            private readonly CustomerHandler handler;

            public CustomerController(ICustomerRepository repository, CustomerHandler handler)
            {
                this.repository = repository;
                this.handler = handler;
            }

            // [ResponseCache(Duration = 60)] Cache, se os dados forem muito mutáveis não compensa.
            // Location =  ResponseCacheLocation.Client
            [Authorize("Bearer")]
            [HttpGet]
            [Route("customers")]
            public async Task<IEnumerable<ListCustomerQueryResult>> Get() => await repository.Get();

            [Authorize("Bearer")]
            [HttpGet]
            [Route("customers/{id}")]
            public async Task<GetCustomerQueryResult> GetById(Guid id) => await repository.Get(id);

            [Authorize("Bearer")]
            [HttpGet]
            [Route("customers/{id}/orders")]
            public async Task<IEnumerable<ListCustomerOrdersQueryResult>> GetOrders(Guid id) => await repository.GetOrders(id);

            [Authorize("Bearer")]
            [HttpPost]
            [Route("customers")]
            public ICommandResult Post([FromBody]CreateCustomerCommand command) => handler.Handle(command);

            //customers/{id} - Padrão
            [Authorize("Bearer")]
            [HttpPut]
            [Route("customers")]
            public ICommandResult Put([FromBody]EditCustomerCommand command) => handler.Handle(command);

            //customers/{id} - Padrão
            [Authorize("Bearer")]
            [HttpDelete]
            [Route("customers")]
            public ICommandResult Delete([FromBody]DeleteCustomerCommand command) => handler.Handle(command);

        }
    }
}

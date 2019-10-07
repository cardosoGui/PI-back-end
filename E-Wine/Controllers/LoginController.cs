using Domain.StoreContext.Commands.LoginCommands;
using Domain.StoreContext.Handlers;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;

namespace E_Wine.Controllers
{

    public class LoginController : Controller
    {
        private readonly LoginHandler handler;
        public LoginController(LoginHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        [Route("login")]
        public ICommandResult Post([FromBody]LoginCommand command) => handler.Handle(command);

    }
}

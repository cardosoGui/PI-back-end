using Domain.StoreContext.Commands.CustommerCommands.Outputs;
using Domain.StoreContext.Commands.LoginCommands;
using Domain.StoreContext.Repositories;
using Flunt.Notifications;
using Microsoft.IdentityModel.Tokens;
using Shared.Commands;
using Shared.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Domain.StoreContext.Handlers
{
    public class LoginHandler : Notifiable, ICommandHanler<LoginCommand>
    {
        private readonly ILoginRepository loginRepository;
        private readonly TokenConfigurations tokenConfigurations;
        private readonly SigningConfigurations signingConfigurations;
        public LoginHandler(ILoginRepository loginRepository, TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            this.loginRepository = loginRepository;
            this.tokenConfigurations = tokenConfigurations;
            this.signingConfigurations = signingConfigurations;
        }
        public ICommandResult Handle(LoginCommand command)
        {
            AddNotifications(command.Notifications);

            if (!loginRepository.CheckUser(command.Document.Number, command.Email.Address))
                AddNotification("Usuário", "Usuário não cadastrado !");

            if (Valid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(command.Email.Address, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, command.Email.Address)
                }
            );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new CommandResult(true, "Login realizado com sucesso", new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                });
            }
            else
            {
                return new CommandResult(false, "Falha ao autenticar !");
            }
        }
    }
}

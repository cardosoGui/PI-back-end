using Domain.StoreContext.ValueObjects;
using Flunt.Notifications;
using Shared.Commands;

namespace Domain.StoreContext.Commands.LoginCommands
{
    public class LoginCommand : Notifiable, ICommand
    {
        public Document Document { get; set; }
        public Email Email { get; set; }

        public bool Validate()
        {
            if (!Document.Validate(Document.Number))
                AddNotification("Document", "Cpf inválido");

            return Valid;
        }
    }
}

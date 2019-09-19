using Flunt.Notifications;
using Flunt.Validations;
using Shared.Commands;
using System;

namespace Domain.StoreContext.Commands.CustommerCommands.Inputs
{
    public class DeleteCustomerCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract().Requires()
                .AreEquals(Id, null, "Id", "O Id do usuário não pode ser núlo."));
            return Valid;
        }

    }
}

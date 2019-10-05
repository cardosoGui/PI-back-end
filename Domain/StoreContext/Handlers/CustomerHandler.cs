using Domain.StoreContext.Commands.CustommerCommands.Inputs;
using Domain.StoreContext.Commands.CustommerCommands.Outputs;
using Domain.StoreContext.Entities;
using Domain.StoreContext.Repositories;
using Domain.StoreContext.ValueObjects;
using Flunt.Notifications;
using Shared.Commands;
using System.Transactions;

namespace Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable, ICommandHanler<CreateCustomerCommand>, ICommandHanler<AddAddressCommand>, ICommandHanler<EditCustomerCommand>, ICommandHanler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository customerRepository;
        //private readonly IEmailService _emailService;
        public CustomerHandler(ICustomerRepository customerRepository/*, IEmailService emailService*/)
        {
            this.customerRepository = customerRepository;
            //_emailService = emailService;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            using (var tx = new TransactionScope(TransactionScopeOption.Required))
            {
                //Verificar se o CPF já existe na base
                if (customerRepository.CheckDocument(command.Document))
                    AddNotification("Document", "Este CPF já está em uso");

                //Verificar se o E-mail existe na base
                if (customerRepository.CheckEmail(command.Email))
                    AddNotification("Document", "Este E-mail já está em uso");

                //Criar os VOs
                var name = new Name(command.FirstName, command.LastName);
                var document = new Document(command.Document);
                var email = new Email(command.Email);

                //Criar Entidade
                var customer = new Customer(name, document, email, command.Phone);

                //Validar Entidades e VOs
                AddNotifications(command.Notifications);
                AddNotifications(name.Notifications);
                AddNotifications(document.Notifications);
                //AddNotifications(email.Notifications);
                AddNotifications(customer.Notifications);

                if (Invalid) return new CommandResult(false, "Por favor, corrija os campos abaixo.", Notifications);

                //Persistir o cliente 
                customerRepository.Save(customer);

                // //Enviar um E-mail de boas vindas
                // _emailService.Send(email.Address, "guilherme.mendes@interlayers.com.br", "Bem Vindo", "Seja Bem vindo ao Guilherme Store!");

                //Retornar o resultado para tela
                var result = new CommandResult(true, "Bem-Vindo a E-Wine", new { Id = customer.Id, Name = name.ToString(), Email = email.Address });

                tx.Complete();
                return result;
            }
        }


        public ICommandResult Handle(EditCustomerCommand command)
        {

            if (!customerRepository.CheckCustomer(command.Id))
                AddNotification("Cliente", "O Cliente não existe.");

            //Criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            //Criar Entidade
            var customer = new Customer(name, document, email, command.Phone);

            //Validar Entidades e VOs
            AddNotifications(command.Notifications);
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            //AddNotifications(email.Notifications);

            if (Invalid) return new CommandResult(false, "Não foi possível editar os dados", Notifications);

            //edita dados do usuário.
            customerRepository.EditCustomer(command.Id, command.Document, command.FirstName, command.LastName, command.Email);

            // //Enviar um E-mail avisando alteração.
            // _emailService.Send(email.Address, "guilherme.mendes@interlayers.com.br", "Alteração de dados", "Seus dados foram Atualizados.");

            return new CommandResult(true, "Informações Editadas com Sucesso !");
        }


        public ICommandResult Handle(DeleteCustomerCommand command)
        {
            //Verificar se o CPF já existe na base
            if (!customerRepository.CheckCustomer(command.Id))
                AddNotification("Cliente", "O Cliente não existe.");

            AddNotifications(command.Notifications);

            if (Invalid) return new CommandResult(false, "Não foi posível realizar a exclusão.", Notifications);

            customerRepository.Delete(command.Id);

            return new CommandResult(true, "Exclusão realizada com sucesso !");
        }

        public ICommandResult Handle(AddAddressCommand command)
        {
            throw new System.NotImplementedException();
        }


    }
}

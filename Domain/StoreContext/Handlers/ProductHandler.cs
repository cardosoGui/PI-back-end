using Domain.StoreContext.Commands.CustommerCommands.Outputs;
using Domain.StoreContext.Commands.ProductsCommands.Inputs;
using Domain.StoreContext.Entities;
using Domain.StoreContext.Repositories;
using Flunt.Notifications;
using Shared.Commands;

namespace Domain.StoreContext.Handlers
{
    public class ProductHandler : Notifiable, ICommandHanler<CreateProductCommand>, ICommandHanler<EditProductCommand>, ICommandHanler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ICommandResult Handle(CreateProductCommand command)
        {
            var product = new Product(command.Title, command.Description, command.Image, command.Price, command.QuantityOnHand);

            AddNotifications(product.Notifications);

            if (Invalid) return new CommandResult(false, "Não foi possível salvar os dados", Notifications);

            productRepository.Save(product);

            return new CommandResult(true, "Produto Cadastrado com Sucesso !");

        }

        public ICommandResult Handle(EditProductCommand command)
        {
            if (!productRepository.CheckProduct(command.Id))
                AddNotification("Produto", "O Produto não existe.");

            //Criar Entidade
            var product = new Product(command.Title, command.Description, command.Image, command.Price, command.QuantityOnHand);

            //Validar Entidades e VOs
            AddNotifications(product.Notifications);
            //AddNotifications(email.Notifications);

            if (Invalid) return new CommandResult(false, "Não foi possível editar os dados", Notifications);

            //edita dados do usuário.
            productRepository.EditProduct(product);

            // //Enviar um E-mail avisando alteração.
            // _emailService.Send(email.Address, "guilherme.mendes@interlayers.com.br", "Alteração de dados", "Seus dados foram Atualizados.");

            return new CommandResult(true, "Informações Editadas com Sucesso !");
        }

        public ICommandResult Handle(DeleteProductCommand command)
        {
            if (!productRepository.CheckProduct(command.Id))
                AddNotification("Produto", "O Produto não existe.");

            if (Invalid) return new CommandResult(false, "Não foi possível excluir os dados", Notifications);

            productRepository.Delete(command.Id);

            return new CommandResult(true, "Exclusão Realizada com Sucesso !");
        }
    }
}

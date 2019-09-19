using Flunt.Notifications;
using Flunt.Validations;
using Shared.Commands;

namespace Domain.StoreContext.Commands.ProductsCommands.Inputs
{
    public class CreateProductCommand : Notifiable, ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int QuantityOnHand { get; set; }
        public bool Validate()
        {
            AddNotifications(new Contract().Requires()
                .HasMinLen(Title, 3, "Title", "O titulo deve ter no mínimo 3 caracteres.")
                .IsNotNullOrEmpty(Title, "Title", "O produto deve conter um titulo.")
                .IsNotNullOrEmpty(Description, "Description", "O produto deve conter uma descrição.")
                .IsNotNullOrEmpty(Image, "Image", "O produto deve conter uma imagem.")
                .IsGreaterThan(Price, 0, "Price", "O preço deve ser maior que 0.")
                .IsGreaterThan(QuantityOnHand, 0, "QuantityOnHand", "A quantidade do produto em mãos deve ser maior que 0."));
            return Valid;
        }
    }
}

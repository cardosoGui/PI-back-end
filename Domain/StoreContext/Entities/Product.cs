using Flunt.Validations;
using Shared.Entities;
using System;

namespace Domain.StoreContext.Entities
{
    public class Product : Entity
    {
        public Product(string title, string description, string image, decimal price, int quantity)
        {
            Title = title;
            Description = description;
            Image = image;
            Price = price;
            QuantityOnHand = quantity;

            AddNotifications(new Contract().Requires()
                .HasMinLen(Title, 3, "Title", "O titulo deve ter no mínimo 3 caracteres.")
                .IsNotNullOrEmpty(Title, "Title", "O produto deve conter um titulo.")
                .IsNotNullOrEmpty(Description, "Description", "O produto deve conter uma descrição.")
                .IsNotNullOrEmpty(Image, "Image", "O produto deve conter uma imagem.")
                .IsGreaterThan(Price, 0, "Price", "O preço deve ser maior que 0.")
                .IsGreaterThan(QuantityOnHand, 0, "QuantityOnHand", "A quantidade do produto em mãos deve ser maior que 0."));
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityOnHand { get; private set; }

        public override string ToString()
        {
            return $"{Title}";
        }

        public void DecreaseQuantity(int quantity)
        {
            QuantityOnHand -= quantity;
        }
    }
}

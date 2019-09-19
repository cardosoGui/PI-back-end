using System;

namespace Domain.StoreContext.Queries.Product
{
    public class ListProductQueryResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int QuantityOnHand { get; set; }
    }
}

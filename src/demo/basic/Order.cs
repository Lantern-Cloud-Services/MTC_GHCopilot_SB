using System;

namespace Company.Function
{
    public class Order
    {
        public string OrderID { get; set; }
        public int Quantity { get; set; }
        public string ProductID { get; set; }

        public Order(string orderID, int quantity, string productID)
        {
            OrderID = orderID;
            Quantity = quantity;
            ProductID = productID;
        }
    }
}

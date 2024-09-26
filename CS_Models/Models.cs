using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? OrderName { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

        public int Advance { get; set; }

    }

    public class Items
    {
        public string? ItemName { get; set; }
        public int UnitPrice { get; set; }
    }

    public class ItemsDb : List<Items>
    {
        public ItemsDb()
        {
            Add(new Items() { ItemName = "Laptop", UnitPrice = 120000 });
            Add(new Items() { ItemName = "Dektop", UnitPrice = 60000 });
            Add(new Items() { ItemName = "RAM", UnitPrice = 8000 });
            Add(new Items() { ItemName = "Monitor", UnitPrice = 6450 });
            Add(new Items() { ItemName = "SSD", UnitPrice = 20000 });
            Add(new Items() { ItemName = "HDD", UnitPrice = 8610 });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp_Sem2_Project
{
    public enum Category { Weapons, Magazines, Accessories, Rigs, Clothing }
    public enum Manufacturer { Tokyo_Marui, Cybergun, Ares, Vortex, Crye, TMC }

    public class Item
    {
        #region Properties

        public int ItemID { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Info { get; set; }
        public string Image { get; set; }

        #endregion Properties

        #region Constructors
        public Item(int itemID, string product, string manufacturer, string category, decimal price, string info, string image)
        {
            ItemID = itemID;
            Manufacturer = manufacturer;
            Category = category;
            ProductName = product;
            Price = price;
            Info = info;
            Image = image;
        }
        #endregion Constructors

        public override string ToString()
        {
            return ProductName;
        }
    }

    public class cartItem
    {
        #region Properties
        public string ProductAdded { get; set; }
        #endregion Properties

        #region Constructors
        public cartItem(string productAdded)
        {
            ProductAdded = productAdded;
        }
        #endregion Constructors
    }
    public class qtyAndPrice
    {
        #region Properties
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        #endregion Properties

        #region Constructors
        public qtyAndPrice(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = Price;
        }
        #endregion Constructors
    }
}

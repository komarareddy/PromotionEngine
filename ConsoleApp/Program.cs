using CLPromotionEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //item price List
          IEnumerable<SKU_Price> PriceList =  new List<SKU_Price> {
                new SKU_Price { SKU_Id = 'A', UnitPrice = 50 },
                new SKU_Price { SKU_Id = 'B', UnitPrice = 30 },
                new SKU_Price { SKU_Id = 'C', UnitPrice = 20 },
                new SKU_Price { SKU_Id = 'D', UnitPrice = 15 } };

            //Promotions
            IEnumerable<Promotion> Promotions = new List<Promotion> {
          new Promotion {
          Items = new List<Item> {
            new Item { SKU_Id = 'A', Quantity = 3 }}, TotalAmount = 130 }, 
        new Promotion {
          Items = new List<Item> {
            new Item { SKU_Id = 'B', Quantity = 2 }},TotalAmount = 45 }, 
        new Promotion {
          Items = new List<Item> {
            new Item { SKU_Id = 'C', Quantity = 1 }, new Item { SKU_Id = 'D', Quantity = 1 }},TotalAmount = 30 } }; 

            //assign PriceList and Promotions
            PromotionEngine actualEngine = new PromotionEngine(PriceList, Promotions);

            //OrderDetail list
            var order = new OrderDetail{
            Items = new List<Item>{
            new Item { SKU_Id = 'A', Quantity = 1 },
            new Item { SKU_Id = 'B', Quantity = 1 },
            new Item { SKU_Id = 'C', Quantity = 1 }} };


            var promotionOrder =
       new OrderDetail
       {
           Items = new List<Item>
         {
            new Item { SKU_Id = 'A', Quantity = 5 },
            new Item { SKU_Id = 'B', Quantity = 5 },
            new Item { SKU_Id = 'C', Quantity = 1 }}
       };

            actualEngine.CheckOut(promotionOrder);
           var totalAmount = promotionOrder.TotalAmount;
            Console.WriteLine($"Your total Price after discount:- {totalAmount}");
            Console.Read();
        }

        
    }
}

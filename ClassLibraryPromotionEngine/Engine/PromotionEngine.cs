using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLPromotionEngine
{
  public class PromotionEngine
  {
        //get pricelist and promotion from client
    private IEnumerable<SKU_Price> PriceList;
    private IEnumerable<Promotion> Promotions;

    public PromotionEngine(IEnumerable<SKU_Price> priceList, IEnumerable<Promotion> promotions)
    {
      this.PriceList = priceList;
      this.Promotions = promotions;
    }

        //check out function
    public void CheckOut(OrderDetail order)
    {
      var foundItems = new List<Item>();
      
            //if promotion found then apply
      if (Promotions != null && Promotions.Count() > 0)
        foreach (var promotion in Promotions)
        {
          //validate order and check if it comes for promotion and  assign promotion
          var validatedItems = promotion.Validate(order, foundItems);
          UpdateValidatedItems(foundItems, validatedItems);
        }

      ApplyPriceWithoutPromotions(order, foundItems);
    }
  
        //if no promtion just apply normal price
    private void ApplyPriceWithoutPromotions(OrderDetail order, List<Item> foundItems)
    {
      foreach (var item in order.Items)
      {
        var validateItem = foundItems.FirstOrDefault(x => x.SKU_Id == item.SKU_Id) ?? item;
        var quantity = validateItem.Quantity;
        if (quantity > 0)
          order.TotalAmount += quantity * PriceList.First(x => x.SKU_Id == item.SKU_Id).UnitPrice;
      }
    }

    private static void UpdateValidatedItems(List<Item> foundItems, IEnumerable<Item> validatedItems)
    {
      if (validatedItems == null || validatedItems.Count() < 1)
        return;

      foreach (var item in validatedItems)
        if (!foundItems.Any(x => x.SKU_Id == item.SKU_Id))
          foundItems.Add(item);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarStore.Data;
using CarStore.Models;

namespace CarStore.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly CarStore.Data.CarStoreContext _context;

        public ShoppingCartModel(CarStore.Data.CarStoreContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get; set; } = new List<Car>();

        public IList<string> ItemPlusQuantityList { get; set; } = new List<string>();
        public int[] QuantityArray { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Car != null && Request.Cookies["ShoppingCart"] != null)
            {
                // split cookie string into an array
                int[] cookiesList = Array.ConvertAll(Request.Cookies["ShoppingCart"].Split(","), int.Parse);

                // count the number of times an item in the car appears
                foreach (var item in cookiesList)
                {
                    // the number of times the specified ID appears
                    int itemCount = cookiesList.Count(s => s == item);
                    ItemPlusQuantityList.Add(item + "," + itemCount);
                }
                // remove duplicate values
                ItemPlusQuantityList = ItemPlusQuantityList.Distinct().ToList();
                QuantityArray = new int[ItemPlusQuantityList.Count];

                // create the car list 
                for (int i = 0; i < ItemPlusQuantityList.Count; i++)
                {
                    int[] delimitedItem = Array.ConvertAll(ItemPlusQuantityList[i].Split(','), int.Parse);

                    // Add to the list of cars
                    var car = _context.Car.Include("CarType").FirstOrDefault(m => m.CarID == delimitedItem[0]);
                    if (car != null)
                    {
                        Car.Add(car);
                    }
                    // Add to the list of quantities
                    QuantityArray[i] = delimitedItem[1];
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using INET2005_FinalProject.Data;
using INET2005_FinalProject.Models;

namespace INET2005_FinalProject.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;

        [BindProperty]
        public Car car { get; set; } = default!;
        public string Message { get; set; } = string.Empty;

        public DetailsModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context)
        {
            _context = context;
        }

      public Car Car { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.Include("CarType").FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }
            else 
            {
                Car = car;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // add product to cart
            string cart = Request.Cookies["ShoppingCart"];

            // Initialize cart
            if (string.IsNullOrEmpty(cart))
            {
                Response.Cookies.Append("ShoppingCart", car.CarID.ToString());
            }
            // Append comma with ID instead
            else
            {
                Response.Cookies.Append("ShoppingCart", cart + "," + car.CarID.ToString());
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarStore.Data;
using CarStore.Models;

namespace CarStore.Pages.CarTypePages
{
    public class DetailsModel : PageModel
    {
        private readonly CarStore.Data.CarStoreContext _context;

        public DetailsModel(CarStore.Data.CarStoreContext context)
        {
            _context = context;
        }

      public CarType CarType { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var cartype = await _context.CarType.FirstOrDefaultAsync(m => m.CarTypeID == id);
            if (cartype == null)
            {
                return NotFound();
            }
            else 
            {
                CarType = cartype;
            }
            return Page();
        }
    }
}

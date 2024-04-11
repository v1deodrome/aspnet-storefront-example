using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarStore.Data;
using CarStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarStore.Pages.CarTypePages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly CarStore.Data.CarStoreContext _context;

        public DeleteModel(CarStore.Data.CarStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }
            var cartype = await _context.CarType.FindAsync(id);

            if (cartype != null)
            {
                CarType = cartype;
                _context.CarType.Remove(CarType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./CarTypeList");
        }
    }
}

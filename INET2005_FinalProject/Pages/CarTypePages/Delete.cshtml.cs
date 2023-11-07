using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using INET2005_FinalProject.Data;
using INET2005_FinalProject.Models;

namespace INET2005_FinalProject.Pages.CarTypePages
{
    public class DeleteModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;

        public DeleteModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context)
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

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INET2005_FinalProject.Data;
using INET2005_FinalProject.Models;

namespace INET2005_FinalProject.Pages.CarTypePages
{
    public class EditModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;

        public EditModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context)
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

            var cartype =  await _context.CarType.FirstOrDefaultAsync(m => m.CarTypeID == id);
            if (cartype == null)
            {
                return NotFound();
            }
            CarType = cartype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CarType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(CarType.CarTypeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarTypeExists(int id)
        {
          return (_context.CarType?.Any(e => e.CarTypeID == id)).GetValueOrDefault();
        }
    }
}

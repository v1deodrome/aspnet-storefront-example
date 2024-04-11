using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarStore.Data;
using CarStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarStore.Pages.CarTypePages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CarStore.Data.CarStoreContext _context;

        public CreateModel(CarStore.Data.CarStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CarType CarType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CarType == null || CarType == null)
            {
                return Page();
            }

            _context.CarType.Add(CarType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./CarTypeList");
        }
    }
}

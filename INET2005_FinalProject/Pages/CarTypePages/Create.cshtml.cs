using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using INET2005_FinalProject.Data;
using INET2005_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace INET2005_FinalProject.Pages.CarTypePages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;

        public CreateModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context)
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

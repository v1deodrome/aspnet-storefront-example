using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using INET2005_FinalProject.Data;
using INET2005_FinalProject.Models;
using System.ComponentModel;

namespace INET2005_FinalProject.Pages.CarPages
{
    public class CreateModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;
        private readonly IWebHostEnvironment _env;

        // List of car types
        public List<SelectListItem> CarTypeList { get; set; } = new List<SelectListItem>();

        public CreateModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

            // Populating the type list 
            List<CarType> carTypes = _context.CarType.ToList();

            foreach (CarType carType in carTypes)
            {
                CarTypeList.Add(new SelectListItem()
                {
                    Value = carType.CarTypeID.ToString(),
                    Text = carType.TypeName
                });
            }
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Get and set TypeID
            int typeID = Car.CarType.CarTypeID;
            CarType carType = _context.CarType.Single(m => m.CarTypeID == typeID);
            Car.CarType = carType;

            // Validate
            if (!ModelState.IsValid || _context.Car == null || Car == null)
            {
                return Page();
            }

            _context.Car.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

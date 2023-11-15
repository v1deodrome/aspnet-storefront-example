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
using Microsoft.AspNetCore.Authorization;

namespace INET2005_FinalProject.Pages.CarPages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;
        private readonly IWebHostEnvironment _env;

        // Select list
        public List<SelectListItem> CarTypeList { get; set; } = new List<SelectListItem>();

        public EditModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context, IWebHostEnvironment env)
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
            _env = env;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        
        // [BindProperty]
        // public IFormFile ImageUpload { get; set; } = default!;

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
            
            Car = car;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // string imageFileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss_") + ImageUpload.FileName;
            // Car.ImageName = imageFileName;

            // Get and set TypeID
            int typeID = Car.CarType.CarTypeID;
            CarType carType = _context.CarType.Single(m => m.CarTypeID == typeID);
            Car.CarType = carType;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                //if (ImageUpload != null)
                //{
                //    // Save image to filesystem
                //    string filePath = Path.Combine(_env.ContentRootPath, "wwwroot/photos", imageFileName);
                //    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        ImageUpload.CopyTo(fileStream);
                //    }
                //}
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.CarID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./CarList");
        }

        private bool CarExists(int id)
        {
          return (_context.Car?.Any(e => e.CarID == id)).GetValueOrDefault();
        }
    }
}

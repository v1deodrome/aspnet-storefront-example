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
    public class IndexModel : PageModel
    {
        private readonly CarStore.Data.CarStoreContext _context;

        public IndexModel(CarStore.Data.CarStoreContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Car != null)
            {
                Car = await _context.Car.Include("CarType").OrderByDescending(a => a.Name).ToListAsync();
            }
        }
    }
}

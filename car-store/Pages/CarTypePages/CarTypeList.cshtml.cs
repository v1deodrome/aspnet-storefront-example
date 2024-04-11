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
    public class CarTypeListModel : PageModel
    {
        private readonly CarStore.Data.CarStoreContext _context;

        public CarTypeListModel(CarStore.Data.CarStoreContext context)
        {
            _context = context;
        }

        public IList<CarType> CarType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CarType != null)
            {
                CarType = await _context.CarType.ToListAsync();
            }
        }
    }
}

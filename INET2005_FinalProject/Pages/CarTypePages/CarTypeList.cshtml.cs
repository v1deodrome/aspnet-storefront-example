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
    public class CarTypeListModel : PageModel
    {
        private readonly INET2005_FinalProject.Data.INET2005_FinalProjectContext _context;

        public CarTypeListModel(INET2005_FinalProject.Data.INET2005_FinalProjectContext context)
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

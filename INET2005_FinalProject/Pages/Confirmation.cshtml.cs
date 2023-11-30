using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INET2005_FinalProject.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string ConfirmationString = string.Empty;

        public void OnGet(string confirmationID)
        {
            ConfirmationString = confirmationID;
        }
    }
}

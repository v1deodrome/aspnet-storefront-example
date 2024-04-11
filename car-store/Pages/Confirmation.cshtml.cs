using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarStore.Pages
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

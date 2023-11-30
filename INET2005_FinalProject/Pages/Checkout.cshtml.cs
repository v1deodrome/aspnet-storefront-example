using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using Flurl.Http;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Azure.Core;

namespace INET2005_FinalProject.Pages
{
    public class CheckoutModel : PageModel
    {
        public string CheckoutErrorString { get; set; } = string.Empty;
        
        [BindProperty]
        public CheckoutDetails Payload { get; set; }
        
        protected string result;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            // set cookies to products
            Payload.Products = Request.Cookies["ShoppingCart"];

            // Validate data before sending to the thing
            if (Math.Floor(Math.Log10(Payload.CcNumber) + 1) != 16)
            {
                CheckoutErrorString = "Card number must be 16 digits long.";
                return Page();
            }
            else if (Payload.CcExpiryDate.Length != 4)
            {
                CheckoutErrorString = "Expiry date must be in the format 'MMYY'";
                return Page();
            }
            else if (Math.Floor(Math.Log10(Payload.Cvv) + 1) != 3)
            {
                CheckoutErrorString = "CVV must be a 3 digit number.";
                return Page();
            }
            else if (IsAnyNullOrEmpty(Payload))
            {
                CheckoutErrorString = "One or more of your fields is empty.";
                return Page();
            }

            // Serialize as JSON data
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonString = JsonSerializer.Serialize(Payload, options);

            // Send to server :: credit to Ademar on StackOverflow:
            // https://stackoverflow.com/a/10027534
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://nscc-inet2005-purchase-api.azurewebsites.net/purchase");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            // Write to server
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = jsonString;
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            // Reset checkout error string.
            CheckoutErrorString = string.Empty;
            Response.Cookies.Append("ShoppingCart", "", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            return RedirectToPage("/Confirmation", new { confirmationID = result});
        }

        public class CheckoutDetails
        {
            public string Name { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Province { get; set; } = string.Empty;
            public string PostalCode { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public long CcNumber { get; set; }
            public string CcExpiryDate { get; set; } = string.Empty;
            public int Cvv { get; set; }
            public string Products { get; set; } = string.Empty;
        }

        bool IsAnyNullOrEmpty(object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

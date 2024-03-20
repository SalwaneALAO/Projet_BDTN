using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client_Sales.Pages.Consoles
{
    public class CreateModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public CreateModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var consoles = await _salesClient.ConsolecsAllAsync();
            ViewData["ConsoleId"] = new SelectList(consoles, "ConsoleId", "Name");

            // Supposons que vous avez un moyen d'obtenir la liste des fabricants ici
            var manufacturers = await _salesClient.ManufacturersAllAsync(); // Assurez-vous que cette méthode existe et fonctionne comme prévu
            LoadManufacturers(manufacturers);

            return Page();
        }

        [BindProperty]
        public Consolec Consolec { get; set; } = default!;
        public SelectList Manufacturers { get; set; } // Assurez-vous que cette propriété existe.

        // Supposons que Manufacturer est une classe contenant 'Id' et 'Name'
        public void LoadManufacturers(IEnumerable<Manufacturer> manufacturers)
        {
            Manufacturers = new SelectList(manufacturers.Select(m =>
                new { Id = m.ManufacturerId, Name = $"{m.ManufacturerId} - {m.Name}" }), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Consolec == null)
            {
                return Page();
            }

            try
            {
                var createdConsolec = await _salesClient.ConsolecsPOSTAsync(Consolec);
                // If we get here, the POST was successful
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 201)
            {
                // The API returned a 201 Created response which we are treating as successful
                // You might want to fetch the location header if you want to redirect to the new resource
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                // Handle other exceptions here, possibly logging the error
                return Page();
            }
        }
    }
}
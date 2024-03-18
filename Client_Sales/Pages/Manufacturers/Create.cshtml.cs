using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Ensure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Manufacturers
{
    public class CreateModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public CreateModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Manufacturer Manufacturer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _salesClient.ManufacturersPOSTAsync(Manufacturer);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 201)
            {
                // The resource was created successfully, so redirect as needed
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                // Log the exception, add error to ModelState, or handle it as needed
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the manufacturer: {ex.Message}");
                return Page();
            }

            try
            {
                // Use the API client to create a new manufacturer
                await _salesClient.ManufacturersPOSTAsync(Manufacturer);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                // Log the error or handle it as needed
                ModelState.AddModelError(string.Empty, "An error occurred while creating the manufacturer: " + ex.Message);
                return Page();
            }
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Ensure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Manufacturers
{
    public class EditModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public EditModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        [BindProperty]
        public Manufacturer Manufacturer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Use the API client to get the manufacturer details
                Manufacturer = await _salesClient.ManufacturersGETAsync(id.Value);
                if (Manufacturer == null)
                {
                    return NotFound();
                }
            }
            catch (ApiException)
            {
                // Handle API exceptions, e.g., not found
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _salesClient.ManufacturersPUTAsync(Manufacturer.ManufacturerId, Manufacturer);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 204)
            {
                // The update was processed successfully, so redirect as needed
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                // Log the exception, add error to ModelState, or handle it as needed
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the manufacturer: {ex.Message}");
                return Page();
            }

            try
            {
                // Use the API client to update the manufacturer
                await _salesClient.ManufacturersPUTAsync(Manufacturer.ManufacturerId, Manufacturer);
            }
            catch (ApiException e)
            {
                if (e.StatusCode == 404) // Not Found
                {
                    return NotFound();
                }
                else
                {
                    throw; // Rethrow for other types of API exceptions
                }
            }

            return RedirectToPage("./Index");
        }
    }
}

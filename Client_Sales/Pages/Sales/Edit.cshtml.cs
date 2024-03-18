using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Ensure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Sales
{
    public class EditModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public EditModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Use the API client to retrieve sale details
                Sale = await _salesClient.SalesGETAsync(id.Value);
                if (Sale == null)
                {
                    return NotFound();
                }
                return Page();
            }
            catch (ApiException)
            {
                // Handle API errors or when the sale is not found
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _salesClient.SalesPUTAsync(Sale.SaleId, Sale);
                // Assuming the update is successful, redirect to the index page or other appropriate action
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 204)
            {
                // If the status code is 204, interpret it as a successful update
                return RedirectToPage("./Index"); // Redirect to the index page or wherever appropriate
            }
            catch (ApiException ex)
            {
                // Handle other unexpected status codes
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the sale: {ex.Message}");
                return Page();
            }

            try
            {
                // Use the API client to update the sale details
                await _salesClient.SalesPUTAsync(Sale.SaleId, Sale);
            }
            catch (ApiException e)
            {
                if (e.StatusCode == 404)
                {
                    // Handle the case when the sale doesn't exist
                    return NotFound();
                }
                else
                {
                    // For other API errors, throw the exception to be handled elsewhere or show an error message
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}

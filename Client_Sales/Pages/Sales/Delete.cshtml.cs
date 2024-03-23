using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Ensure your API client namespace is correctly referenced
//delete
namespace Client_Sales.Pages.Sales
{
    public class DeleteModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public DeleteModel(ISalesClient salesClient)
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

            // Use the API client to retrieve the sale by id
            Sale = await _salesClient.SalesGETAsync(id.Value);
            if (Sale == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _salesClient.SalesDELETEAsync(id.Value);
                // Assuming the delete is successful, redirect to the index page or other appropriate action
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 204)
            {
                // If the status code is 204, interpret it as a successful delete
                return RedirectToPage("./Index"); // Redirect to the index page or wherever appropriate
            }
            catch (ApiException ex)
            {
                // Handle other unexpected status codes
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the sale: {ex.Message}");
                return Page();
            }

            // Use the API client to delete the sale
            await _salesClient.SalesDELETEAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}

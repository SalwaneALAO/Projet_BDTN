using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Ensure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Sales
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
        public Sale Sale { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var createdSale = await _salesClient.SalesPOSTAsync(Sale);
                // You can use createdSale if you need the result of the POST operation
                // Redirect to a new page or indicate success as needed
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 201)
            {
                // If the status code is 201, interpret it as success and handle accordingly
                // Possibly log the response or inform the user of success
                return RedirectToPage("./Index"); // Or wherever you need to redirect to
            }
            catch (ApiException ex)
            {
                // Handle other unexpected status codes
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the sale: {ex.Message}");
                return Page();
            }

            // Use the API client to add a new sale
            await _salesClient.SalesPOSTAsync(Sale);

            return RedirectToPage("./Index");
        }
    }
}

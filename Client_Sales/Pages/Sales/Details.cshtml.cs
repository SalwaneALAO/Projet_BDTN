using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Ensure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Sales
{
    public class DetailsModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public DetailsModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

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
                // Handle the case when the sale is not found or any other API error occurs
                return NotFound();
            }
        }
    }
}

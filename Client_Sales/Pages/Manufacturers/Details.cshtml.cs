using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API; // Make sure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Manufacturers
{
    public class DetailsModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public DetailsModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

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
                // Handle the case where the manufacturer is not found
                // This might be a not found exception or similar
                return NotFound();
            }

            return Page();
        }
    }
}

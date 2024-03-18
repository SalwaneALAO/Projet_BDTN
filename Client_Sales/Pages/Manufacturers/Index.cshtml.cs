using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;
using Microsoft.AspNetCore.Mvc.Rendering; // Ensure your API client namespace is correctly referenced

namespace Client_Sales.Pages.Manufacturers
{
    public class IndexModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public IndexModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        public IList<Manufacturer> Manufacturer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var manufacturer = await _salesClient.ManufacturersAllAsync();
             ViewData["ManufacturerId"] = new SelectList(manufacturer, "ManufacturerId", "Name");
            //Consolec = consoles.ToList(); // Convert ICollection to IList
            // Use the API client to get the list of manufacturers
            Manufacturer = manufacturer.ToList();
        }
        
    }
}

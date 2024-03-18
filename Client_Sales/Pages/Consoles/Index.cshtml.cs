using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Client_Sales.API;

namespace Client_Sales.Pages.Consoles
{
    public class IndexModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public IndexModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        public IList<Consolec> Consolec { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var consoles = await _salesClient.ConsolecsAllAsync();
            ViewData["ConsoleId"] = new SelectList(consoles, "ConsoleId", "Name");
            Consolec = consoles.ToList(); // Convert ICollection to IList
            
        }
    }
}


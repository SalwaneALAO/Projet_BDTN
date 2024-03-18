using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;

namespace Client_Sales.Pages.Consoles
{
    public class DetailsModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public DetailsModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        public Consolec Consolec { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Consolec = await _salesClient.ConsolecsGETAsync(id.Value);
                if (Consolec == null)
                {
                    return NotFound();
                }
            }
            catch (ApiException)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;

namespace Client_Sales.Pages.Consoles
{
    public class EditModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public EditModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        [BindProperty]
        public Consolec Consolec { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Consolec = await _salesClient.ConsolecsGETAsync(id.Value);

            if (Consolec == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _salesClient.ConsolecsPUTAsync(Consolec.ConsoleId, Consolec);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 204)
            {
                // Treat 204 as success and redirect or take other appropriate action
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                // Handle other API exceptions or log them
                ModelState.AddModelError(string.Empty, "An error occurred while updating the console.");
                return Page();
            }

            try
            {
                await _salesClient.ConsolecsPUTAsync(Consolec.ConsoleId, Consolec);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}

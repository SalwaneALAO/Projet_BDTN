using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Client_Sales.API; // Assurez-vous que l'espace de noms de votre client API est correctement référencé.

namespace Client_Sales.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public CreateModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        // La propriété pour SelectList des consoles avec ID et Nom.
        public SelectList ConsoleList { get; set; }

        [BindProperty]
        public Sale Sale { get; set; } = new Sale(); // Modèle de vente initialisé.

        public async Task<IActionResult> OnGetAsync()
        {
            var consoles = await _salesClient.ConsolecsAllAsync();

            // Créez une liste qui combine l'ID et le nom pour chaque console.
            ConsoleList = new SelectList(consoles.Select(c =>
                new { Id = c.ConsoleId, Name = $"{c.ConsoleId} - {c.Name}" }), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var consoles = await _salesClient.ConsolecsAllAsync();
                ConsoleList = new SelectList(consoles.Select(c =>
                    new { Id = c.ConsoleId, Name = $"{c.ConsoleId} - {c.Name}" }), "ConsoleId", "Name");
                return Page();
            }

            try
            {
                var createdSale = await _salesClient.SalesPOSTAsync(Sale);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex) when (ex.StatusCode == 201)
            {
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the sale: {ex.Message}");
                return Page();
            }
        }
    }
}

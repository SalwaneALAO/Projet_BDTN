﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;
//delete
namespace Client_Sales.Pages.Consoles
{
    public class DeleteModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public DeleteModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        [BindProperty]
        public Consolec Consolec { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _salesClient.ConsolecsDELETEAsync(id.Value);
            }
            catch (ApiException ex) when (ex.StatusCode == 204)
            {
                // The delete was processed successfully, so redirect as needed
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                // Log the exception, add error to ModelState, or handle it as needed
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the console: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client_Sales.API;

namespace Client_Sales.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly ISalesClient _salesClient;

        public IndexModel(ISalesClient salesClient)
        {
            _salesClient = salesClient;
        }

        public IList<Sale> Sale { get; set; }

        public async Task OnGetAsync()
        {
            Sale = (await _salesClient.SalesAllAsync()).ToList();
        }
    }
}

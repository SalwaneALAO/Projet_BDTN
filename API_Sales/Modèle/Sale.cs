using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API_Sales.Modèle
{
    //Sales Classieybhihu"'
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public int Year { get; set; }
        public double UnitsSold { get; set; }

        // Foreign Key
        [ForeignKey("Consolec")]
        public int ConsoleId { get; set; }
       
    }
}

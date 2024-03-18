
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//classe console
namespace API_Sales.Modèle
{
    public class Consolec
    {
        [Key]
        public int ConsoleId { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }

        // Navigation property for Manufacturer
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        
    }
}
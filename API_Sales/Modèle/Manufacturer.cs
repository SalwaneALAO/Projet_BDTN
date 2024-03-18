using System.ComponentModel.DataAnnotations;
namespace API_Sales.Modèle
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string OriginCountry { get; set; }

       

    }
}

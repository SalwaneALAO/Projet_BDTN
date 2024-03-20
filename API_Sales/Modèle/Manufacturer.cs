using System.ComponentModel.DataAnnotations;
namespace API_Sales.Modèle
{
    // Class manufacturer
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string OriginCountry { get; set; }

       

    }
}

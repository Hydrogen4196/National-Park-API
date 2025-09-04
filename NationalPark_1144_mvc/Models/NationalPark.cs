using System.ComponentModel.DataAnnotations;

namespace NationalPark_1144_mvc.Models
{
    public class NationalPark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime Cretaed { get; set; }
        public DateTime Established { get; set; }
    }
}

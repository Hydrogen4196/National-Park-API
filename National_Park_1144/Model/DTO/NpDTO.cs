using System.ComponentModel.DataAnnotations;

namespace National_Park_1144.Model.DTO
{
    public class NpDTO
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

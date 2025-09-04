﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NationalPark_1144_mvc.Models
{
    public class Trail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }
        public DateTime DateCreated { get; set; }
        public enum DifficultyType
        {
            Easy,
            Moderate,
            Difficult
        }
        public DifficultyType Difficulty { get; set; }
        [Display(Name ="National Park")]
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }
    }
}

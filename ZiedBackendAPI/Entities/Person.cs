using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;

namespace ZiedBackendAPI.Entities
{
    public class Person
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 75)]
        [Required]
        public string Name { get; set; }
        public DateTime PostTime { get; set; }
        public String Poster { get; set; }
        [Required(ErrorMessage = "Please enter your phone number"), MaxLength(15)]
        public int PhoneNumber { get; set; }
        public String Discription { get; set; }

        public List<PersonSector> PersonSector { get; set; }
        public Point Location { get; set; }
      
    }
}

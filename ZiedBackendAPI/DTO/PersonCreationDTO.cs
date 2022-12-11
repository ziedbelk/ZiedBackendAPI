using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZiedBackendAPI.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ZiedBackendAPI.DTO
{
    public class PersonCreationDTO
    {
        public string Name { get; set; }
        public System.DateTime PostTime { get; set; }
        public IFormFile Poster { get; set; }

        [Required(ErrorMessage = "Please enter your phone number"), MaxLength(15)]
        public int PhoneNumber { get; set; }
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
        public String Description { get; set; }



        //to send the informations of the related entities

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> SectorId { get; set; }
    }
}

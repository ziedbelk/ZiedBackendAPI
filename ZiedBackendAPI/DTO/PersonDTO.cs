using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZiedBackendAPI.Entities;
using ZiedBackendAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZiedBackendAPI.DTO
{
    public class PersonDTO
    {
        public string Name { get; set; }
        //public DateTime PostTime { get; set; }
        public IFormFile Poster { get; set; }
        public DateTime PostTime { get; set; }
        public List<Sector> Sectors { get; set; }
        public String Description { get; set; }

        public int PhoneNumber { get; set; }


    }
}

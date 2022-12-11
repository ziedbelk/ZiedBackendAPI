using AutoMapper;
using ZiedBackendAPI.DTO;
using ZiedBackendAPI.DTOs;
using ZiedBackendAPI.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ZiedBackendAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<SectorDTO, Sector>().ReverseMap();
            CreateMap<SectorCreationDTO, Sector>();


            CreateMap<PersonCreationDTO, Person>()
             .ForMember(x => x.Poster, options => options.Ignore())
             .ForMember(x => x.PersonSector, options => options.MapFrom(MapPersonSector))
             .ForMember(x => x.Location, x => x.MapFrom(dto =>
               geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<Person, PersonDTO>()
              .ForMember(x => x.Sectors, options => options.MapFrom(MapSectors));
            
            //CreateMap<Person, PersonDTO>()
            // .ForMember(x => x.Sectors, options => options.MapFrom(MapSectors));
            CreateMap<IdentityUser, UserDTO>();

        }
        private List<PersonSector> MapPersonSector(PersonCreationDTO personCreationDTO, Person person)
        {
            var result = new List<PersonSector>();

            if (personCreationDTO.SectorId == null) { return result; }

            foreach (var id in personCreationDTO.SectorId)
            {
                result.Add(new PersonSector() { SectorId = id });
            }

            return result;
        }
        private List<SectorDTO> MapSectors(Person person, PersonDTO persondto)
        {
            var result = new List<SectorDTO>();

            if (person.PersonSector != null)
            {
                foreach (var sector in person.PersonSector)
                {
                    result.Add(new SectorDTO() { Id = sector.SectorId, Name = sector.Sector.Name });
                }
            }

            return result;
        }

       

}
}

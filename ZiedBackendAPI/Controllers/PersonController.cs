using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZiedBackendAPI.DTO;
using ZiedBackendAPI.DTOs;
using ZiedBackendAPI.Entities;
using ZiedBackendAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ZiedBackendAPI.Controllers
{

    [Route("api/person")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]

    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<PersonController> logger;
        private string container = "person";



        public PersonController(ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<PersonController> logger
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
            this.logger = logger;
        }
        //[HttpPost]
        //public async Task<ActionResult<int>> Post([FromForm] PersonCreationDTO personCreationDTO)
        //{
        //    var person = mapper.Map<Person>(personCreationDTO);

        //    if (personCreationDTO.Poster != null)
        //    {
        //        person.Poster = await fileStorageService.SaveFile(container, personCreationDTO.Poster);
        //    }
        //    person.PostTime = DateTime.Today;

        //    context.Add(person);
        //    await context.SaveChangesAsync();
        //    return person.Id;
        //}

        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult<int>> Post([FromForm] PersonCreationDTO movieCreationDTO)
        {
            logger.LogInformation("Adding Worker");

            var movie = mapper.Map<Person>(movieCreationDTO);

            if (movieCreationDTO.Poster != null)
            {
                movie.Poster = await fileStorageService.SaveFile(container, movieCreationDTO.Poster);
            }

            context.Add(movie);
            await context.SaveChangesAsync();
            return movie.Id;
        }

        //[HttpGet("JobsTypeSearch")]
        //public async Task<ActionResult<PersonSearchDTO>> PostGet()
        //{
        //    var sectors = await context.sectors.OrderBy(x => x.Name).ToListAsync();

        //    var sectorsDTO = mapper.Map<List<GenreDTO>>(sectors);

        //    return new PersonSearchDTO() { sectors = sectorsDTO };
        //}
        [HttpGet("filter")]
        [AllowAnonymous]

        public async Task<ActionResult<List<PersonDTO>>> Filter([FromQuery] FilterPersonDTO filterPersonDTO)
        {
            logger.LogInformation("Searching for Worker by his active sector");

            var personQueryable = context.Person.AsQueryable();

            if (!string.IsNullOrEmpty(filterPersonDTO.Title))
            {
                personQueryable = personQueryable.Where(x => x.Name.Contains(filterPersonDTO.Title));
            }
       
        

            var persons = await personQueryable.OrderBy(x => x.PostTime).ToListAsync();
            return mapper.Map<List<PersonDTO>>(persons);
        }
        [HttpGet("get all the persons")] // api/sectors
        [AllowAnonymous]

        public async Task<ActionResult<List<Person>>> Get()
        {
            logger.LogInformation("Get all the available  workers");

            var persons = await context.Person.ToListAsync();
            return persons;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] PersonDTO personDTO)
        {
            logger.LogInformation("Updating Worker details");

            var person = await context.Person.Include(x => x.PersonSector)
               
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            person = mapper.Map(personDTO, person);

            if (personDTO.Poster != null)
            {
                person.Poster = await fileStorageService.EditFile(container, personDTO.Poster,
                    person.Poster);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            logger.LogInformation("Deleting Worker");

            var person = await context.Person.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            context.Remove(person);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(person.Poster, container);
            return NoContent();
        }
    }
}

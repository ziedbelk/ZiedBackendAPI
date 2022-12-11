using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZiedBackendAPI.DTOs;
using ZiedBackendAPI.Entities;
using ZiedBackendAPI.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ZiedBackendAPI.Controllers
{
    [Route("api/sectors")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]

    public class SectorController: ControllerBase
    {
        private readonly ILogger<SectorController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SectorController(ILogger<SectorController> logger,ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/sectors
        [AllowAnonymous]

        public async Task<ActionResult<List<SectorDTO>>> Get()
        {
            logger.LogInformation("Getting all the available Sectors");

            var sectors= await context.Sector.ToListAsync();
            return mapper.Map<List<SectorDTO>>(sectors);
        }

        [HttpGet("{Id:int}")]
        [AllowAnonymous]

        public async Task<ActionResult<SectorDTO>> Get(int Id)
        {
            var genre = await context.Sector.FirstOrDefaultAsync(x => x.Id == Id);

            if (genre == null)
            {
                return NotFound();
            }
            logger.LogInformation("Getting Sector by id");

            return mapper.Map<SectorDTO>(genre);
        }

        [HttpPost]
        public async Task <ActionResult> Post([FromBody] SectorCreationDTO genreCreationDTO)
        {
            logger.LogInformation("Adding Sector");

            var genre =mapper.Map<Sector>(genreCreationDTO);
            context.Add(genre);
            await context.SaveChangesAsync();
            return NoContent();


        }

        //here i am mapping from genre creattion dto to genre and i am maintaining the same instance of genre 

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SectorCreationDTO genreCreationDTO)
        {
            logger.LogInformation("Updating Sector");

            var genre = await context.Sector.FirstOrDefaultAsync(x => x.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            genre = mapper.Map(genreCreationDTO, genre);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            logger.LogInformation("Deleting Sector by id");

            var exists = await context.Sector.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Sector() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
    }


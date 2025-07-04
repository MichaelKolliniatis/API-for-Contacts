﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Web_API_for_Contacts_2._0.Data;
using Web_API_for_Contacts_2._0.Models;

namespace Web_API_for_Contacts_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController(ContactsDbContext context, IMapper mapper) : ControllerBase
    {

        private readonly ContactsDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<List<Hobby>>> GetHobbies()
        {
            return Ok(await _context.Hobby.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Hobby>> CreateHobby([FromBody] CreateHobbyDto newHobbyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingHobby = await _context.Hobby
                .FirstOrDefaultAsync(c => c.Name.ToLower() == newHobbyDto.Name.ToLower());

            if (existingHobby != null)
            {
                return Conflict(new { message = $"{newHobbyDto.Name} already exists." });
            }

            var newHobby = _mapper.Map<Hobby>(newHobbyDto);

            _context.Hobby.Add(newHobby);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{newHobby.Name} added with id {newHobby.Id}." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHobby([FromBody] Hobby hobbyToDelete)
        {
            if (hobbyToDelete == null || hobbyToDelete.Id == 0 || string.IsNullOrWhiteSpace(hobbyToDelete.Name))
            {
                return BadRequest(new { message = "Invalid Id or Name." });
            }

            var hobby = await _context.Hobby
                .FirstOrDefaultAsync(c => c.Id == hobbyToDelete.Id && c.Name.ToLower() == hobbyToDelete.Name.ToLower());

            if (hobby == null)
            {
                return NotFound(new { message = $"No hobby with Id {hobbyToDelete.Id} and name {hobbyToDelete.Name}." });
            }

            var personWithHobby = await _context.PersonHobby
                .FirstOrDefaultAsync(p => p.HobbyId == hobbyToDelete.Id);

            if (personWithHobby != null)
            {
                return Conflict(new { message = $"Cannot delete the hobby {hobbyToDelete.Name} because there is at least one person associated with it." });
            }

            _context.Hobby.Remove(hobby);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{hobby.Name} deleted successfully." });
        }


    }
}
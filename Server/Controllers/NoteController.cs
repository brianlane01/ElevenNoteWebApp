using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Server.Services.NoteServices;
using Shared.Models.Note;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        private string? GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if(userIdClaim == null) 
                return null;
            
            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if(userId == null)
                return false;

            _noteService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<NoteListItem>> Index()
        {
            if(!SetUserIdInService())
                return new List<NoteListItem>();
            
            var notes = await _noteService.GetAllNotesAsync();
            
            return notes.ToList();
        }

        [HttpPost]
        public async Task<IActionResult>  Create(NoteCreate model)
        {
            if(model == null)
                return BadRequest();

            if(!SetUserIdInService())
                return Unauthorized();

            bool wasSuccessful = await _noteService.CreateNoteAsync(model);

            if(wasSuccessful)
                return Ok ();

            else    
                return UnprocessableEntity();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Note(int id)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var note = await _noteService.GetNoteByIdAsync(id);

            if(note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> NoteByCategory(int categoryId)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var note = await _noteService.GetNoteByCategoryIdAsync(categoryId);

            if(note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, NoteEdit model)
        {
            if(!SetUserIdInService())
                return Unauthorized();

            if(model == null || !ModelState.IsValid) 
                return BadRequest();

            bool wasSuccessful = await _noteService.UpdateNoteAsync(model);

            if(wasSuccessful)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!SetUserIdInService())
                return Unauthorized();

            var note = await _noteService.GetNoteByIdAsync(id);

            if(note == null)
                return NotFound();

            bool wasSuccessful = await _noteService.DeleteNoteAsync(id);

            if(!wasSuccessful)
                return BadRequest();
            
            return Ok();
        }

    }
}
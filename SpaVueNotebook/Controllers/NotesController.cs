using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaVueNotebook.Model;

namespace SpaVueNotebook.Controllers
{
    //Контролер CRUD
    public class NotesController : Controller
    {
        private readonly INotesDataAccessLayer _notes;

        public NotesController(INotesDataAccessLayer notes)
        {
            _notes = notes;
        }

        [HttpGet]
        [Route("api/Notes/All")]
        public IActionResult AllNotes()
        {
            return Ok(
                _notes.GetAllNotes()
            );
        }

        [HttpPost]
        [Route("api/Notes/Create")]
        public IActionResult Post([FromBody] Note note)
        {
            return Ok(
                _notes.AddNote(note)
            );
        }

        [HttpPut]
        [Route("api/Note/Edit")]
        public IActionResult Put([FromBody] Note note)
        {
            return Ok(
                _notes.UpdateNote(note)
            );
        }

        [HttpDelete]
        [Route("api/Note/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _notes.DeleteNote(id)
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaVueNotebook.Model
{
    public interface INotesDataAccessLayer
    {
        IEnumerable<Note> GetAllNotes();
        int AddNote(Note note);
        int DeleteNote(int id);
        int UpdateNote(Note note);
    }
    public class NotesDataAccessLayer: INotesDataAccessLayer
    {
        private readonly ApplicationDbContext _context;

        public NotesDataAccessLayer(ApplicationDbContext context)
        {
            _context = context;
        }

        //Получение всех записей
        public IEnumerable<Note> GetAllNotes()
        {
            try
            {
                return _context.Notes.ToList();
            }
            catch
            {
                throw;
            }
        }

        //Добавление записи
        public int AddNote(Note note)
        {
            try
            {
                _context.Add(note);
                _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Редактировать запись
        public int UpdateNote(Note note)
        {
            try
            {
                var originalModel = _context.Notes.Single(x =>
                    x.Id == note.Id
                );

                originalModel.FirstName = note.FirstName;
                originalModel.LastName = note.LastName;
                originalModel.Phone = note.Phone;
                originalModel.Email = note.Email;

                _context.Update(originalModel);
                _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Удалить запись
        public int DeleteNote(int id)
        {
            try
            {
                _context.Entry(new Note { Id = id }).State = EntityState.Deleted; ;
                _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}

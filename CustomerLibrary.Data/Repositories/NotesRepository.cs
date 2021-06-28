 using CustomerClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary.Data
{
    public  class NotesRepository : BaseRepository, IEntityRepository<Note>
    {
        public int Create(Note note)
        {
            var newNoteId = 0;

            using var connection = GetConnection();

            var command = new SqlCommand("INSERT INTO [dbo].[Notes] (CustomerId, Note) VALUES (@CustomerId, @Note) " +
                        "SELECT CAST(scope_identity() AS int)", connection);

            var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Value = note.CustomerId
            };

            var noteParam = new SqlParameter("@Note", SqlDbType.NVarChar, 255)
            {
                Value = note.NoteText
            };

            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(noteParam);

            var response = command.ExecuteScalar();
            if (response is not null)
            {
                newNoteId = (int)response;
            }

            note.NoteId = newNoteId;

            return newNoteId;
        }

        public Note Read(Note note)
        {
            using var connection = GetConnection();

            var command = new SqlCommand("SELECT * FROM [dbo].[Notes] " +
                        "WHERE NoteId = @NoteId", connection);


            var NoteIdParam = new SqlParameter("@NoteId", SqlDbType.Int)
            {
                Value = note.CustomerId
            };

            command.Parameters.Add(NoteIdParam);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Note
                    {
                        NoteId = (int)reader["NoteId"],
                        CustomerId = (int)reader["CustomerId"],
                        NoteText = reader["Note"]?.ToString()
                    };
                }
            }

            return null;
        }

        public List<Note> ReadAll(int customerId)
        {
            using var connection = GetConnection();

            var command = new SqlCommand("SELECT * FROM [dbo].[Notes] " +
	                    "WHERE [CustomerId] = @CustomerId", connection);

            var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Value = customerId
            };

            command.Parameters.Add(customerIdParam);

            var notes = new List<Note>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    notes.Add(new Note
                    {
                        NoteId = (int)reader["NoteId"],
                        CustomerId = (int)reader["CustomerId"],
                        NoteText = reader["Note"]?.ToString()
                    });
                }
            }

            return notes;
        }

        public void Update(Note note)
        {
            using var connection = GetConnection();

            var command = new SqlCommand("UPDATE [dbo].[Notes] SET [CustomerId] = @CustomerId,[Note] = @Note " +
	                    "WHERE [NoteId] = @NoteId", connection);

            var noteIdParam = new SqlParameter("@NoteId", SqlDbType.Int)
            {
                Value = note.NoteId
            };

            var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Value = note.CustomerId
            };

            var noteParam = new SqlParameter("@Note", SqlDbType.NVarChar, 255)
            {
                Value = note.NoteText
            };

            command.Parameters.Add(noteIdParam);
            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(noteParam);

            command.ExecuteNonQuery();
        }

        public void Delete(Note note)
        {
            using var connection = GetConnection();

            var command = new SqlCommand("DELETE FROM [dbo].[Notes] " +
	                    "WHERE [NoteId] = @NoteId", connection);

            var noteIdParam = new SqlParameter("@NoteID", SqlDbType.Int)
            {
                Value = note.NoteId
            };

            command.Parameters.Add(noteIdParam);

            command.ExecuteNonQuery();
        }

        public void DeleteAll()
        {
            using var connection = GetConnection();

            var command = new SqlCommand("DELETE FROM [dbo].[Notes]", connection);

            command.ExecuteNonQuery();
        }

        public List<Note> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}

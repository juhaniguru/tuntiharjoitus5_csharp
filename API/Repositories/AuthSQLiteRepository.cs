
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.Data.Sqlite;

namespace API.Repositories
{
    // tässä on ensimmäinen ongelma! Arkkitehtuurissa samalla tasolla olevat riippuvuudet riippuvat toisistaan
    // ÄLÄ KOSKAAN rakenna sovelluksen arkkitehtuuria niin, että data kulkee saman kerroksen sisällä sivuttain
    
    public class AuthSQLiteRepository(SqliteConnection _connection) : IAuthRepo
    {
        public async Task<AppUser?> Login(string username, string password)
        {
            AppUser? user;
           
            // haetaan käyttäjä käyttäjänimen mukaan
            var command = _connection.CreateCommand();
            command.CommandText = $"SELECT id, first_name, last_name, username, password FROM users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            
            using var reader = await command.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return null;
            }





            // luetaan luupissa kaikki rivit, jotka kyselyllä saadaan ulos
            // tietokannasta
            await reader.ReadAsync();

           
            user = new AppUser
            {
                Id = reader.GetInt64(0),
                Firstname = reader.GetString(1),
                Lastname = reader.GetString(2),
                Username = reader.GetString(3),
                Password = reader.GetString(4)
            };
            


            

            return user;
        }

        
    }
}

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
    
    public class AuthSQLiteRepository(SqliteConnection _connection, ILogRepo _logRepo) : IAuthRepo
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
            // jos käyttäjä löytyy, verrataan salasanoja
            

            // tämä rikkoo SRP:tä (Single Responsibility Principleä).
            // Repositorio-layerin tehtävä ei ole hoitaa businesslogiikaa, vaan toimia data access layerina
            // toisin sanoen: salasanan tarkistus ei ole data access layerin tehtävä
            if (password != user.Password)
            {
                return null;
            }

            // jos käyttäjä löytyy ja salasana on oikein,
            // lisätään rivi logs-tauluun, josta näkee, 
            // milloin käyttäjä on viimeksi kirjautunut sisään


            // TÄMÄ EI SAA OLLA TÄÄLLÄ
            // REPOSITORYSTA EI SAA TEHDÄ KUTSUA TOISEEN REPOSITORIOON
            // KOSKA REPOSITORIOT OVAT ARKKITEHTUURISSA SAMALLA SOVELLUSKERROKSELLA

            // LISÄKSI TÄMÄ RIKKOO SRP:TÄ. NYT YKSI REPOSITORION METODI ON VASTUUSSA USEAMMASTA ASIASTA

            await _logRepo.Create(new AddLogEntryReq
            {
                UserName = user.Username
            });


            return user;
        }

        
    }
}
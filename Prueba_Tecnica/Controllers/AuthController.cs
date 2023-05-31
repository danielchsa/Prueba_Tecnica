using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica.Models;


namespace Prueba_Tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    [HttpGet]
    public IActionResult Users()
    {
        string path = "./Data/users.json"; 
        try
        {
                
            string js = System.IO.File.ReadAllText(path);

            
            var users = JsonConvert.DeserializeObject<User[]>(js);

            return Ok(users);

        }
        catch (FileNotFoundException)
        {
           
            return BadRequest();
        }
        catch (JsonException)
        {
          
            return BadRequest();
        }
    }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            string path = "./Data/users.json";

            try
            {
                string js = System.IO.File.ReadAllText(path);
                var users = JsonConvert.DeserializeObject<List<User>>(js);

                users.Add(user);

                var jsonObject = JsonConvert.SerializeObject(users);

                System.IO.File.WriteAllText(path, jsonObject);

                

                return Ok(new { success = "Success" }); ;
            }
            catch (FileNotFoundException)
            {
                return BadRequest();
            }
            catch (JsonException)
            {
                
                return BadRequest();
            }

        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(User user)
        {
            string path = "./Data/users.json";

            try
            {
                string js = System.IO.File.ReadAllText(path);
                var users = JsonConvert.DeserializeObject<List<User>>(js);

                var userValid = users.Find(u => u.user == user.user && u.password == user.password);

                if(userValid == null)
                {
                    return BadRequest();
                }

                return Ok(userValid);
            }
            catch (FileNotFoundException)
            {
                return BadRequest();
            }
            catch (JsonException)
            {
              
                return BadRequest();
            }

            return Ok();
        }


    }

}

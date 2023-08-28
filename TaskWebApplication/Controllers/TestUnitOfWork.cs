using JWTAuthorization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWebApplication.UnitOfWork;

//didnt work just for trying
namespace TaskWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestUnitOfWork : ControllerBase
    {
        /*private readonly ITryUnitOfWork tryUnitOfWork;

        public TestUnitOfWork(ITryUnitOfWork _tryUnitOfWork)
        {
            tryUnitOfWork = _tryUnitOfWork;
        }
        [HttpGet]
        public IActionResult DataSeeding() 
        {
            for (int i = 10; i < 15; i++) 
            {
                tryUnitOfWork.userRepository.UserRegister(new UserRegisterDto()
                {
                    Email=$"usertest{i}@example.com",
                    Password="789Mikha.",
                    ComfirmPassword="789Mikha."

                });;

                tryUnitOfWork.toDoListRepository.Create(new Models.ToDoListItemCreateDto()
                {
                    Title= $"testcaseUserof{i}",
                    Completed=false

                }, $"usertest{i}@example.com"); ;
            }
            
            return Ok(tryUnitOfWork.effectedRows());
        }*/
    }
}

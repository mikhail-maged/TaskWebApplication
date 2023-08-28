using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveToDoController : ControllerBase
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly IMapper mapper;
        public LiveToDoController(IMapper _mapper)
        {

            mapper = _mapper;
        }

        [HttpGet("GetAllListItems")]
        public async Task<IActionResult> GetAllListItems()
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if(data!=null) 
                {
                    List<ConsumeApiDto>? consumeApiDtos = JsonSerializer.Deserialize<List<ConsumeApiDto>>(data) ?? null;
                    List<ToDoListItemsDto>? toDoListItemsDtos = mapper.Map<List<ToDoListItemsDto>>(consumeApiDtos);

                    return Ok(toDoListItemsDtos);

                }
                
            }
            return BadRequest();
        }


        [HttpGet("GetParticularPage/{pageNumder:int}/{pageElementsCount:int}")]
        public async Task<IActionResult> GetParticularPage(int pageNumder, int pageElementsCount)
        {

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if(data!=null)
                {
                    List<ConsumeApiDto>? consumeApiDtos = JsonSerializer.Deserialize<List<ConsumeApiDto>>(data) ?? null;

                    int totalPages = (int)Math.Ceiling((decimal)consumeApiDtos.Count() / pageElementsCount);

                    List<ConsumeApiDto>? choosenPage = consumeApiDtos.Skip((pageNumder - 1) * pageElementsCount).Take(pageElementsCount).ToList();
                    List<ToDoListItemsDto>? choosenmappedPage = mapper.Map<List<ToDoListItemsDto>>(choosenPage);
                    return Ok(new { totalPages = totalPages, currentPage = pageNumder, items = choosenmappedPage });

                }
               
            }
            return BadRequest();
        }
    }
}

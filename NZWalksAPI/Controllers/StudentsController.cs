using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = { "Ömer Faruk", "Ayşe", "Remzi", "Tarık", "Sümeyye", "Sena", "Merve", "Mehmet" };
            return Ok(studentNames);
        }
    }
}

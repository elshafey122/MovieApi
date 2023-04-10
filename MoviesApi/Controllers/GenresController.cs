using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresServices _GenresServices;
        public GenresController(IGenresServices genresServices)
        {
            _GenresServices = genresServices;
        }
        [HttpGet]
        public async Task<IActionResult> getasync()
        {
            var genres = await _GenresServices.GetAll();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> postasync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _GenresServices.Add(genre);
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putasync(byte id, [FromBody] CreateGenreDto dto)
        {
            var genre = await _GenresServices.GetById(id);
            if (genre == null)
                return NotFound($"no element to delete in id {id}");
            genre.Name = dto.Name;
            _GenresServices.Update(genre);
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteasync(byte id)
        {
            var genre = await _GenresServices.GetById(id);
            if (genre == null)
                return NotFound($"no element to delete in id {id}");
            _GenresServices.Delete(genre);
            return Ok(genre);
        }
    }
    
}

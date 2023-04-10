using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly List<string> _allowformat = new List<string> { ".jpg", ".png" };
        private readonly double allowsize = 1048567;
        private readonly IMoviesServices _moviesServices;
        private readonly IGenresServices _GenresServices;
        private readonly IMapper _mapper;
        public MoviesController( IMoviesServices moviesServices , IGenresServices iGenresServices , IMapper mapper)
        {
            _moviesServices = moviesServices;
            _GenresServices = iGenresServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult>getasync()
        {
            var movies = await _moviesServices.GetAll();
            var mapperdata = _mapper.Map<IEnumerable<MovieDetails>>(movies);
            return Ok(mapperdata);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>getmoveid(int id)
        {
            var movie = await _moviesServices.GetById(id);
            if (movie == null)
                return BadRequest("no movie with id");
            var datamapper = _mapper.Map<MovieDetails>(movie);
            return Ok(datamapper);
        }
        [HttpGet("getbygenreid")]
        public async Task<IActionResult> getmoviebygenreid(byte genreid)
        {
            var movies = await _moviesServices.GetAll(genreid);
            var datamapper = _mapper.Map<MovieDetails>(movies);
            return Ok(datamapper);
        }

        [HttpPost]
        public async Task<IActionResult>createmovie([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("poster is required");
           
            if (!_allowformat.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("only jpg and png are allowed");
            }
            if(dto.Poster.Length > allowsize)
            {
                return BadRequest("size is more than 1mb");
            }
            var isvalidgenreid =await _GenresServices.IsValidGenres(dto.GenreId);
            if (!isvalidgenreid)
            {
                return BadRequest("genreid not found in database");
            }
            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = datastream.ToArray();
            _moviesServices.Add(movie);
            return Ok(movie);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteid(int id)
        {
            var movie =await _moviesServices.GetById(id);
            if (movie == null)
                return BadRequest($"no movie with id {id} to delete");
            _moviesServices.Delete(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>modifydata(int id,[FromForm] MovieDto dto)
        {
            var movie =await _moviesServices.GetById(id);
            var isvalid = await _GenresServices.IsValidGenres(dto.GenreId);
            if(!isvalid)
            {
                return BadRequest("not correct genderid");
            }
            if(dto.Poster!=null)
            {
                if (!_allowformat.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("only jpg and png are allowed");
                }
                if (dto.Poster.Length > allowsize)
                {
                    return BadRequest("size is more than 1mb");
                }
                using var datastream= new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();
            }
            
            movie.Title = dto.Title;
            movie.Rate = dto.Rate;
            movie.StoreLine = dto.StoreLine;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;

            _moviesServices.Update(movie);
            return Ok(movie);
        }
    }
}

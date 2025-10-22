using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoryDto = new List<CategoryDto>();
            foreach (var cat in categories)
            {
                categoryDto.Add(_mapper.Map<CategoryDto>(cat));
            }
            return Ok(categoryDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound($"La categoria con el id: {id} no existe.");
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return BadRequest(ModelState); //ModelState?????
            }
            if (_categoryRepository.CategoryExist(createCategoryDto.Name))
            {
                ModelState.AddModelError("CustomError", "La categoria ya existe.");
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(createCategoryDto);
            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al crear la categoria con el nombre {category.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }


        [HttpPatch("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] CreateCategoryDto updateCategoryDto)
        {
            if (!_categoryRepository.CategoryExist(id))
            {
                return NotFound($"La categoria con el id: {id} no existe.");
            }
            if (updateCategoryDto == null)
            {
                return BadRequest(ModelState); //ModelState?????
            }
            if (_categoryRepository.CategoryExist(updateCategoryDto.Name))
            {
                ModelState.AddModelError("CustomError", "La categoria ya existe.");
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(updateCategoryDto);
            category.Id = id;
            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al actualizars la categoria con el nombre {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int id)
        {
            if (!_categoryRepository.CategoryExist(id))
            {
                return NotFound($"La categoria con el id: {id} no existe.");
            }


            var category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound($"La categoria con el id: {id} no existe.");
            }
            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al eliminar la categoria con el nombre {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

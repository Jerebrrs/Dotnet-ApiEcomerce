🧱 ASP.NET Core + Entity Framework + Repository Pattern + AutoMapper

Este documento resume los pasos y conceptos básicos para crear una aplicación ASP.NET Core Web API utilizando Entity Framework Core, el Patrón Repository, DTOs y AutoMapper.

🚀 1. Crear la aplicación
dotnet new webapi -n NombreDelProyecto

📂 2. Estructura recomendada del proyecto

Organizar las carpetas de la siguiente forma:

/Data
/Models
/Repository
/Mapping
/Dtos

Carpeta	Descripción
Data	Contiene el ApplicationDbContext.
Models	Incluye las clases de entidades (por ejemplo, Category).
Repository	Contiene las interfaces y clases del patrón Repository.
Mapping	Incluye las configuraciones de AutoMapper.
Dtos	Contiene los Data Transfer Objects (DTOs).
📦 3. Instalación de paquetes

Instalar los paquetes necesarios mediante NuGet:

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

⚙️ 4. Instalación de la herramienta de migraciones

Instalar globalmente la herramienta de Entity Framework:

dotnet tool install --global dotnet-ef

🧩 5. Migraciones de base de datos
Crear la primera migración
dotnet ef migrations add InitialMigration

Aplicar las migraciones a la base de datos
dotnet ef database update


💡 Esto creará la base de datos y aplicará todas las migraciones pendientes.

🧭 6. Patrón Repository

El Patrón Repository abstrae el acceso a datos, separando la lógica de negocio de la lógica de persistencia.

Ejemplo de interfaz (ICategoryRepository)
public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category GetById(int id);
    bool Create(Category category);
    bool Update(Category category);
    bool Delete(Category category);
    bool Save();
}

Ejemplo de implementación (CategoryRepository)
public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Category> GetAll() => _db.Categories.ToList();

    public Category GetById(int id) => _db.Categories.FirstOrDefault(c => c.Id == id);

    public bool Create(Category category)
    {
        _db.Categories.Add(category);
        return Save();
    }

    public bool Update(Category category)
    {
        _db.Categories.Update(category);
        return Save();
    }

    public bool Delete(Category category)
    {
        _db.Categories.Remove(category);
        return Save();
    }

    public bool Save() => _db.SaveChanges() >= 0;
}


🔁 También se puede crear un repositorio genérico (IRepository<T>) para reutilizar la lógica con otras entidades.

📦 7. DTOs (Data Transfer Objects)

Los DTOs se utilizan para transferir datos entre cliente y servidor, evitando exponer directamente las entidades del modelo.

Ejemplo de DTO para creación
using System.ComponentModel.DataAnnotations;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Name { get; set; } = string.Empty;
}

🔁 8. Configuración de AutoMapper

Instalar el paquete:

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

Crear la clase de mapeo en /Mapping
using AutoMapper;
using Proyecto.Models;
using Proyecto.Dtos;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        // Se pueden agregar más mapeos según sea necesario
    }
}

🧠 9. Controlador API

Ejemplo de controlador básico para Category:

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        if (createCategoryDto == null)
            return BadRequest(ModelState);

        var category = _mapper.Map<Category>(createCategoryDto);

        if (!_categoryRepository.Create(category))
        {
            ModelState.AddModelError("CustomError", $"Algo salió mal al crear la categoría {category.Name}");
            return StatusCode(500, ModelState);
        }

        // Devuelve un código 201 Created y la URL del nuevo recurso
        return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
    }
}


🧩 ¿Por qué se usa CreatedAtRoute?
Devuelve un HTTP 201 (Created) junto con la ubicación (Location) del recurso recién creado, lo que sigue las buenas prácticas REST.

🧾 10. Inyección de dependencias (Program.cs)

Registrar los servicios necesarios para los repositorios y AutoMapper:

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(typeof(Program));


⚠️ Verificar la compatibilidad de versiones entre .NET, Entity Framework y AutoMapper.
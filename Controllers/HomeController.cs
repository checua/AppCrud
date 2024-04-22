using AppCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using AppCrud.Repositorios.Contrato;

namespace AppCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Departamento> _departamentoRepository;
        private readonly IGenericRepository<Empleado> _empleadoRepository;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Departamento> departamentoRepository,
            IGenericRepository<Empleado> empleadoRepository)
        {
            _logger = logger;
            _departamentoRepository = departamentoRepository;
            _empleadoRepository = empleadoRepository;
        }

        public async IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> listaEmpleado()
        {
            
            List<Empleado> _lista = await _empleadoRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }
        [HttpGet]
        public async Task<IActionResult> listaDepartamentos()
        {

            List<Departamento> _lista = await _departamentoRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> guardarEmpleado([FromBody] Empleado modelo)
        {

            bool _resultado = await _empleadoRepository.Guardar(modelo);
            if(_resultado)
                return StatusCode(StatusCodes.Status200OK, new {valor = _resultado, msg = "ok"});
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Eorror" });
        }

        [HttpPut]
        public async Task<IActionResult> editarEmpleado([FromBody] Empleado modelo)
        {

            bool _resultado = await _empleadoRepository.Editar(modelo);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Eorror" });
        }

        [HttpDelete]
        public async Task<IActionResult> eliminarEmpleado(int idEmpleado)
        {

            bool _resultado = await _empleadoRepository.Eliminar(idEmpleado);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Eorror" });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

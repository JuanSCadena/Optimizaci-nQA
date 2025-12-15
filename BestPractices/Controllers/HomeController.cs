using Best_Practices.Infraestructure.Factories;
using Best_Practices.Infraestructure.Singletons;
using Best_Practices.Models;
using Best_Practices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Practices.Controllers
{
    /// <summary>
    /// Controlador principal de la aplicación.
    /// Maneja las operaciones CRUD de vehículos y acciones del motor.
    /// 
    /// Patrones aplicados:
    /// - Inyección de Dependencias (recibe IVehicleRepository)
    /// - Factory Method (usa creators para crear vehículos)
    /// 
    /// Principio SOLID: DIP - Depende de abstracciones (IVehicleRepository, Creator)
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        /// <param name="vehicleRepository">Repositorio de vehículos</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public HomeController(IVehicleRepository vehicleRepository, ILogger<HomeController> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        /// <summary>
        /// Página principal que muestra la lista de vehículos
        /// </summary>
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Vehicles = VehicleCollection.Instance.Vehicles;
            string error = Request.Query.ContainsKey("error") ? Request.Query["error"].ToString() : null;
            ViewBag.ErrorMessage = error;

            return View(model);
        }

        /// <summary>
        /// Agrega un vehículo Ford Mustang a la colección.
        /// Usa Factory Method Pattern para delegar la creación al factory específico.
        /// </summary>
        [HttpGet]
        public IActionResult AddMustang()
        {
            var factory = new FordMustangCreator();
            var vehicle = factory.Create();
            _vehicleRepository.AddVehicle(vehicle);
            return Redirect("/");
        }

        /// <summary>
        /// Agrega un vehículo Ford Explorer a la colección.
        /// Usa Factory Method Pattern para delegar la creación al factory específico.
        /// </summary>
        [HttpGet]
        public IActionResult AddExplorer()
        {
            var factory = new FordExplorerCreator();
            var vehicle = factory.Create();
            _vehicleRepository.AddVehicle(vehicle);
            return Redirect("/");
        }

        /// <summary>
        /// Agrega un vehículo Ford Escape a la colección.
        /// REQUERIMIENTO: El negocio solicitó poder agregar Ford Escape desde la interfaz.
        /// 
        /// Patrón aplicado: Factory Method
        /// - No necesitamos saber cómo se construye el Escape
        /// - El Factory se encarga de toda la configuración
        /// - Principio OCP: Agregamos funcionalidad sin modificar código existente
        /// </summary>
        [HttpGet]
        public IActionResult AgregarEscape()
        {
            // Usamos el Factory Method para crear el vehículo
            var factory = new FordEscapeCreator();
            var vehiculo = factory.Create();
            
            // El repositorio se encarga de almacenarlo
            _vehicleRepository.AddVehicle(vehiculo);
            
            return Redirect("/");
        }

        /// <summary>
        /// Enciende el motor de un vehículo específico
        /// </summary>
        /// <param name="id">ID del vehículo</param>
        [HttpGet]
        public IActionResult StartEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StartEngine();
                return Redirect("/");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        /// <summary>
        /// Agrega gasolina a un vehículo específico
        /// </summary>
        /// <param name="id">ID del vehículo</param>
        [HttpGet]
        public IActionResult AddGas(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.AddGas();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        /// <summary>
        /// Detiene el motor de un vehículo específico
        /// </summary>
        /// <param name="id">ID del vehículo</param>
        [HttpGet]
        public IActionResult StopEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StopEngine();
                return Redirect("/");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        /// <summary>
        /// Página de política de privacidad
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Página de error
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
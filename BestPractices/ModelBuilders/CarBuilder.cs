using Best_Practices.Models;
using System;

namespace Best_Practices.ModelBuilders
{
    /// <summary>
    /// Patrón Builder para construir objetos Car de forma fluida.
    /// Por: Juan Cadena
    /// VENTAJAS:
    /// - Código más legible (encadenamiento de métodos)
    /// - Fácil agregar propiedades sin romper código existente (Principio OCP)
    /// - Valores por defecto centralizados
    /// 
    /// Principios SOLID aplicados:
    /// - SRP: Solo se encarga de construir vehículos
    /// - OCP: Abierto para extensión, cerrado para modificación
    /// </summary>
    public class CarBuilder
    {
        #region Propiedades Privadas del Builder
        
        private string _marca = "Ford";
        private string _modelo = "Mustang";
        private string _color = "Rojo";
        private int _año = DateTime.Now.Year;
        
        // Propiedades preparadas para siguiente sprint (20+ propiedades)
        private string _tipoMotor = "I4";
        private int _caballosFuerza = 200;
        private string _transmision = "Automática";
        private int _añosGarantia = 3;
        
        #endregion

        #region Métodos de Configuración Individual

        /// <summary>
        /// Establece la marca del vehículo
        /// </summary>
        /// <param name="marca">Marca del vehículo (ej: Ford, Toyota)</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerMarca(string marca)
        {
            _marca = marca;
            return this;
        }

        /// <summary>
        /// Establece el modelo del vehículo
        /// </summary>
        /// <param name="modelo">Modelo del vehículo (ej: Mustang, Explorer)</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerModelo(string modelo)
        {
            _modelo = modelo;
            return this;
        }

        /// <summary>
        /// Establece el color del vehículo
        /// </summary>
        /// <param name="color">Color del vehículo</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerColor(string color)
        {
            _color = color;
            return this;
        }

        /// <summary>
        /// Establece el año del vehículo
        /// </summary>
        /// <param name="año">Año del vehículo</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerAño(int año)
        {
            _año = año;
            return this;
        }

        /// <summary>
        /// Establece el tipo de motor
        /// </summary>
        /// <param name="tipoMotor">Tipo de motor (ej: V8, V6, I4 Turbo)</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerTipoMotor(string tipoMotor)
        {
            _tipoMotor = tipoMotor;
            return this;
        }

        /// <summary>
        /// Establece los caballos de fuerza del motor
        /// </summary>
        /// <param name="caballos">Cantidad de caballos de fuerza</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerCaballosFuerza(int caballos)
        {
            _caballosFuerza = caballos;
            return this;
        }

        /// <summary>
        /// Establece el tipo de transmisión
        /// </summary>
        /// <param name="transmision">Tipo de transmisión (Automática, Manual)</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerTransmision(string transmision)
        {
            _transmision = transmision;
            return this;
        }

        /// <summary>
        /// Establece los años de garantía
        /// </summary>
        /// <param name="años">Cantidad de años de garantía</param>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder EstablecerAñosGarantia(int años)
        {
            _añosGarantia = años;
            return this;
        }

        #endregion

        #region Métodos de Valores Por Defecto (REQUERIMIENTO CLAVE)

        /// <summary>
        /// Establece el año actual automáticamente.
        /// REQUERIMIENTO: El negocio solicitó que por defecto sea el año actual.
        /// </summary>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder ConAñoActual()
        {
            _año = DateTime.Now.Year;
            return this;
        }

        /// <summary>
        /// Establece la garantía estándar de 3 años
        /// </summary>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder ConGarantiaEstandar()
        {
            _añosGarantia = 3;
            return this;
        }

        /// <summary>
        /// Establece el motor estándar (I4, 200HP)
        /// </summary>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder ConMotorEstandar()
        {
            _tipoMotor = "I4";
            _caballosFuerza = 200;
            return this;
        }

        /// <summary>
        /// Establece transmisión automática por defecto
        /// </summary>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder ConTransmisionAutomatica()
        {
            _transmision = "Automática";
            return this;
        }

        /// <summary>
        /// Método centralizado que aplica TODOS los valores por defecto.
        /// IMPORTANTE: Cuando se agreguen las 20+ propiedades en el próximo sprint,
        /// solo se deben agregar aquí para mantener consistencia.
        /// 
        /// Principio aplicado: DRY (Don't Repeat Yourself)
        /// </summary>
        /// <returns>El builder para encadenar métodos</returns>
        public CarBuilder ConTodosLosDefectos()
        {
            return this
                .ConAñoActual()
                .ConGarantiaEstandar()
                .ConMotorEstandar()
                .ConTransmisionAutomatica();
            
            // Para el próximo sprint, agregar aquí:
            // .ConSistemaSeguridadEstandar()
            // .ConPaqueteMultimediaBasico()
            // .ConMantenimientoProgramado()
            // etc... (20+ propiedades más)
        }

        #endregion

        #region Método de Construcción Final

        /// <summary>
        /// Construye y devuelve el objeto Car con todas las propiedades configuradas
        /// </summary>
        /// <returns>Instancia de Car completamente configurada</returns>
        public Car Construir()
        {
            var carro = new Car(_color, _marca, _modelo);
            
            // Configurar propiedades adicionales
            carro.Año = _año;
            carro.TipoMotor = _tipoMotor;
            carro.CaballosFuerza = _caballosFuerza;
            carro.Transmision = _transmision;
            carro.AñosGarantia = _añosGarantia;
            
            return carro;
        }

        /// <summary>
        /// Alias de Construir() para mantener compatibilidad con código existente
        /// </summary>
        /// <returns>Instancia de Car</returns>
        public Car Build()
        {
            return Construir();
        }

        #endregion
    }
}

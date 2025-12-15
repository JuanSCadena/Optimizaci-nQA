using Best_Practices.ModelBuilders;
using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    /// <summary>
    /// Factory Method concreto para crear vehículos Ford Escape.
    /// 
    /// REQUERIMIENTO: El negocio solicitó agregar el modelo Ford Escape con:
    /// - Color: Rojo
    /// - Marca: Ford
    /// - Modelo: Escape
    /// 
    /// Principios SOLID aplicados:
    /// - OCP: Agregamos nuevo modelo SIN modificar código existente
    /// - SRP: Esta clase solo se encarga de crear Ford Escape
    /// - DIP: Hereda de Creator (abstracción)
    /// </summary>
    public class FordEscapeCreator : Creator
    {
        /// <summary>
        /// Crea una instancia de Ford Escape completamente configurada
        /// </summary>
        /// <returns>Vehículo Ford Escape</returns>
        public override Vehicle Create()
        {
            var builder = new CarBuilder();
            
            return builder
                .EstablecerMarca("Ford")
                .EstablecerModelo("Escape")
                .EstablecerColor("Rojo")
                .EstablecerTipoMotor("I4 Turbo")
                .EstablecerCaballosFuerza(250)
                .ConAñoActual()
                .ConGarantiaEstandar()
                .ConTransmisionAutomatica()
                .Construir();
        }
    }
}
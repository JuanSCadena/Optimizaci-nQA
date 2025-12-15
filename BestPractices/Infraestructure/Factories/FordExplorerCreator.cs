using Best_Practices.ModelBuilders;
using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Practices.Infraestructure.Factories
{
    public class FordExplorerCreator : Creator
    {
         public override Vehicle Create()
        {
            var builder = new CarBuilder();
            return builder
                .EstablecerModelo("Explorer")  
                .EstablecerColor("Negro")      
                .EstablecerTipoMotor("V6")
                .EstablecerCaballosFuerza(300)
                .ConAñoActual()
                .Construir();
        }
    }
}

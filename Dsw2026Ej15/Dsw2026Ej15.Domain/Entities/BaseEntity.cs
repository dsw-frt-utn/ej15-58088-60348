using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity //esta clase funciona como "plantilla". 
                                     //No existen objetos "entidad base", pero sí existen Médicos que tienen características de una entidad como un ID. 
    {

        public Guid Id { get; set; }  //Guid es un tipo de dato que genera un código alfanumperico larguísimo y
                                      //único en el mundo. Es más seguro que usar números simples para identificar registrso en una API.  
    }                                 //get; set; permiten que otras partes del programa lean (get) o asignen (set) un valor al ID. 
}

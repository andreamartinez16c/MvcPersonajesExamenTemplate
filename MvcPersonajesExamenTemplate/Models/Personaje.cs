﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPersonajesExamenTemplate.Models
{
    public class Personaje
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TallerMecanico.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evidencias
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Comentarios { get; set; }
        public byte[] foto { get; set; }
        public Nullable<int> Id_Carro { get; set; }
    
        public virtual Carros Carros { get; set; }
    }
}
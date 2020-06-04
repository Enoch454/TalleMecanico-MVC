using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerMecanico.Models
{
    public class EvidenciasCE
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Comentarios { get; set; }
        public byte[] fotoFinal { get; set; }
        public Nullable<int> Id_Carro { get; set; }
        public HttpPostedFileBase foto { get; set; }

    }
}
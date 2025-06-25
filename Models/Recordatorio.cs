using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    internal class Recordatorio
    {
        public string Texto { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Activo { get; set; }

        //con ayuda de IA para calculo de tiempo faltante para recordatorios
        public int DiasRestantes => (FechaHora - DateTime.Now).Days;
        public int HorasRestantes => (FechaHora - DateTime.Now).Hours;
        public int MinutosRestantes => (FechaHora - DateTime.Now).Minutes;
    }
}

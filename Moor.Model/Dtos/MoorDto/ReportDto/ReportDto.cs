using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto.ReportDto
{
    public class ReportDto
    {
        public DateTime Tarih { get; set; }
        public string AcenteAd { get; set; }
        public decimal Ücret { get; set; }
        public string Nereden { get; set; }
        public string Nereye { get; set; }
        public List<Yolcu> Yolcular { get; set; }

    }

    public class Yolcu
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }

    }
}

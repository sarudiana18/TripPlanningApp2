using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AtractieTuristica
    {
        [Key][DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Nume {get; set;}
        public string Descriere {get;set;}
        public string Adresa {get; set;}
        public int CityId {get; set;}
        public City City {get; set; }
        public List<Photo> Photos { get; set; } = new();
    }
}
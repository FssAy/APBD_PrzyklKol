using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace APBD_PrzyklKol.Models
{
    public class MPrescriptionMedicament
    {
        public int IdMedicament { get; }
        public int Dose { get; }

        [StringLength(100)]
       public string Details { get; }
    }
}

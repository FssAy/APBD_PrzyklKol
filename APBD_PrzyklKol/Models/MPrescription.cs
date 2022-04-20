using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace APBD_PrzyklKol.Models
{
    public class MPrescription
    {
        int IdPrescription { get; set; }

        [DataType(DataType.Date)]
        string Date { get; set; }


        [DataType(DataType.Date)]
        string DueDate { get; set;  }

        int IdPatient { get; set; }

        int IdDoctor { get; set;  }

        internal static MPrescription FromReader(SqlDataReader reader)
        {
            return new MPrescription
            {
                IdPrescription = (int) reader["IdPrescription"],
                Date = reader["Date"].ToString(),
                DueDate = reader["DueDate"].ToString(),
                IdPatient = (int)reader["IdPatient"],
                IdDoctor = (int)reader["IdDoctor"],
            };
        }
    }
}

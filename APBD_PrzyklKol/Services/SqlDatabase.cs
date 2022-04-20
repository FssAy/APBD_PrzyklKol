using APBD_PrzyklKol.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_PrzyklKol.Services
{
    public class SqlDatabase : IDatabaseAsync
    {
        private static async Task<SqlConnection> getConnection()
        {
            var c = new SqlConnection("Data source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s23232;User ID=s23232;Password=");
            await c.OpenAsync();
            return c;
        }

        public async Task<IEnumerable<MPrescription>> getPrescriptionsAsync()
        {
            var prescriptions = new List<MPrescription>();

            using (var c = await getConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prescription");
                cmd.Connection = c;
                var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync()) {
                    prescriptions.Add(MPrescription.FromReader(reader));
                }
            }

            return prescriptions;
        }

        public async Task<IEnumerable<MPrescription>> getPrescriptionsAsync(string lastName)
        {
            var prescriptions = new List<MPrescription>();

            using (var c = await getConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prescription Pr INNER JOIN Patient Pa ON Pa.IdPatient=Pr.IdPatient WHERE Pa.LastName=@lastName");
                cmd.Parameters.AddWithValue("lastName", lastName);
                cmd.Connection = c;
                var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    prescriptions.Add(MPrescription.FromReader(reader));
                }
            }

            return prescriptions;
        }

        public async Task<bool> addMedicamentAsync(IEnumerable<MPrescriptionMedicament> medicaments, int prescriptionId)
        {
            using (var c = await getConnection())
            {
                var transaction = c.BeginTransaction();

                foreach (MPrescriptionMedicament medicament in medicaments)
                {
                    var cmdCheck = new SqlCommand("SELECT '1' FROM Medicament WHERE IdMedicament = @id");
                    cmdCheck.Parameters.AddWithValue("id", medicament.IdMedicament);
                    cmdCheck.Connection = c;
                    cmdCheck.Transaction = transaction;
                    var result = await cmdCheck.ExecuteScalarAsync();

                    if (result != null && (string) result == "1")
                    {
                        var cmdAdd = new SqlCommand("INSERT INTO Prescription_Medicament(IdMedicament, IdPrescription, Dose, Details) VALUES (@idm, @idp, @dose, @det)");
                        cmdCheck.Parameters.AddWithValue("idm", medicament.IdMedicament);
                        cmdCheck.Parameters.AddWithValue("idp", prescriptionId);
                        cmdCheck.Parameters.AddWithValue("dose", medicament.Dose);
                        cmdCheck.Parameters.AddWithValue("det", medicament.Details);
                        cmdAdd.Connection = c;
                        cmdAdd.Transaction = transaction;
                        if (await cmdAdd.ExecuteNonQueryAsync() > 0)
                        {
                            // success
                        } else
                        {
                            // already exists
                        }
                    } else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                transaction.Commit();
            }

            return false;
        }
    }
}

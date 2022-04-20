using APBD_PrzyklKol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_PrzyklKol.Services
{
    public interface IDatabaseAsync
    {
        public async Task<IEnumerable<MPrescription>> getPrescriptionsAsync()
        {
            return null;
        }

        public async Task<IEnumerable<MPrescription>> getPrescriptionsAsync(string lastName)
        {
            return null;
        }

        public async Task<bool> addMedicamentAsync(IEnumerable<MPrescriptionMedicament> medicaments, int prescriptionId)
        {
            return false;
        }
    }
}

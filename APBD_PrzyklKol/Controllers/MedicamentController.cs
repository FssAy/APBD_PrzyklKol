using APBD_PrzyklKol.Models;
using APBD_PrzyklKol.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_PrzyklKol.Controllers
{
    [ApiController]
    [Route("medicament/addToPrescription/{prescriptionId}")]
    public class MedicamentController : Controller
    {
        private readonly IDatabaseAsync db;

        public MedicamentController(IDatabaseAsync db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicament([FromBody] IEnumerable<MPrescriptionMedicament> medicaments, int prescriptionId)
        {
            if (medicaments == null || medicaments.Count() == 0)
            {
                return new StatusCodeResult(400);
            }

            if (await db.addMedicamentAsync(medicaments, prescriptionId))
            {
                return Ok();
            } 

            return new StatusCodeResult(406);
        }
    }
}

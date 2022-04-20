using APBD_PrzyklKol.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_PrzyklKol.Controllers
{
    [ApiController]
    [Route("prescription")]
    public class PrescriptionController : Controller
    {
        private readonly IDatabaseAsync db;

        public PrescriptionController(IDatabaseAsync db)
        {
            this.db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetPrescription([FromQuery] string lastName)
        {
            try
            {
                if (lastName == null || lastName == "")
                {
                    return Ok(await db.getPrescriptionsAsync());
                }

                return Ok(await db.getPrescriptionsAsync(lastName));
            } 
            catch (KeyNotFoundException _)
            {
                return new NotFoundResult();
            }
            catch (Exception _)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}

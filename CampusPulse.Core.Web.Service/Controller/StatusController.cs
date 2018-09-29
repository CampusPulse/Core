
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CampusPulse.Core.Service.Controller
{

    public class StatusController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Services is up and running");
            //throw new Exception("Intentioanlly");
        }
    }
}
﻿
using Microsoft.AspNetCore.Mvc;

namespace CampusPulse.Core.Service.Controller
{

    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok("Services is up and running");
            //throw new Exception("Intentioanlly");
        }
    }
}
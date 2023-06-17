﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arbolus.Model;
using Microsoft.AspNetCore.Http;
using Arbolus.Service.Interface;

namespace Arbolus.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallPrice : ControllerBase
    {
        private readonly ILogger<CallPrice> logger;
        private readonly ICallService callService;

        public CallPrice(ILogger<CallPrice> logger, ICallService callService)
        {
            this.logger = logger;
            this.callService = callService;
        }

        [HttpGet]
        [Route("/api/Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return this.Ok(await this.callService.GetPrices());
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error in Get :- {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while processing data!");
            }
        }

        [HttpGet]
        [Route("/api/GetByName")]
        public async Task<IActionResult> GetByName(string expert, string client)
        {
            try
            {
                if (string.IsNullOrEmpty(expert) || string.IsNullOrEmpty(client))
                    return BadRequest("Please provide either expert or client");

                return this.Ok(await this.callService.GetPriceByName(expert, client));
            }
            catch(Exception ex)
            {
                this.logger.LogError($"Error in GetByName :- {ex} for expert { expert } and client {client}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while processing data!");
            }
        }
    }
}
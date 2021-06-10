using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


using GatewayCommon.EF;
using GatewayCommon.Services;
using GatewayCommon.BusinessObjects;


namespace GatewayRest.Controllers
{
        //[EnableCors("CorsPolicy")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ApiController]
        [Route("/api/sms")]
        public class SMSController : ControllerBase
        {
            private readonly ILogger<SMSController> _logger;
            private readonly SMSService _smsService;


            public SMSController(ILogger<SMSController> logger)
            {
                _logger = logger;
                _smsService = new SMSService();
            }


            //Gets SMS msg            
        [HttpGet("{ID}")]
        public IActionResult GetSMS(int ID)
        {
           var msg = _smsService.GetSMS(ID);
            var rtrn = msg.Msg +";"+ msg.Phone.ToString();
            return Ok(rtrn);
        }

        //[HttpPost("{status}")]
        //public IActionResult PostStatus(string status, int ID)
        //{
        //    _smsService.PostStatus(status, ID);

        //    return Ok();
        //}

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost("{msg}/{phone}")]

        public IActionResult PostSMS(string msg, int phone)
        {
            if (phone.ToString().Length != 9)
            {
                string error = "Given phone number must have 9 characters";
                return BadRequest(error);
            }
            else
            {          
                if (_smsService.CheckSMS() == false)
                {
                    SMS sms = new SMS();
                    sms.Msg = msg;
                    sms.Phone = phone;
                    sms.Sent = "n";

                    _smsService.PostSMS(sms);
                    return Ok();
                }
                else
                {
                    string error = "There is unsent SMS";
                    return BadRequest(error);
                }



                
            }

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        //Mark msg as sent
        [HttpPost("{ID}")]
        public IActionResult PostSentY(int ID)
        {

            _smsService.PostSentY(ID);
                return Ok();
        }


        [HttpGet()]
        public IActionResult GetID()
        {
            int ID = _smsService.GetID();

            return Ok(ID);
        }
        









        }
    }

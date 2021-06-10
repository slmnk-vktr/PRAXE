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
    
    [ApiController]
    [Route("/api/user")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;


        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = new UserService();
        }



        //Adds device into tbUser
        [HttpPost("{deviceInfo}")]
        public IActionResult PostStatus(string deviceInfo)
        {

            bool already;

            using (var context = new MssqlContext())
            {

                var query = from u in context.User
                            where u.DeviceInfo == deviceInfo
                            select u;
                var exists = query.Any<User>();
                if(exists == true)
                {
                    already = true;
                }
                else
                {
                    already = false;
                }
            }

            if(already == false)
            {
                User user = new User();
                user.DeviceInfo = deviceInfo;

                _logger.LogInformation(user.ToString());

                _userService.StoreUser(user);
                return Ok("Registration successful");
            }
            else
            {
                return Ok("User already exists");
            }


          

        }

        //Looks for user with given ID -> if exists return bool true;
        [HttpGet("{id}")]
        public IActionResult SearchUser(int ID)
        {
            var b = _userService.FindUser(ID);
            return Ok(b);
        }


        [HttpDelete("{deviceInfo}")]
        public IActionResult DeleteUser(string deviceInfo)
        {

            bool already;

            using (var context = new MssqlContext())
            {

                var query = from u in context.User
                            where u.DeviceInfo == deviceInfo
                            select u;
                var exists = query.Any<User>();
                if (exists == true)
                {
                    already = true;
                }
                else
                {
                    already = false;
                }
            }

            if (already == true)
            {

                _userService.DeleteUser(deviceInfo);
                return Ok("Successfully logged out");
            }
            else
            {
                return Ok("No such user is registered");
            }
        }


        //  
        [HttpGet("{deviceInfo}")]
        public IActionResult GetDevice(string deviceInfo)
        {

            var b = _userService.GetDevice(deviceInfo);
            return Ok(b);
        }








    }
}

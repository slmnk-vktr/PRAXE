using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using GatewayCommon.EF;
using GatewayCommon.BusinessObjects;

namespace GatewayCommon.Services
{
    public class UserService
    {

        public void StoreUser(User user)
        {

            try
            {
                using (var context = new MssqlContext())
                {
                    context.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }

        }

        public void DeleteUser(string deviceInfo)
        {
            try
            {
                using (var context = new MssqlContext())
                {

                    var query = from u in context.User
                                where u.DeviceInfo == deviceInfo
                                select u;
                    var user = query.Single();
                    context.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }


        }


        public bool FindUser(int ID)
        {
            bool b = false;
            try
            {
                using (var context = new MssqlContext())
                {
                    var query = from u in context.User
                                where u.ID == ID
                                select u;
                    var user = query.Any();

                    if (user == true)
                    {
                        b = true;
                    }
                    else
                    {
                        b = false;
                    }
                    return b;
                }
            }
            catch (Exception e)
            {
                return b;
            }
        }                                                                                                    


        public bool GetDevice(string deviceInfo)
        {
            bool b;
            b = false;
            try
            {
                using (var context = new MssqlContext())
                {
                    var query = from u in context.User
                                where u.DeviceInfo == deviceInfo
                                select u;
                    var user = query.Any();

                    if (user == true)
                    {
                        b = true;
                    }
                    else
                    {
                        b = false;
                    }
                    return b;
                }
            }
            catch (Exception e)
            {
                return b;
            }
        }

    }
}

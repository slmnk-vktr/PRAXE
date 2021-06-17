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
    public class SMSService
    {


        //returns null if error
        public SMS GetSMS(int ID)
        {

            try
            {
                using (var context = new MssqlContext())
                {
                    var query = from u in context.SMS
                                where u.ID == ID     
                                select u;
                    var msg = query.Single();

                    return msg;
                }
            }
            //return msg where message is null -> FE sees this as no msg
            catch (Exception e)
            {
                SMS msg = new SMS();
                msg.Msg = null;

                return msg;

            }



        }


        //Check if msg with Sent set to "n" exists; if exists return true
        public bool CheckSMS()
        {
            bool b;
            using (var context = new MssqlContext())
            {
                var query = from u in context.SMS
                            where u.Sent == "n"
                            select u;
                var msg = query.Any();
                if (msg == false)
                {
                    b = false;
                }
                else
                {
                    b = true;
                }
            }
            return b;
        }

            //Creates a new msg
            public void PostSMS(SMS sms)
        {

            using (var context = new MssqlContext())
            {
                    context.Add(sms);
                    context.SaveChanges();
            }
        }

        //Mark msg as sent
        public bool PostSentY(int ID)
        {
            bool done;
            using (var context = new MssqlContext())
            {
                var query = from u in context.SMS
                            where u.ID == ID
                            select u;
                SMS SMS;
                try
                {
                    SMS = query.Single();
                    SMS.Sent = "y";
                    context.SaveChanges();
                    done = true;
                }
                catch(Exception e)
                {
                    done = false;
                }
                return done;
             
            }
        }

        //Find msg with Sent column set to "n"
        public int GetID()
        {

            try
            {
                using (var context = new MssqlContext())
                {
                    var query = from u in context.SMS
                                where u.Sent == "n"
                                select u;
                    var msg = query.Single();

                    return msg.ID;
                }
            }
            //return msg where message is 0 -> FE sees this as no msg
            catch (Exception e)
            {

                SMS msg = new SMS();
                msg.ID = 0;

                return msg.ID;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GatewayCommon.BusinessObjects
{

    [Table("tbSMS")]
    public class SMS
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Msg")]
        public string Msg { get; set; }

        [Column("Sent")]
        public string Sent { get; set; }

        [Column("Phone")]
        public int Phone { get; set; }


    }
}

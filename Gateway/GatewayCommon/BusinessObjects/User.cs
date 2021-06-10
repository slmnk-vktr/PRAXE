using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GatewayCommon.BusinessObjects
{
    [Table("tbUser")]
    public class User
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("DeviceInfo")]
        public string DeviceInfo { get; set; }

    }
}

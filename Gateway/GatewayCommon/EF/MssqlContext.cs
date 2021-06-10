using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using GatewayCommon.BusinessObjects;


namespace GatewayCommon.EF
    
{
    public class MssqlContext :DbContext
    {
        private readonly string _connectionString;

        public DbSet<SMS> SMS { get; set; }
        public DbSet<User> User { get; set; }

        public MssqlContext()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            _connectionString = config["ConnectionStrings:dbGatewayConnectionString"];
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


    }
}

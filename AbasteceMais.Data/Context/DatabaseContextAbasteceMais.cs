using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;
using AbasteceMais.Domain.Entities;

namespace AbasteceMais.Data.Context
{
    public partial class DatabaseContextAbasteceMais : DbContext
    {
        public DatabaseContextAbasteceMais()
            : base("name=StringConnectionAbasteceMais")
        {
            // Fix for the <No Entity Framework provider found for the ADO.NET provider with invariant name 'System.Data.SqlClient'> error
            // https://stackoverflow.com/questions/18455747/no-entity-framework-provider-found-for-the-ado-net-provider-with-invariant-name
            // https://stackoverflow.com/questions/14033193/entity-framework-provider-type-could-not-be-loaded/19130718#19130718

            var Instance = SqlProviderServices.Instance;
        }

        public virtual DbSet<Registration> Registrations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

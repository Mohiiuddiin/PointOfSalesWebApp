namespace PointOfSalesWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SalesInfoViewContext : DbContext
    {
        //SalesInfoViewContext
        public SalesInfoViewContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<SalesInfoListView> SalesInfoListViews { get; set; }
        public virtual DbSet<SalesInfoView> SalesInfoViews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

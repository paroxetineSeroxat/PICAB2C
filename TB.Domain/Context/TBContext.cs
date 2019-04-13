using TB.Domain.BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.Context
{
    public class TBContext : DbContext
    {
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<SportEvent> SportEvent { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<TransportCompany> TransportCompany { get; set; }
        public DbSet<RoomBooking> RoomBooking { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        

        public TBContext() : base()
        {
            this.Configuration.LazyLoadingEnabled = false;

            this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TBContext>(null);
            base.OnModelCreating(modelBuilder);

        }
    }

}

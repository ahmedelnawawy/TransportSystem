namespace TransportSystems.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TransportModel : DbContext
    {
        public TransportModel()
            : base("name=TransportModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspUser> AspUser { get; set; }
        public virtual DbSet<BankMoved> BankMoved { get; set; }
        public virtual DbSet<CarChangeRateOnDis> CarChangeRateOnDis { get; set; }
        public virtual DbSet<CarMaintenance> CarMaintenance { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<CarType> CarType { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<CommandMessages> CommandMessages { get; set; }
        public virtual DbSet<CommandRequest> CommandRequest { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<DriverLocation> DriverLocation { get; set; }
        public virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<FromRegion> FromRegion { get; set; }
        public virtual DbSet<Indx> Indx { get; set; }
        public virtual DbSet<KhznaMoved> KhznaMoved { get; set; }
        public virtual DbSet<Levels> Levels { get; set; }
        public virtual DbSet<ListPrice> ListPrice { get; set; }
        public virtual DbSet<MainAccount> MainAccount { get; set; }
        public virtual DbSet<privilage> privilage { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<PurchaseInvoice> PurchaseInvoice { get; set; }
        public virtual DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetail { get; set; }
        public virtual DbSet<Rol_PrivFT> Rol_PrivFT { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SalesInvoice> SalesInvoice { get; set; }
        public virtual DbSet<SalesInvoiceDetail> SalesInvoiceDetail { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Solar> Solar { get; set; }
        public virtual DbSet<SubAccount> SubAccount { get; set; }
        public virtual DbSet<SubStore> SubStore { get; set; }
        public virtual DbSet<TrafficDepartment> TrafficDepartment { get; set; }
        public virtual DbSet<TransferProductType> TransferProductType { get; set; }
        public virtual DbSet<TransportCommand> TransportCommand { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Driver)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.CarMaintenance)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.CarType)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.City)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Colors)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.MainAccount)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Entry)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.FromRegion)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Indx)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.KhznaMoved)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Levels)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.PurchaseInvoice)
                .WithRequired(e => e.AspUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Rol_PrivFT)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Role1)
                .WithOptional(e => e.AspUser1)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.SalesInvoice)
                .WithRequired(e => e.AspUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Sector)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.Services)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.SubAccount)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.SubStore)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.TrafficDepartment)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<AspUser>()
                .HasMany(e => e.TransportCommand)
                .WithOptional(e => e.AspUser)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<BankMoved>()
                .Property(e => e.Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Cars>()
                .HasMany(e => e.CarChangeRateOnDis)
                .WithRequired(e => e.Cars)
                .HasForeignKey(e => e.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cars>()
                .HasMany(e => e.CarMaintenance)
                .WithRequired(e => e.Cars)
                .HasForeignKey(e => e.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cars>()
                .HasMany(e => e.SalesInvoice)
                .WithRequired(e => e.Cars)
                .HasForeignKey(e => e.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cars>()
                .HasMany(e => e.Solar)
                .WithOptional(e => e.Cars)
                .HasForeignKey(e => e.CarID);

            modelBuilder.Entity<Cars>()
                .HasMany(e => e.TransportCommand)
                .WithRequired(e => e.Cars)
                .HasForeignKey(e => e.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colors>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.Colors)
                .HasForeignKey(e => e.ColorId);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.DriverLocation)
                .WithRequired(e => e.Driver)
                .HasForeignKey(e => e.driver_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<FromRegion>()
                .HasMany(e => e.ListPrice)
                .WithOptional(e => e.FromRegion)
                .HasForeignKey(e => e.RegionFromId);

            modelBuilder.Entity<FromRegion>()
                .HasMany(e => e.ListPrice1)
                .WithOptional(e => e.FromRegion1)
                .HasForeignKey(e => e.RegionToId);

            modelBuilder.Entity<FromRegion>()
                .HasMany(e => e.TransportCommand)
                .WithRequired(e => e.FromRegion)
                .HasForeignKey(e => e.FromRegionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FromRegion>()
                .HasMany(e => e.TransportCommand1)
                .WithRequired(e => e.FromRegion1)
                .HasForeignKey(e => e.ToRegionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhznaMoved>()
                .Property(e => e.Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MainAccount>()
                .HasMany(e => e.SubAccount)
                .WithOptional(e => e.MainAccount)
                .HasForeignKey(e => e.MainAccount_id);

            modelBuilder.Entity<privilage>()
                .HasMany(e => e.Rol_PrivFT)
                .WithOptional(e => e.privilage)
                .HasForeignKey(e => e.Priv_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .Property(e => e.PPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.SPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.PurchaseInvoiceDetail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.SalesInvoiceDetail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.TransportCommand)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasMany(e => e.PurchaseInvoiceDetail)
                .WithRequired(e => e.PurchaseInvoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.AspUser)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Rol_PrivFT)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.Rol_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SalesInvoice>()
                .HasMany(e => e.SalesInvoiceDetail)
                .WithRequired(e => e.SalesInvoice)
                .HasForeignKey(e => e.PurchaseInvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.CarChangeRateOnDis)
                .WithRequired(e => e.Services)
                .HasForeignKey(e => e.ServiceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.CarMaintenance)
                .WithRequired(e => e.Services)
                .HasForeignKey(e => e.ServiceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.SalesInvoice)
                .WithRequired(e => e.Services)
                .HasForeignKey(e => e.ServiceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.Solar)
                .WithOptional(e => e.Services)
                .HasForeignKey(e => e.ServiceID);

            modelBuilder.Entity<Solar>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SubAccount>()
                .Property(e => e.ABalance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SubAccount>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.SubAccount)
                .HasForeignKey(e => e.SubAccId);

            modelBuilder.Entity<SubAccount>()
                .HasMany(e => e.ListPrice)
                .WithOptional(e => e.SubAccount)
                .HasForeignKey(e => e.SubAccId);

            modelBuilder.Entity<SubAccount>()
                .HasMany(e => e.PurchaseInvoice)
                .WithRequired(e => e.SubAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubAccount>()
                .HasMany(e => e.TransportCommand)
                .WithRequired(e => e.SubAccount)
                .HasForeignKey(e => e.SubAccClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubAccount>()
                .HasMany(e => e.TransportCommand1)
                .WithRequired(e => e.SubAccount1)
                .HasForeignKey(e => e.SubAccVendorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubStore>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.SubStore)
                .HasForeignKey(e => e.StorID);

            modelBuilder.Entity<TrafficDepartment>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.TrafficDepartment)
                .HasForeignKey(e => e.TrafficDepID);

            modelBuilder.Entity<TrafficDepartment>()
                .HasMany(e => e.Driver)
                .WithOptional(e => e.TrafficDepartment)
                .HasForeignKey(e => e.TrafficDepID);

            modelBuilder.Entity<TransferProductType>()
                .HasMany(e => e.ListPrice)
                .WithOptional(e => e.TransferProductType)
                .HasForeignKey(e => e.ProductType);

            modelBuilder.Entity<TransportCommand>()
                .HasMany(e => e.DriverLocation)
                .WithRequired(e => e.TransportCommand)
                .HasForeignKey(e => e.command_id)
                .WillCascadeOnDelete(false);
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farming.WpfClient.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FarmingEntities : DbContext
    {
        public FarmingEntities()
            : base("name=FarmingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bull> Bulls { get; set; }
        public virtual DbSet<BloodType> BloodTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<logi> logi { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Priplod> Priplods { get; set; }
        public virtual DbSet<Productivity> Productivities { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Retirement> Retirements { get; set; }
        public virtual DbSet<MethodSluchki> MethodsSluchki { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cow> Cows { get; set; }
        public virtual DbSet<Reproduction> Reproductions { get; set; }
    }
}

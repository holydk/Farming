//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Cow
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cow()
        {
            this.Childrens = new HashSet<Cow>();
            this.Priplods = new HashSet<Priplod>();
            this.Productivities = new HashSet<Productivity>();
            this.Retirements = new HashSet<Retirement>();
            this.Reproductions = new HashSet<Reproduction>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> BreedId { get; set; }
        public double Porodnost { get; set; }
        public Nullable<int> LineId { get; set; }
        public Nullable<int> FamilyId { get; set; }
        public System.DateTime BDay { get; set; }
        public string BPlace { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> BloodTypeId { get; set; }
        public Nullable<int> MotherId { get; set; }
        public Nullable<int> FatherId { get; set; }
        public bool IsInHerd { get; set; }
    
        public virtual Bull Father { get; set; }
        public virtual BloodType BloodType { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cow> Childrens { get; set; }
        public virtual Cow Mother { get; set; }
        public virtual Line Line { get; set; }
        public virtual Breed Breed { get; set; }
        public virtual Family Family { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Priplod> Priplods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Productivity> Productivities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Retirement> Retirements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reproduction> Reproductions { get; set; }
    }
}

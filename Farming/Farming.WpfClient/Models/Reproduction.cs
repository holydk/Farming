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
    
    public partial class Reproduction
    {
        public int Id { get; set; }
        public Nullable<int> CowId { get; set; }
        public Nullable<int> MethodSluchkiId { get; set; }
        public System.DateTime DateOsemeneniya { get; set; }
        public int ChisloSuhihDney { get; set; }
        public Nullable<int> BullId { get; set; }
        public int SorPeriod { get; set; }
        public System.DateTime DateOtela { get; set; }
    
        public virtual Bull Bull { get; set; }
        public virtual Cow Cow { get; set; }
        public virtual MethodSluchki MethodSluchki { get; set; }
    }
}

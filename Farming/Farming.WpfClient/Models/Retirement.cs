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
    
    public partial class Retirement
    {
        public int Id { get; set; }
        public int CowId { get; set; }
        public System.DateTime Date { get; set; }
        public string Reason { get; set; }
    
        public virtual Cow Cow { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TCK_Off_Fac.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Master_Item
    {
        public int ID { get; set; }
        public string Item_No { get; set; }
        public string Item_Name { get; set; }
        public string Item_Des { get; set; }
        public Nullable<System.DateTime> Item_Create { get; set; }
        public Nullable<System.DateTime> Item_Update { get; set; }
        public string Barcode { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public string Item_Img { get; set; }
        public string Type { get; set; }
        public string Site { get; set; }
    }
}

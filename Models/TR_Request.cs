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
    
    public partial class TR_Request
    {
        public int ID { get; set; }
        public string Document_No { get; set; }
        public string Doc_Prefix { get; set; }
        public Nullable<int> Qty_Request { get; set; }
        public Nullable<System.DateTime> Doc_Create { get; set; }
        public Nullable<System.DateTime> Doc_Update { get; set; }
        public string Doc_type { get; set; }
        public string Doc_Status { get; set; }
        public string Barcode { get; set; }
        public Nullable<System.DateTime> RQ_Date_Request { get; set; }
        public string RQ_Doc_Ref { get; set; }
        public string RQ_User { get; set; }
        public string RQ_Customer { get; set; }
    }
}

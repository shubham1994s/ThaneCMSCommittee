//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SwachBharat.CMS.Dal.DataContexts
{
    using System;
    
    public partial class SP_GetHSHouseDetails1_Result
    {
        public Nullable<int> userId { get; set; }
        public int houseId { get; set; }
        public string houseLat { get; set; }
        public string houseLong { get; set; }
        public string ReferanceId { get; set; }
        public Nullable<bool> QRStatus { get; set; }
        public Nullable<System.DateTime> QRStatusDate { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public string qrEmpName { get; set; }
        public string QRCodeImage { get; set; }
        public Nullable<int> FilterTotalCount { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Epicode.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotificatonType
    {
        public NotificatonType()
        {
            this.Notification = new HashSet<Notification>();
        }
    
        public short NotificationTypeID { get; set; }
        public string NotificationTypeName { get; set; }
        public bool IsEnabled { get; set; }
    
        public virtual ICollection<Notification> Notification { get; set; }
    }
}
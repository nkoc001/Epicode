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
    
    public partial class NotificationSource
    {
        public NotificationSource()
        {
            this.Notification = new HashSet<Notification>();
        }
    
        public short NotificationSourceID { get; set; }
        public string NotificationSourceName { get; set; }
        public string NotificationSourceSenderAddress { get; set; }
    
        public virtual ICollection<Notification> Notification { get; set; }
    }
}

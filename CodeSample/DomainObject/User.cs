using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using GreenSign.DomainObject.Warehouse;
using MVCCore;

namespace GreenSign.DomainObject
{
    [Flags]
    public enum UserStatus
    {
        [Description("Actived")]
        IsPublish = 1,
        [Description("Deleted")]
        IsDeleted = -1,
        [Description("UnActived")]
        IsDisable = 0
    }

    [Flags]
    public enum UserRole
    {
        [Description("P/O No Manager")]
        PoNoManager = 14,
        [Description("WH Producer")]
        WHProducer = 13,
        [Description("WH Operator")]
        WHOperator = 12,
        [Description("WH Admin")]
        WHAdmin = 11,
        [Description("Watcher")]
        Watcher = 10,
        [Description("Plan Manager")]
        PlanManager = 9,
        [Description("Planner")]
        Planner = 8,
        [Description("Super Merchandiser")]
        SuperMechandiser = 6,
        [Description("Brand Master")]
        BrandMaster = 4,
        [Description("Merchandiser")]
        Mechandiser = 2,
        [Description("Super Admin")]
        Admin = 1
    }

    public class User : BaseEntity, IPrincipal
    {
        private ICollection<Vendor> _vendors;
        public User(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public User()
        {
           
        }

        public IIdentity Identity { get; private set; }

        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string YM { get; set; }
        public string Skype { get; set; }
        public string Email { get; set; }
        //public int? MechandiserId { get; set; }
        public int? BrandNameId { get; set; }
        public UserStatus Status { get; set; }
        public UserRole UserRole { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime LastLoginDateUtc { get; set; }
        public string RegId { get; set; }// gcm registration id
        public bool Deleted { get; set; }

        //public int? WarehouseId { get; set; }
        public virtual BrandName BrandName { get; set; }
        //public virtual Vendor Warehouse { get; set; }
        public virtual ICollection<RelationToBrandMaster> RelationToBrandMaster { get; set; }
        public virtual ICollection<RelationToBrandMaster> RelationToUser { get; set; }
        public virtual ICollection<Order> BrandMasterOrders { get; set; }
        public virtual ICollection<Order> MechandiserOrders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<Messages> MessagesesTo { get; set; }
        public virtual ICollection<Messages> MessagesesFrom { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<OrderWh> OrderWhs { get; set; }

        public virtual ICollection<Vendor> Warehouses
        {
            get { return _vendors ?? (_vendors = new List<Vendor>()); }
            protected set { _vendors = value; }
        }

        public bool IsInRole(string roles)
        {
            return roles.IndexOf(UserRole.ToString(), System.StringComparison.Ordinal) > -1;
        }
    }
}


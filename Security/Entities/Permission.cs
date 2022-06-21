using System.ComponentModel.DataAnnotations;

namespace Security.Entities
{
    public enum Permission
    {
        // [Display(GroupName = "Color", Name = "Read", Description = "Can read colors")]
        // ColorRead = 0x10,
        // [Display(GroupName = "Color", Name = "Create", Description = "Can create a color entry")]
        // ColorCreate = 0x11,
        // [Display(GroupName = "Color", Name = "Update", Description = "Can update a color entry")]
        // ColorUpdate = 0x12,
        // [Display(GroupName = "Color", Name = "Delete", Description = "Can delete a color entry")]
        // ColorDelete = 0x13,

        // [Display(GroupName = "UserAdmin", Name = "Read users", Description = "Can list User")]
        // UserRead = 0x20,
        // //This is an example of grouping multiple actions under one permission
        // [Display(GroupName = "UserAdmin", Name = "Alter user", Description = "Can do anything to the User")]
        // UserChange = 0x21,
        
        [Display(GroupName = "UserAdmin", Name = "View weather", Description = "Can view the weather")]
        ViewWeather = 0x21,
    }
}
using Microsoft.AspNet.Identity.EntityFramework;


namespace MenuTakip.Kullanici
{
    public class AppRole:IdentityRole
    {
           public string Description { get; set; }

        public AppRole()
        {

        }

        public AppRole(string roleName, string description)
            : base(roleName)
          
        {
            this.Description = description;
        }

    }
}
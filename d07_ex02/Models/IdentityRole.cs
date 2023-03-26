using System.ComponentModel;

namespace d07_ex02.Models
{
    public class IdentityRole
    {
        public IdentityRole()
        {
        }

        [Description("Name")]
        [DefaultValue("No name")]
        public string Name { get; set; }

        [Description("Description")]
        [DefaultValue("Empty description")]
        public string Description { get; set; }

        public override string ToString()
           => $"{Name}, {Description}";
    }
}

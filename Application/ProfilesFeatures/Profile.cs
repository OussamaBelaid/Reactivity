using Domain;
using System.Collections.Generic;

namespace Application.ProfilesFeatures
{
    public class Profile
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}

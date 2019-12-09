using Application.ProfilesFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProfileReader
    {
        Task<Profile> ReadProfile(string username);
    }
}

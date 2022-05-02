using FoA.Shared.Personalizers;
using FoA.Shared.Personalizers.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoA.Shared.Users
{
    internal class FoAUser
    {
        internal FoAUser(ulong UserId)
        {
            this.UserId = UserId;
            this.Settings = new();
            
        }

        internal FoAUser(ulong UserId, FoAUserSettings Settings)
        {
            this.UserId = UserId;
            this.Settings = Settings;
        }

        internal ulong UserId { get; }

        internal FoAUserSettings Settings { get; }
    }
}

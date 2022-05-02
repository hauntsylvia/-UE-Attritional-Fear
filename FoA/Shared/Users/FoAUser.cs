using FoA.Shared.Personalizers;
using FoA.Shared.Personalizers.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoA.Shared.Users
{
    public class FoAUser
    {
        public FoAUser(ulong UserId)
        {
            this.UserId = UserId;
            this.Settings = new();
            
        }

        public FoAUser(ulong UserId, FoAUserSettings Settings)
        {
            this.UserId = UserId;
            this.Settings = Settings;
        }

        public ulong UserId { get; }

        public FoAUserSettings Settings { get; }
    }
}

using FoA.Shared.Personalizers;
using FoA.Shared.Personalizers.Specific;
using FoA.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace FoA.Client.Controllers.Base
{
    public class Controller<T> : Actor where T : class, IPersonalizer, new()
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Controller(string Name, FoAUserSettings Settings) : base(Name, null)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.UserSettingsReloaded(Settings);
        }
        public T Settings { get; set; }

        void UserSettingsReloaded(FoAUserSettings Settings)
        {
            this.Settings = Settings.GetSettingOfType<T>();
        }
    }
}

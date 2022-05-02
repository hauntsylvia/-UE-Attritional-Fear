using FoA.Shared.Personalizers.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoA.Shared.Personalizers
{
    internal class FoAUserSettings
    {
        internal FoAUserSettings(IPersonalizer[]? Personalizers = null)
        {
            List<IPersonalizer> AllPersonalizers = new();
            foreach(Type T in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(!T.IsInterface && typeof(IPersonalizer).IsAssignableFrom(T))
                {
                    object? Constructed = Activator.CreateInstance(T);
                    if(Constructed != null)
                    {
                        if (Constructed is IPersonalizer P)
                        {
                            AllPersonalizers.Add(P);
                        }
                    }
                }
            }
            this.Personalizers = Personalizers ?? AllPersonalizers.ToArray();
        }

        IPersonalizer[] Personalizers { get; }
    }
}

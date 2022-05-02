using FoA.Shared.Personalizers.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoA.Shared.Personalizers
{
    public class FoAUserSettings
    {
        public FoAUserSettings(IPersonalizer[]? Personalizers = null)
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

        public T GetSettingOfType<T>() where T : class, IPersonalizer, new()
        {
            foreach(IPersonalizer P in this.Personalizers)
            {
                if(P.GetType() == typeof(T))
                {
                    if(P is T PT)
                    {
                        return PT;
                    }
                }
            }
            return Activator.CreateInstance(typeof(T)) as T ?? throw new Exception($"No settings of type {typeof(T).Name}");
        }

        IPersonalizer[] Personalizers { get; }
    }
}

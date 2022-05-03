using FoA.Client.Controllers.FoACamera;
using System;
using System.Drawing;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace FoA
{
    public class Main
	{ // Indicates the main entry point for automatic loading by the plugin
		public static FoACamera? Camera { get; set; }
		public static void OnWorldBegin()
		{
		}

		public static void OnWorldPostBegin()
		{
			Camera = new(new());
		}

		public static void OnWorldEnd() => Debug.AddOnScreenMessage(-1, 10.0f, Color.DeepPink, "UE world end.");

		public static void OnWorldPrePhysicsTick(float DeltaTime)
		{
			if(Camera != null)
            {
				Camera.Tick(DeltaTime);
            }
		}

		public static void OnWorldDuringPhysicsTick(float deltaTime)
        {
		}

		public static void OnWorldPostPhysicsTick(float deltaTime)
        {
		}

		public static void OnWorldPostUpdateTick(float deltaTime)
        {
			
		}
	}
}
using FoA.Client.Camera;
using System;
using System.Drawing;
using UnrealEngine.Framework;

namespace FoA
{
	public class Main
	{ // Indicates the main entry point for automatic loading by the plugin
		public static FoACamera? Camera { get; set; }
		public static void OnWorldBegin()
		{
			Debug.AddOnScreenMessage(-1, 10.0f, Color.DeepPink, "UE world begin.");
			Camera = new();
			Debug.AddOnScreenMessage(-1, 10.0f, Color.DeepPink, "UE world begin.");
		}

		public static void OnWorldPostBegin() => Debug.AddOnScreenMessage(-1, 10.0f, Color.DeepPink, "How's it going?");

		public static void OnWorldEnd() => Debug.AddOnScreenMessage(-1, 10.0f, Color.DeepPink, "UE world end.");

		public static void OnWorldPrePhysicsTick(float deltaTime)
		{
			if(Camera != null)
            {
				Camera.Tick(deltaTime);
            }
		}

		public static void OnWorldDuringPhysicsTick(float deltaTime) => Debug.AddOnScreenMessage(2, 10.0f, Color.DeepPink, "On during physics tick invoked!");

		public static void OnWorldPostPhysicsTick(float deltaTime) => Debug.AddOnScreenMessage(3, 10.0f, Color.DeepPink, "On post physics tick invoked!");

		public static void OnWorldPostUpdateTick(float deltaTime) => Debug.AddOnScreenMessage(4, 10.0f, Color.DeepPink, "On post update tick invoked!");
	}
}
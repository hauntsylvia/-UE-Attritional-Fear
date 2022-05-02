using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace FoA.Client.Camera
{
    public class FoACamera
    {
        public FoACamera()
        {
        }

        internal PlayerController? Controller => World.GetFirstPlayerController();

        public void Tick(float DeltaTime)
        {
            if(this.Controller != null)
            {
                PlayerInput? Inp = this.Controller.GetPlayerInput();
                if(Inp != null)
                {
                    if(Inp.IsKeyPressed("W"))
                    {
                        Debug.AddOnScreenMessage(-1, 10.0f, Color.DeepPink, "Key pressed!");
                    }
                    this.Controller.SetViewTargetWithBlend(this.Controller, 0.25f * DeltaTime);
                }
            }
        }
    }
}

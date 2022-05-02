using FoA.Client.Controllers.Base;
using FoA.Shared.Personalizers;
using FoA.Shared.Personalizers.Specific;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace FoA.Client.Controllers.FoACamera
{
    public class FoACamera : Controller<CameraSettings>
    {
        public FoACamera(FoAUserSettings Settings) : base("FoACamera", Settings)
        {
            this.Camera = World.GetActor<Camera>();
        }

        public static PlayerController? PlayerController => World.GetFirstPlayerController();
        public Camera? Camera { get; }

        public float ForwardVelocity = 0f;
        public float RightVelocity = 0f;
        public float RightRotationVelocity = 0f;
        public float InwardVelocity = 0f;
        public float ImmediateInwardVelocity = 0f;

        public float CameraYAngle = 0f;

        public bool CanHaveVelocity { get; private set; } = true;

        public void DisableVelocity()
        {
            this.CanHaveVelocity = false;
        }

        public void EnableVelocity()
        {
            this.CanHaveVelocity = true;
        }

        public void Tick(float DeltaTime)
        {
            if (this.CanHaveVelocity && PlayerController != null)
            {
                SceneComponent? Scene = this.GetRootComponent<SceneComponent>();
                PlayerInput? Inp = PlayerController.GetPlayerInput();
                if (Inp != null && Scene != null)
                {
                    this.ProcessMove(PlayerController, Scene, Inp, DeltaTime);
                }
            }
        }

        private void ProcessMove(PlayerController Controller, SceneComponent Scene, PlayerInput Inp, float DeltaTime)
        {
            bool MoveForw =     Inp.IsKeyPressed(this.Settings.MoveForward);
            bool MoveBack =     Inp.IsKeyPressed(this.Settings.MoveBackwards);
            bool MoveRight =    Inp.IsKeyPressed(this.Settings.MoveRight);
            bool MoveLeft =     Inp.IsKeyPressed(this.Settings.MoveLeft);
            bool RotateRight =  Inp.IsKeyPressed(this.Settings.RotateRight);
            bool RotateLeft =   Inp.IsKeyPressed(this.Settings.RotateLeft);

            this.ForwardVelocity =          (MoveForw && !MoveBack ? 1 : !MoveForw && MoveBack ? -1 : 0);
            this.RightVelocity =            (MoveRight && !MoveLeft ? 1 : !MoveRight && MoveLeft ? -1 : 0);
            this.RightRotationVelocity =    (RotateRight && !RotateLeft ? 1 : !RotateRight && RotateLeft ? -1 : 0);

            this.CameraYAngle += this.RightRotationVelocity;

            Transform CameraTransform = Scene.GetTransform();
            Hit H = new();
            bool HitSomething = World.LineTraceSingleByChannel(CameraTransform.Location, Scene.GetForwardVector() * 300, CollisionChannel.Visibility, ref H, false, this.Camera);
            float CurrentMin = (HitSomething ? H.ImpactLocation.Y + this.Settings.MinimumZoom : this.Settings.MinimumZoom);
            float CurrentMax = CurrentMin + this.Settings.MaximumZoom;
            if(CameraTransform.Location.Y > CurrentMax)
            {
                this.InwardVelocity = -0.5f;
            }
            else if(CameraTransform.Location.Y < CurrentMin)
            {
                this.InwardVelocity = 0.5f;
            }

            this.ImmediateInwardVelocity = this.InwardVelocity;

            float CameraZoomVisualTotal = this.InwardVelocity * this.Settings.CameraZoomSteps;
            Scene.AddLocalRotation(Quaternion.CreateFromYawPitchRoll(0, -this.CameraYAngle, 0));
            Vector3 NewPos = ((Scene.GetForwardVector() * this.ForwardVelocity) + (Scene.GetRightVector() * this.RightVelocity)) * CameraTransform.Location * Scene.GetForwardVector() * CameraZoomVisualTotal;
            Scene.SetWorldLocation(NewPos);
            Debug.AddOnScreenMessage(-1, 0, Color.AntiqueWhite, NewPos.ToString());
            Controller.SetViewTargetWithBlend(Controller, 0.25f * DeltaTime);
        }
    }
}

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
    public class FoACamera : FoAController<CameraSettings>
    {
        public FoACamera(FoAUserSettings Settings) : base(Settings)
        {
            this.PlayerInput = PlayerController?.GetPlayerInput() ?? throw new NullReferenceException("B");
            this.Pawn = PlayerController?.GetPawn() ?? throw new NullReferenceException("C");
            Debug.AddOnScreenMessage(-1, 200, Color.Aqua, this.Pawn.Name);
            this.SpringArm = this.Pawn.GetComponent<SpringArmComponent>() ?? throw new NullReferenceException("D");
            this.Camera = this.Pawn.GetComponent<CameraComponent>() ?? throw new NullReferenceException("E");
            Debug.AddOnScreenMessage(-1, 200, Color.Aqua, this.Camera.Name);
        }

        public static PlayerController PlayerController => World.GetFirstPlayerController();
        public CameraComponent Camera { get; set; }
        public SpringArmComponent SpringArm { get; set; }
        public PlayerInput PlayerInput { get; set; }
        private Pawn Pawn { get; set; }

        public float ForwardVelocity = 0f;
        public float RightVelocity = 0f;
        public float RightRotationVelocity = 0f;
        public float InwardVelocity = 0f;
        public float ImmediateInwardVelocity = 0f;

        public float CameraYAngle = 70f;

        public Vector3 ReqPos = Vector3.Zero;

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
            Debug.AddOnScreenMessage(0, 10, Color.RebeccaPurple, "AAA");
            if (this.CanHaveVelocity && PlayerController != null)
            {
                if (this.PlayerInput != null && this.SpringArm != null)
                {
                    this.ProcessMove(DeltaTime);
                }
            }
        }

        private void ProcessMove(float DeltaTime)
        {
            bool MoveForw = this.PlayerInput.IsKeyPressed(this.Settings.MoveForward);
            bool MoveBack = this.PlayerInput.IsKeyPressed(this.Settings.MoveBackwards);
            bool MoveRight = this.PlayerInput.IsKeyPressed(this.Settings.MoveRight);
            bool MoveLeft = this.PlayerInput.IsKeyPressed(this.Settings.MoveLeft);
            bool RotateRight = this.PlayerInput.IsKeyPressed(this.Settings.RotateRight);
            bool RotateLeft = this.PlayerInput.IsKeyPressed(this.Settings.RotateLeft);

            this.ForwardVelocity =          (MoveForw && !MoveBack ? 1 : !MoveForw && MoveBack ? -1 : 0);
            this.RightVelocity =            (MoveRight && !MoveLeft ? 1 : !MoveRight && MoveLeft ? -1 : 0);
            this.RightRotationVelocity =    (RotateRight && !RotateLeft ? 1 : !RotateRight && RotateLeft ? -1 : 0);

            this.CameraYAngle += this.RightRotationVelocity;

            Hit H = new();
            bool HitSomething = World.LineTraceSingleByChannel(this.SpringArm.GetLocation(), this.SpringArm.GetForwardVector() * 300, CollisionChannel.Visibility, ref H, false, this.Pawn);
            float CurrentMin = (HitSomething ? H.ImpactLocation.Y + this.Settings.MinimumZoom : this.Settings.MinimumZoom);
            float CurrentMax = CurrentMin + this.Settings.MaximumZoom;

            if(this.SpringArm.GetLocation().Y > CurrentMax)
            {
                this.InwardVelocity = -0.5f;
            }
            else if(this.SpringArm.GetLocation().Y < CurrentMin)
            {
                this.InwardVelocity = 0.5f;
            }

            this.ImmediateInwardVelocity = this.InwardVelocity;

            float CameraZoomVisualTotal = this.InwardVelocity * this.Settings.CameraZoomSteps;
            Vector3 ForV = this.SpringArm.GetForwardVector() * this.ForwardVelocity;
            Vector3 RightV = this.SpringArm.GetRightVector() * this.RightVelocity;
            Vector3 NewPos = (ForV + RightV) * this.Settings.CameraSpeed * DeltaTime;
            this.SpringArm.AddLocalOffset(NewPos);
            Debug.AddOnScreenMessage(-1, 2, Color.RebeccaPurple, this.SpringArm.GetLocation().ToString());
            this.InwardVelocity = this.InwardVelocity > -5f ? this.InwardVelocity - (this.InwardVelocity * 12.5f) : 0f;
            this.ImmediateInwardVelocity = 0;
        }
    }
}

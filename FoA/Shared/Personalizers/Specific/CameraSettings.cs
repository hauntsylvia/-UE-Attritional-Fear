using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace FoA.Shared.Personalizers.Specific
{
    public class CameraSettings : IPersonalizer
    {
        public CameraSettings()
        {

        }
        public CameraSettings(string MoveForward = Keys.W,
                               string MoveBackwards = Keys.S,
                               string MoveLeft = Keys.A,
                               string MoveRight = Keys.D,
                               string MoveInwards = Keys.I,
                               string MoveOutwards = Keys.O,
                               string RotateRight = Keys.E,
                               string RotateLeft = Keys.Q,
                               float MinimumZoom = 50f,
                               float MaximumZoom = 300f,
                               float CameraSpeed = 750f,
                               float CameraZoomSteps = 80f)
        {
            this.MoveForward = MoveForward;
            this.MoveBackwards = MoveBackwards;
            this.MoveLeft = MoveLeft;
            this.MoveRight = MoveRight;
            this.MoveInwards = MoveInwards;
            this.MoveOutwards = MoveOutwards;
            this.RotateRight = RotateRight;
            this.RotateLeft = RotateLeft;
            this.MinimumZoom = MinimumZoom;
            this.MaximumZoom = MaximumZoom;
            this.CameraSpeed = CameraSpeed;
            this.CameraZoomSteps = CameraZoomSteps;
        }

        public string MoveForward { get; set; } = Keys.W;
        public string MoveBackwards { get; set; } = Keys.S;
        public string MoveLeft { get; set; } = Keys.A;
        public string MoveRight { get; set; } = Keys.D;
        public string MoveInwards { get; set; } = Keys.I;
        public string MoveOutwards { get; set; } = Keys.O;
        public string RotateRight { get; set; } = Keys.E;
        public string RotateLeft { get; set; } = Keys.Q;
        public float MinimumZoom { get; set; } = 50f;
        public float MaximumZoom { get; set; } = 300f;
        public float CameraSpeed { get; set; } = 750f;
        public float CameraZoomSteps { get; set; } = 80f;
    }
}

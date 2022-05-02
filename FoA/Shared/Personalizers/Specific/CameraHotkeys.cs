using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Framework;

namespace FoA.Shared.Personalizers.Specific
{
    internal class CameraHotkeys : IPersonalizer
    {
        internal CameraHotkeys(string MoveForward = Keys.W,
                               string MoveBackwards = Keys.S,
                               string MoveLeft = Keys.A,
                               string MoveRight = Keys.D,
                               string MoveInwards = Keys.I,
                               string MoveOutwards = Keys.O,
                               string RotateRight = Keys.E,
                               string RotateLeft = Keys.Q)
        {
            this.MoveForward = MoveForward;
            this.MoveBackwards = MoveBackwards;
            this.MoveLeft = MoveLeft;
            this.MoveRight = MoveRight;
            this.MoveInwards = MoveInwards;
            this.MoveOutwards = MoveOutwards;
            this.RotateRight = RotateRight;
            this.RotateLeft = RotateLeft;
        }

        public string MoveForward { get; }
        public string MoveBackwards { get; }
        public string MoveLeft { get; }
        public string MoveRight { get; }
        public string MoveInwards { get; }
        public string MoveOutwards { get; }
        public string RotateRight { get; }
        public string RotateLeft { get; }
    }
}

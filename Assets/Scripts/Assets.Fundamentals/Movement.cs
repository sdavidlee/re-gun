using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Time;
using static UnityEngine.Input;
using Assets.Scripts.Extensions;

namespace Assets.Fundamentals
{
    public class Movement
    {
        public float MaxSpeed { get; }
        public float Acceleration { get; }
        public float CurrentSpeed { get; set; }
        public Vector3 Direction { get; set; } 
        public Vector3 MovementVector { get { return Direction.normalized * CurrentSpeed; } }
        public bool CanMove { get; set; }
        public bool CanRotate { get; set; } = true;

        public Movement(float maxSpeed, float acceleration)
        {
            this.MaxSpeed = maxSpeed;
            this.Acceleration = acceleration;
            this.CanMove = true;
        }

        //for animation event only
        public void SetCanMove(bool canmove)
        {
            this.CanMove = canmove;
        }
    }
}

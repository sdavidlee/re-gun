using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.RootPathfinding;

namespace Assets.EnemyAI
{
    public abstract class EnemyVariousActions : MonoBehaviour
    {
        protected Enemy Self { get; set; }

        public abstract void Behavior();

        protected RootPathFinding PathFinder { get; set; }
        protected float nextUpdate { get; set; } = 0;
        protected List<(Vector3 direction, Vector3 destination)> Paths { get; set; } = new List<(Vector3 direction, Vector3 destination)>();
        protected int CurrentPathIndex { get; set; } = 0;
        protected void UpdatePaths(Vector3 targetCoord, float coolTime)
        {
            if (Time.time < this.nextUpdate)
                return;

            PathFinder.FindPath(transform.position, targetCoord);

            this.Paths = this.PathFinder.GetPathVectors();
            this.CurrentPathIndex = 0;
            this.nextUpdate += coolTime;
        }

        protected void Chase(float turningDistance, float rotationSmooth)
        {
            //Debug.Log($"path count: {Paths.Count}");
            if (Paths.Count <= 0)
                return;

            var distanceToNextNode = Vector3.Distance(transform.position, Paths[CurrentPathIndex].destination);
            if (distanceToNextNode < turningDistance && CurrentPathIndex < Paths.Count - 1)
                CurrentPathIndex++;
            var targetRotation = Quaternion.LookRotation(Paths[CurrentPathIndex].direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);
            Self.Movement.Accelerate();
            Self.Movement.UpdateSpeed();
        }

        protected void SlowDown(float maxSlowdown)
        {
            if (Self.Movement.CurrentSpeed < 0.15f)
            {
                Self.Movement.CurrentSpeed = 0;
                Self.Movement.UpdateSpeed();
                return;
            }

            var slowDownFactor = 1f - Mathf.Clamp01(maxSlowdown) *
                Mathf.Clamp01(Self.Movement.CurrentSpeed + 0.2f / Self.Movement.MaxSpeed);
            var currentSpeed = Self.Animator.GetFloat("speed");
            Self.Movement.CurrentSpeed = Mathf.Clamp01(slowDownFactor) * currentSpeed;
            Self.Movement.UpdateSpeed();
        }


    }
}

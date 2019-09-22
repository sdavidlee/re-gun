using Assets.CharacterInfo;
using Assets.Fundamentals.Extensions;
using Assets.Pathfinding;
using System;
using System.Collections;
using UnityEngine;
using static Assets.CharacterInfo.Character;

namespace Assets.EnemyAI
{
    public enum Direction { Left, Right }

    public abstract class Detection : MonoBehaviour
    {
        public event Action TargetSpotted;

        #region props
        [SerializeField] private float stoppingDistance;
        [SerializeField] private float detectionRange;

        public Vector3 TargetCoord { get; protected set; }
        public PlayerAnimations TargetState { get => FindObjectOfType<Character>().CurrentAnimation; }
        public Vector3 DirectionTowardsTarget { get => (ThePlayer.Position - transform.position).FlatOut(); }
        public float DistanceToTarget { get => DirectionTowardsTarget.magnitude; }
        public Direction TurnDirection { get; protected set; }
        public bool EnemyInRange { get; protected set; }

        protected Enemy Self { get; set; }
        #endregion

        private void Awake()
        {
            Self = GetComponent<Enemy>();
        }

        public abstract void DetectEnemy();

        public abstract void GetTargetSpot();

        public Direction DetermineDirection()
        {
            var standardVector = transform.right;

            var dotProduct = Vector3.Dot(DirectionTowardsTarget, standardVector);
            if (dotProduct >= 0)
                return Direction.Right;
            else
                return Direction.Left;
        }

        public bool IsFacingTarget(float threshold)
        {
            var selfVector = transform.forward.normalized;
            var targetVector = (ThePlayer.transform.position - transform.position).normalized;
            var dotProduct = Vector3.Dot(selfVector, targetVector);

            return dotProduct > Mathf.Clamp01(threshold);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.EnemyAI;
using static Assets.CharacterInfo.Character;
using UnityEngine.AI;
using Assets.Fundamentals.Extensions;
using System.Collections;
using Assets.CharacterInfo;

namespace Assets.EnemyAI
{
    public class ElementaryMeleeDetection : Detection
    {
        protected float CollisionRadius { get; private set; }
        protected float RootMotionAdjustment { get; private set; }
        protected Vector3 PreviousDirection { get; private set; }
        public float TurnAngle { get; private set; }

        private void Awake()
        {
            base.Self = GetComponent<Enemy>();
            this.CollisionRadius = GetComponent<NavMeshAgent>().radius;
            this.RootMotionAdjustment = 0.8f;
        }

        private void OnEnable()
        {
            this.PreviousDirection = (FindObjectOfType<Character>().Position - transform.position).FlatOut();
            StartCoroutine(GetTurnAngle());
        }

        public override void DetectEnemy()
        {
            base.EnemyInRange = base.DistanceToTarget <= Self.Stats.Range + CollisionRadius + RootMotionAdjustment;
        }

        public override void GetTargetSpot()
        {
            base.TargetCoord = ThePlayer.Position;
        }

        private IEnumerator GetTurnAngle()
        {
            yield return new WaitForSeconds(0.5f);
            this.TurnAngle = Vector3.Angle(PreviousDirection, base.DirectionTowardsTarget);
            base.TurnDirection = base.DetermineDirection();
            this.PreviousDirection = base.DirectionTowardsTarget;
            StartCoroutine(GetTurnAngle());
        }
    }
}
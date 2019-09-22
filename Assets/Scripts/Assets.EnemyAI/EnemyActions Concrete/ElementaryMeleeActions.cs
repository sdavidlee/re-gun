using Assets.CharacterInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.RootPathfinding;
using Assets.Fundamentals.Extensions;
using static Assets.CharacterInfo.Character;
using Assets.Supervisor;

namespace Assets.EnemyAI
{
    public class ElementaryMeleeActions : EnemyMeleeActions
    {
        [SerializeField] protected float sphereCastRadius;
        [SerializeField] protected float sphereCastMaxDistance;
        protected float NextAvailableAttack { get; set; } = 0;
        protected ElementaryMeleeDetection TheDetection;
        protected bool EnemyIsInRange { get => TheDetection.DistanceToTarget <= Self.Stats.Range + RootMotionAdjustment; }
        private float RootMotionAdjustment { get; set; } = 0.75f;

        private void Awake()
        {
            base.Self = GetComponent<Enemy>();
            base.PathFinder = GetComponent<RootPathFinding>();
            this.TheDetection = GetComponent<ElementaryMeleeDetection>();
        }

        private void Start()
        {
            GameManager.Instance.GlobalUpdate += Behavior;
        }

        public override void Behavior()
        {
            TheDetection.DetectEnemy();

            if (TheDetection.TurnAngle >= 45f)
                Turn();
            Move();
            if (EnemyIsInRange)
                Attack();
        }

        public override Transform GetTargetInAtackRange()
        {
            var ray = new Ray(origin: transform.position, direction: transform.forward);
            RaycastHit[] hits = new RaycastHit[1];

            Physics.SphereCastNonAlloc(
                ray: ray,
                radius: sphereCastRadius,
                maxDistance: sphereCastMaxDistance,
                results: hits,
                layerMask: LayerMask.GetMask("Player"));

            foreach (var hit in hits)
                if (hit.transform?.GetComponent<Character>())
                    return hit.transform;

            return null;
        }

        public override void Attack()
        {
            if (Time.time < NextAvailableAttack
                || Self.CurrentAnimation != KindsOfAnimation.None
                || Self.Detection.TargetState == PlayerAnimations.Dead)
                return;

            if (!TheDetection.IsFacingTarget(threshold: 0.95f)){
                var smooth = 3f;
                var targetRotation = Quaternion.LookRotation(Self.Detection.DirectionTowardsTarget);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);
                return;
            }

            StartCoroutine(Self.Movement.StopForAnimation(KindsOfAnimation.Attack));
            Self.Animator.SetTrigger("attack");
            NextAvailableAttack = Time.time + Self.Stats.CoolTime;
        }

        public override void Move()
        {
            if (Self.CurrentAnimation != KindsOfAnimation.None ||
                Self.IsDead ||
                !gameObject.activeInHierarchy)
                return;

            base.UpdatePaths(ThePlayer.Position, coolTime: 0.8f);

            if (this.EnemyIsInRange)
                base.SlowDown(maxSlowdown: 0.1f);
            else
                base.Chase(turningDistance: 0.5f, rotationSmooth: 5.3f);
        }

        public override void Turn()
        {
            if (Self.CurrentAnimation == KindsOfAnimation.None){
                Self.CurrentAnimation = KindsOfAnimation.Turn;
                switch (Self.Detection.TurnDirection)
                {
                    case Direction.Left:
                        Self.Animator.SetTrigger("turnLeft");
                        break;
                    case Direction.Right:
                        Self.Animator.SetTrigger("turnRight");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

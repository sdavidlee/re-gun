using Assets.EnemyAI.ParticlesAndProjectiles;
using Assets.Fundamentals.Extensions;
using Assets.Pathfinding;
using Assets.RootPathfinding;
using Assets.Spawning;
using Assets.Supervisor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.CharacterInfo;
using static Assets.CharacterInfo.Character;

namespace Assets.EnemyAI
{
    public class ElementaryRangedAction : EnemyRangedActions
    {
        public GameObject spotMark;
        [SerializeField] protected Pooled_MonoBehavior trajectile;
        [SerializeField] protected Transform arrowFireTransform;
        [SerializeField] protected Transform eyes;
        public float NextAvailableAttack { get; protected set; } = 0;
        protected Pool ArrowPool { get; private set; }

        protected Obstacle ObstacleHit { get; private set; }
        protected MovementNavmesh Movement { get; set; }
        private ElementaryRangedDetection theDetection;
        private void Awake()
        {
            base.Self = GetComponent<Enemy>();
            base.PathFinder = GetComponent<RootPathFinding>();
            base.Animator = GetComponent<Animator>();
            this.Movement = GetComponent<MovementNavmesh>();
            this.ArrowPool = Pool.GetPool(trajectile);
            this.theDetection = Self.Detection as ElementaryRangedDetection; 
        }

        private void OnEnable()
        {
            GameManager.Instance.GlobalUpdate += Behavior;
        }

        public override void Behavior()
        {
            if (theDetection == null)
                theDetection = Self.Detection as ElementaryRangedDetection;
            theDetection.DetectEnemy();
            Move();
            Turn();
            Attack();
        }

        public override bool TargetIsInRange()
        {
            var distance = Vector3.Distance(this.transform.position, ThePlayer.transform.position);
            var rootMotionAdjustment = 0.43f;
            var enemyInRange = distance <= Self.Stats.Range + rootMotionAdjustment;

            return enemyInRange;
        }

        public override void Attack()  
        {
            var coolHasntReturned = Time.time < NextAvailableAttack;
            var targetOutOfRange = !TargetIsInRange();
            var isNotFacingTarget = !theDetection.IsFacingTarget(threshold: 0.99f);
            if (targetOutOfRange ||
                coolHasntReturned ||
                isNotFacingTarget ||
                theDetection.ObstacleIsBlocking ||
                Self.CurrentAnimation != KindsOfAnimation.None ||
                theDetection.TargetState == PlayerAnimations.Dead)
                return;

            transform.rotation = Quaternion.LookRotation(ThePlayer.transform.position - transform.position);
            Self.Animator.SetTrigger("attack");
            Self.Movement.CurrentSpeed = 0;
            NextAvailableAttack += Self.Stats.CoolTime;
        }
        void Fire()
        {
            var arrow = ArrowPool.Get<SimpleArrowTrajectile>(
                spawnPos: arrowFireTransform.position,
                directionVector: transform.forward) as SimpleArrowTrajectile;

            arrow.ShotBy = this.Self;
            arrow.LaunchDirection = new Vector3(0, -0.1f, 1f);
            StartCoroutine(arrow.MoveStraight());
            StartCoroutine(ReturnTrajectileToPool());

            IEnumerator ReturnTrajectileToPool()
            {
                yield return new WaitForSeconds(5f);
                ArrowPool.ReturnToPool(arrow);
                arrow.gameObject.SetActive(false);
            }
        }

        public override void Move()
        {
            if (Self.CurrentAnimation != KindsOfAnimation.None || !gameObject.activeInHierarchy)
                return;

            base.UpdatePaths(theDetection.TargetCoord, coolTime: 0.5f);

            var distanceToTargetCoord = Vector3.Distance(transform.position, theDetection.TargetCoord);
            var targetReached = distanceToTargetCoord < 0.55f;
            var playerInRangeWithNoObstacle = 
                Vector3.Distance(ThePlayer.Position, transform.position) < Self.Stats.Range && 
                !theDetection.ObstacleIsBlocking;

            if (targetReached || playerInRangeWithNoObstacle)
                base.SlowDown(maxSlowdown: 0.2f);
            else
                base.Chase(turningDistance: 0.5f, rotationSmooth: 2.8f);
        } 

        public override void Turn()
        {
            if (Movement.CurrentSpeed > 0 ||
                Self.CurrentAnimation == KindsOfAnimation.Turn ||
                Self.IsDead)
                return;

            var angle = Vector3.Angle(transform.forward.FlatOut(), theDetection.DirectionTowardsTarget);
            if (angle > 60f)
                PlayTurnAnimation(theDetection.DetermineDirection());

            if (Self.CurrentAnimation.Equals(KindsOfAnimation.Turn))
                return;
            var targetRotation = Quaternion.LookRotation(ThePlayer.Position - transform.position);
            var smooth = 5f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);

            void PlayTurnAnimation(Direction dir)
            {
                Self.CurrentAnimation = KindsOfAnimation.Turn;
                switch (dir)
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

using System.Collections;
using System.Linq;
using UnityEngine;
using Assets.Fundamentals;
using Assets.Fundamentals.Extensions;
using Assets.Interfaces;
using Assets.EnemyAI; 
using static Assets.CharacterInfo.PlayerStats;

namespace Assets.CharacterInfo
{
    public class PlayerActions : MonoBehaviour
    {
        private float offset = 0.8f;
        private Collider[] hitResults = new Collider[5];
        private Character Self { get; set; }
        private Animator Animator { get; set; }
        private PlayerStats Stats { get; set; }
        private Movement Movement { get; set; }

        private void Awake()
        {
            Self = GetComponent<Character>();
            Animator = GetComponent<Animator>();
            Stats = GetComponent<PlayerStats>();
        }

        public void Hit()
        {
            var spherePosition = transform.position + transform.forward * offset;
            int hitsCount = Physics.OverlapSphereNonAlloc(spherePosition, radius: 1.4f, hitResults, 1 << 8);

            var validHits =
                hitResults
                    .Where(h =>
                            (h != null) &&
                            (!h.GetComponent<Enemy>().IsDead));

            if (validHits.All(h => h == null))
                return;

            var hit =
                validHits
                    .First();

            hit.GetComponent<IHittable>().TakeHit(this.Stats, AttackTypes.Normal);
            hitResults.ClearAll();
        }

        public void TakeHit(GameObject hitter)
        {
            if (Self.Stats.Health <= 0){
                Die();
                return;
            }

            var conditions = PlayerAnimations.None;
            if ((Self.CurrentAnimation & conditions) == 0)
                return;

            Self.CurrentAnimation = PlayerAnimations.TakingHit;
            var playerVector = transform.forward;
            var standardVector = hitter.transform.forward * -1;
            var dotProduct = Vector3.Dot(playerVector, standardVector);
            FacingDirection direction = DetermineDirection(dotProduct);

            Animator.SetTrigger("takeHit");
            React(direction);

            //local functions
            FacingDirection DetermineDirection(float dot)
            {
                if (dot > 0.7f && dot <= 1)
                    return direction = FacingDirection.Front;
                else if (dot >= -0.7f && dot <= 0.7f)
                    return direction = transform.forward.z > 0
                        ? FacingDirection.SideUp
                        : FacingDirection.SideDown;
                else
                    return direction = FacingDirection.Back;
            }

            void React(FacingDirection dir)
            {
                switch (dir)
                {
                    case FacingDirection.Front:
                    case FacingDirection.SideDown:
                        Animator.SetTrigger("reactRight");
                        break;
                    case FacingDirection.Back:
                    case FacingDirection.SideUp:
                        Animator.SetTrigger("reactLeft");
                        break;
                    default:
                        break;
                }
            }

            void Die()
            {
                Animator.Play("Death");
                Self.CurrentAnimation = PlayerAnimations.Dead;
                Self.GetComponent<Character>().OnPlayerDeath();
            }
        }

        public void SetCurrentAnimation_None()
        {
            StartCoroutine(Coroutine());

            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(0.1f);
                Self.CurrentAnimation = PlayerAnimations.None;
            }
        }

        public void SetCurrentAnimation_Hitting()
        {
            Self.CurrentAnimation = PlayerAnimations.Hitting;
        }
    }
}

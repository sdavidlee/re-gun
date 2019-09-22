using Assets.CharacterInfo;
using Assets.GameUI;
using Assets.Interfaces;
using Assets.Spawning;
using Assets.Pathfinding;
using System.Linq;
using System;
using System.Collections;
using UnityEngine;
using static Assets.CharacterInfo.PlayerStats;
using System.Runtime.CompilerServices;

namespace Assets.EnemyAI
{
    public class EnemyActions : MonoBehaviour, IHittable
    {
        private Enemy Self { get; set; }
        private float NextAvaialbeAttack { get; set; }
        public EnemyRangedActions RangedAction { get; private set; }
        public EnemyMeleeActions MeleeAction { get; private set; }
        public EnemyResurrectionActions ResurrectionAction { get; private set; }

        private void Awake()
        {
            this.Self = GetComponent<Enemy>();
            RangedAction = GetComponent<EnemyRangedActions>();
            MeleeAction = GetComponent<EnemyMeleeActions>();
            ResurrectionAction = GetComponent<EnemyResurrectionActions>();
        }

        public void DealDamage()
        {
            var target = this.MeleeAction.GetTargetInAtackRange();

            if (target == null)
                return;

            target.GetComponent<Character>().Stats.CalculateDamage(Self.Stats, AttackTypes.Normal);
            target.GetComponent<PlayerActions>().TakeHit(gameObject);
            UITextManager.Instance.PlayerHealthUI.UpdateValues();
        }

        public void TakeHit(PlayerStats hitterStats, AttackTypes attackType = AttackTypes.Normal)
        {
            if (Self.IsDead || Self.CurrentAnimation == KindsOfAnimation.BeingSummoned)
                return;

            TakeDamge(attackType);
            GenerateHitEffect();
            PlayAnimationIfPossible();

            #region Local Functions
            void TakeDamge(AttackTypes typeOfAttack)
            {
                Self.Stats.CalculateDamage(hitterStats, attackType);

                Self.OnEnemyHasBeenAttacked();
                DieIfHealthBelowZero();
            }

            void GenerateHitEffect()
            {
                var particlePosition = transform.position + new Vector3(0, 2.3f, 0);
                Instantiate(GetComponent<Pooled_MonoBehavior>().HitEffect, particlePosition, Quaternion.identity);
            }

            void PlayAnimationIfPossible()
            {
                if ((Self.CurrentAnimation & (KindsOfAnimation.TakingHit | KindsOfAnimation.Attack)) != 0)
                    return;

                Self.CurrentAnimation = KindsOfAnimation.TakingHit;
                if (Self.Stats.Health > 0)
                    Self.Animator.Play("GetHit");
            }
            #endregion
        }

        public void DieIfHealthBelowZero(bool shouldResurrect = true)
        {
            if (Self.Stats.Health > 0)
                return;

            Self.Animator.Play("Death");
            Self.IsDead = true;
            Self.OnEnemyDeath();
            Self.HealthBar.gameObject.SetActive(false);
            ResurrectionAction?.Resurrect(shouldResurrect);

            Self.Deactivate();
        }
    }
}

using System;
using Assets.EnemyAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.CharacterInfo.Character;
using static Assets.CharacterInfo.PlayerStats;

namespace Assets.CharacterInfo
{
    [Obsolete("Deprecated")]
    public class SwordCast : MonoBehaviour
    {
        private Animator SwordAnimator { get; set; }

        private void Awake()
        {
            SwordAnimator = GetComponentInChildren<Animator>();
        }

        private void OnTriggerEnter(Collider hit)
        {
            var isThrowingSword = SwordAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordCast");
            if (!isThrowingSword)
                return;

            if (hit.gameObject.layer == Mathf.Log(LayerMask.GetMask("Hittable"), 2))
            {
                var enemy = hit.GetComponent<Enemy>();
                var playerStats = GetComponentInParent<PlayerStats>();
                enemy.EnemyActions.TakeHit(playerStats, AttackTypes.ThrowWeapon);
            }
        }

        public void Cast_ThrowWeapon()
        {
            SwordAnimator.SetTrigger("cast");
        }

        public void SetCurrentAnimation(PlayerAnimations typeofAnimation)
        {
            var player = GetComponentInParent<Character>();

            switch (typeofAnimation)
            {
                case PlayerAnimations none when none is PlayerAnimations.None:
                    player.CurrentAnimation = none;
                    break;
                case PlayerAnimations hitting when hitting is PlayerAnimations.Hitting:
                    player.CurrentAnimation = hitting;
                    break;
                case PlayerAnimations takingHit when takingHit is PlayerAnimations.TakingHit:
                    player.CurrentAnimation = takingHit;
                    break;
                default:
                    throw new ArgumentException("You have entered the wrong type of enum Animations as an input");
            }
        }
    }
}

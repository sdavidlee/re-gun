using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Spawning;
using Assets.CharacterInfo;
using Assets.GameUI;

namespace Assets.EnemyAI.ParticlesAndProjectiles
{
    public class SimpleArrowTrajectile : Pooled_MonoBehavior
    {
        public Enemy ShotBy { get; set; }
        public Vector3 LaunchDirection { get; set; }

        public IEnumerator MoveStraight()
        {
            yield return null;
            transform.Translate(LaunchDirection.normalized);
            if (isActiveAndEnabled)
                StartCoroutine(MoveStraight());
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponentInParent<Character>();
            if (player){
                DealDamage();
                Pool.GetPool(this).ReturnToPool(this);
                gameObject.SetActive(false);
            }

            //local funcs
            void DealDamage()
            {
                player.Stats.CalculateDamage(ShotBy.Stats, AttackTypes.Normal);
                UITextManager.Instance.PlayerHealthUI.UpdateValues();
                player.Actions.TakeHit(ShotBy.gameObject);
            }
        }
    }
}
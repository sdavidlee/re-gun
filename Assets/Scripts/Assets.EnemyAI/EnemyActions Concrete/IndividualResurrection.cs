using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Supervisor;

namespace Assets.EnemyAI
{
    public class IndividualResurrection : EnemyResurrectionActions
    {
        private void Awake()
        {
            base.Self = GetComponent<Enemy>();
        }

        public override void Resurrect(bool shouldResurrect = true)
        {
            if (!shouldResurrect)
                return;

            var timeUntilResurrection = UnityEngine.Random.Range(3f, 35f);
            Invoke("StartResurrection", timeUntilResurrection);
            GameManager.Instance.GlobalUpdate += Self.GetComponent<EnemyVariousActions>().Behavior;
        }
        private void StartResurrection()
        {
            Self.Animator.Play("Resurrect");
            Self.Animator.ResetTrigger("takeHit");
        }

        public override void UponResurrectionUpdateInfo()
        {
            Self.IsDead = false;
            Self.Stats.Reset();
            Self.SetCurrentAnimation_None();
            Self.HealthBar.gameObject.SetActive(true);
            Self.HealthBar.UpdateHealthBar();
        }
    }
}

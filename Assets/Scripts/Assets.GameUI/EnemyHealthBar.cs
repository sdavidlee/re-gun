using Assets.Fundamentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameUI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image Bar;
        private Quaternion DefaultRotaiton { get; set; }
        private Stats Stats { get; set; }

        private void Awake()
        {
            Stats = GetComponentInParent<Stats>();
        }

        private void FixedUpdate()
        {
            //maintains consistent rotation
            //transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        }

        private void Update()
        {
            //maintains consistent rotation
            transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        }

        public void OnEnemyHasBeenAttacked_UpdateHealthBar()
        {
            UpdateHealthBar();
        }

        public void UpdateHealthBar()
        {
            var remainingHealth = Stats.GetHealthRatio();
            Bar.fillAmount = remainingHealth;
        }
    }
}

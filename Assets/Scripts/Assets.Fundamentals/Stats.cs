using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum AttackTypes{
    Normal,
    ThrowWeapon
}

namespace Assets.Fundamentals
{

    public class Stats : MonoBehaviour
    {
        public float MaxHealth { get; private set; }

        [SerializeField] private float _health;
        public float Health
        {
            get => _health;
            set { _health = value; }
        }
        [SerializeField] private float _physicalDamage;
        public float PhysicalDamage
        {
            get => _physicalDamage;
            set { _physicalDamage = value; }
        }
        [SerializeField] private float _coolTime;
        public float CoolTime
        {
            get => _coolTime;
            set { _coolTime = value; }
        }

        private void Awake()
        {
            MaxHealth = _health;
        }

        public void Reset()
        {
            Health = MaxHealth;
        }

        public void CalculateDamage(Stats hitterStats, AttackTypes attackType)
        {
            switch (attackType){
                case AttackTypes.Normal:
                    this.Health -= hitterStats.PhysicalDamage;
                    break;
                case AttackTypes.ThrowWeapon:
                    this.Health -= hitterStats.PhysicalDamage * 0.7f;
                    break;
                default:
                    break;
            }
        }

        public float GetHealthRatio() => Health / MaxHealth;
    }
}

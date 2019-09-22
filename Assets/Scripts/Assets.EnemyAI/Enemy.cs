using System;
using Assets.EnemyAI;
using Assets.Fundamentals;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Assets.Spawning;
using static Assets.EnemyAI.Detection;
using Assets.Missions;
using Assets.GameUI;
using Assets.Supervisor;
using System.Linq;

namespace Assets.EnemyAI
{
    [Flags]
    public enum KindsOfAnimation
    {
        None = 1,
        Turn = 2,
        TakingHit = 4,
        Attack = 8,
        BeingSummoned = 16
    }

    [RequireComponent(typeof(EnemyActions))]
    [RequireComponent(typeof(Detection))]
    [RequireComponent(typeof(MovementNavmesh))]
    [RequireComponent(typeof(EnemyStats))]
    public class Enemy : MonoBehaviour
    {
        public event Action EnemyDied;
        public event Action EnemyHasBeenAttacked;

        public MovementNavmesh Movement { get; private set; }
        public Animator Animator { get; private set; } 
        public Detection Detection { get; private set; }
        public KindsOfAnimation CurrentAnimation { get; set; } = KindsOfAnimation.None;
        public EnemyStats Stats { get; private set; }
        public EnemyHealthBar HealthBar { get; private set; }
        
        public EnemyActions EnemyActions { get; private set; }
        public bool IsDead { get; set; }
    
        private void Awake()
        {
            Detection = GetComponent<Detection>();
            Movement = GetComponent<MovementNavmesh>();
            Animator = GetComponent<Animator>();
            EnemyActions = GetComponent<EnemyActions>();
            Stats = GetComponent<EnemyStats>();
            HealthBar = GetComponentInChildren<EnemyHealthBar>();

            EnemyDied += FindObjectOfType<Crystal>().OnEnemyDeath_BoostProgress;

            EnemyHasBeenAttacked += GetComponentInChildren<EnemyHealthBar>().OnEnemyHasBeenAttacked_UpdateHealthBar;
        }

        public void OnEnemyDeath()
        {
            EnemyDied?.Invoke();
            GameManager.Instance.GlobalUpdate -= GetComponent<EnemyVariousActions>().Behavior;
        }

        public void OnEnemyHasBeenAttacked()
        {
            EnemyHasBeenAttacked?.Invoke();
        }

        public void Deactivate()
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            this.IsDead = true;
        }

        public void ActivateBehaviors()
        {
            GetComponents<MonoBehaviour>()
                .ToList()
                .ForEach(b => b.enabled = true);

            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }

        public void SetCurrentAnimation_None()
        {
            CurrentAnimation = KindsOfAnimation.None;
        }
    }
}


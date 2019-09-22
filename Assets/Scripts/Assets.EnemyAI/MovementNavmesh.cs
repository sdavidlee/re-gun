using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Time;
using static Assets.EnemyAI.Enemy;

namespace Assets.EnemyAI
{
    [Serializable]
    public class MovementNavmesh : MonoBehaviour
    {
        [SerializeField] private float _acceleration;
        public float Acceleration { get => _acceleration; }
        [SerializeField]  private float _maxSpeed;
        public float MaxSpeed { get => _maxSpeed; }
        public float CurrentSpeed { get; set; }
        public NavMeshAgent navMeshAgent { get; private set; }

        private Enemy Enemy { get; set; }
        private Detection Detection { get; set; }

        private void Awake()
        {
            //navMeshAgent = GetComponent<NavMeshAgent>();
            Enemy = GetComponent<Enemy>();
            Detection = GetComponent<Detection>();
        }

        public void Accelerate()
        {
            //if (Detection.ReachedDestination)
            //    return;

            CurrentSpeed +=
                CurrentSpeed < MaxSpeed
                ? Acceleration * smoothDeltaTime
                : MaxSpeed - CurrentSpeed;
        }

        //this class is deprecated
        public void StopIfDestinationReached()
        {
            //if (!Detection.ReachedDestination)
            //    return;

            float smooth = 3.5f;
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, smooth * fixedDeltaTime);
            UpdateSpeed();
        }

        public IEnumerator StopForAnimation(KindsOfAnimation kindOfAnimation)
        {
            Enemy.CurrentAnimation = kindOfAnimation;
            float smooth = 6f;

            for (int i = 0; i < 60; i++)
            {
                CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, smooth * fixedDeltaTime);
                UpdateSpeed();
                yield return null;
            }
        }

        public void UpdateSpeed()
        {
            Enemy.Animator.SetFloat("speed", CurrentSpeed);
        }
    }
}

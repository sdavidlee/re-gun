using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Spawning;
using Assets.GameUI;
using Assets.Missions;
using Assets.EnemyAI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

namespace Assets.Supervisor
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action GlobalUpdate;
        public event Action GlobalLateUpdate;
        public event Action GlobalFixedUpdate;
        public event Action CrystalQuestCleared;

        private void Awake()
        {
            if (GameManager.Instance != null)
                Destroy(gameObject);
            
            Instance = this;
        }

        private void Update()
        {
            GlobalUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            GlobalFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            GlobalLateUpdate?.Invoke();
        }

        public void OnCrystalQuestCleared([CallerMemberName] string memberName = "")
        {
            CrystalQuestCleared?.Invoke();
        }

        public void OnCrystalQuestCleared_KillAllEnemies()
        {
            StartCoroutine(WaitThenKill());

            IEnumerator WaitThenKill()
            {
                yield return new WaitForSeconds(0.3f);

                var enemies = FindObjectsOfType<Enemy>().ToList();
                foreach (var enemy in enemies)
                {
                    enemy.Stats.Health = 0;
                    enemy.IsDead = true;
                    enemy.Animator.SetBool("crystalHasExploded", true);

                    var isPlayingResurrectionAnimation = enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Resurrect");
                    if (isPlayingResurrectionAnimation)
                        enemy.Animator.SetTrigger("crystalExploded");
                    else
                        enemy.GetComponent<EnemyActions>().DieIfHealthBelowZero(shouldResurrect: false);
                }
            }
        }

        //for test uses
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

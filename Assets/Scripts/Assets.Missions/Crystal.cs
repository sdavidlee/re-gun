using System;
using UnityEngine;
using Assets.GameUI;
using Assets.Supervisor;
using Assets.Spawning;
using System.Collections;
using static Assets.CharacterInfo.Character;

namespace Assets.Missions
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField] private float missionFinishedThreshold;
        [SerializeField] private float targetIntensity;
        [SerializeField] private ProgressBar progress;
        [SerializeField] private Spawner spawner;
        private Light crystalLight;
        private Animator animator;
        private float CurrentProgress { get; set; }
        private float DifficultyFactor { get; set; } = 1.1f;
        private float x { get; set; } = 0;
        private Func<float, float> progressFunction = x => 2 * Mathf.Sqrt(2) * x;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.enabled = false;
            crystalLight = GetComponentInChildren<Light>();
            StartCoroutine(UpdateProgress());

            ThePlayer.PlayerDied += StopProgress;
        }

        private IEnumerator UpdateProgress()
        {
            yield return new WaitForSeconds(0.5f);
            missionFinishedThreshold += 0.05f;
            x += 0.1f;
            CurrentProgress = progressFunction(x) / missionFinishedThreshold;
            AdjustLight(CurrentProgress);
            progress.ScaleBar(CurrentProgress);

            if (CurrentProgress >= 1)
            {
                enabled = false;
                GameManager.Instance.OnCrystalQuestCleared();
                StopAllCoroutines();
            }
            else
                StartCoroutine(UpdateProgress());
        }

        private void StopProgress()
        {
            StopAllCoroutines();
        }

        private void AdjustLight(float progress)
        {
            var scale = targetIntensity;
            var currentIntensity = scale * Mathf.Clamp(progress, 0, 1f);
            crystalLight.intensity = currentIntensity;
        }

        public void OnEnemyDeath_BoostProgress()
        {
            x += 1.2f;
        }

        public void OnQuestComplete_EmitLight()
        {
            animator.enabled = true;
            animator.Play("Crystal Lights Up");
        }

        public void OnQuestComplete_StopSpawning()
        {
            spawner.StopSpawning();
        }
    }
}

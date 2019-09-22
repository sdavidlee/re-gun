using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Collections;
using Assets.EnemyAI;
using Assets.Spawning;

namespace Assets.GameUI
{
    [RequireComponent(typeof(PlayerHealthUI))]
    [DisallowMultipleComponent]
    public class UITextManager : MonoBehaviour
    {
        public static UITextManager Instance { get; private set; }
        public PlayerHealthUI PlayerHealthUI { get; private set; }

        [SerializeField] private GameObject GameOverText;
        [SerializeField] private GameObject QuestComplete;

        private void Awake()
        {
            Instance = this;
            PlayerHealthUI = GetComponent<PlayerHealthUI>();
        }

        public void OnPlayerDeath_DisPlayGameOver()
        {
            StartCoroutine(TmProFadeIn(GameOverText));
        }

        public void OnQuestCleared_DisplayQuestCompleteText()
        {
            StartCoroutine(FadeInAndOut(QuestComplete));
        }

        private IEnumerator TmProFadeIn(GameObject text)
        {
            text.SetActive(true);
            var tmpro = text.GetComponent<TextMeshProUGUI>();
            
            var steps = 180;
            for (int i = 0; i < steps; i++)
            {
                yield return null;
                tmpro.alpha = i / (float)steps;
            }
        }

        private IEnumerator TmProFadeOut(GameObject text)
        {
            var tmpro = text.GetComponent<TextMeshProUGUI>();

            var steps = 180;
            for (int i = steps; i > 0; i--)
            {
                yield return null;
                tmpro.alpha = i / (float)steps;
            }

            text.SetActive(false);
        }

        private IEnumerator FadeInAndOut(GameObject text)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(TmProFadeIn(text));
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(TmProFadeOut(text));
        }
    }
}

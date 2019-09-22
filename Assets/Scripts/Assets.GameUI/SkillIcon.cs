using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameUI
{
    public enum SkillShortKeys { Q, W, E, R, F }

    public class SkillIcon : MonoBehaviour
    {
        public SkillShortKeys shortKey;
        private Image CoolTimeShade { get; set; }

        private void Awake()
        {
            this.CoolTimeShade = GetComponentsInChildren<Image>()[1];
        }

        public void DisplayCool(float cool)
        {
            if (this.CoolTimeShade.fillAmount > 0)
                return;

            this.CoolTimeShade.fillAmount = 1;
            StartCoroutine(StartCool(timeElapsed: 0));
            
            IEnumerator StartCool(float timeElapsed)
            {
                yield return null;
                timeElapsed += Time.deltaTime;
                this.CoolTimeShade.fillAmount = 1f - Mathf.Clamp01(timeElapsed / cool);
                if (timeElapsed < cool)
                    StartCoroutine(StartCool(timeElapsed));
            }
        }
    }
}

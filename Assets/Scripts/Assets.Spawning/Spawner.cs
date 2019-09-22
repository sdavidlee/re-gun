using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.EnemyAI;

namespace Assets.Spawning
{
    public class Spawner : MonoBehaviour
    {
        protected void FadeIn(GameObject summoned, ParticleSystem spawnEffect, Shader targetShader = null)
        {
            var materials = summoned.GetComponentsInChildren<Renderer>().Select(m => m.material);
            var startColor = new Color(1, 1, 1, 0);
            var targetColor = new Color(1, 1, 1, 1);

            StartCoroutine(ShiftColor());

            IEnumerator ShiftColor()
            {
                var framesCount = 70f;
                for (int i = 0; i < framesCount; i++)
                {
                    yield return null;
                    foreach (var material in materials)
                        material.color = Color.Lerp(startColor, targetColor, i / framesCount);
                }
                    
                summoned.GetComponent<Enemy>().CurrentAnimation = KindsOfAnimation.None;
                SwitchShader();
                Destroy(spawnEffect.gameObject, 3f);
            }

            void SwitchShader()
            {
                if (targetShader == null)
                    return;

                summoned.GetComponentInChildren<Renderer>().material.shader = targetShader;
            }
        }

        public void StopSpawning()
        {
            CancelInvoke();
        }
    }
}

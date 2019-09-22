using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.CharacterInfo.Character;

namespace Assets.WeaponSystem
{
    public class Mine : MonoBehaviour
    {
        private float retrievalCompleteDistance = 0.2f;
        [SerializeField] private float returnSpeed;
        private float smooth;

        private void Awake()
        {
            this.smooth = Mathf.Clamp01(returnSpeed * Time.deltaTime);
        }

        public void ReturnToPlayer()
        {
            var destination = ThePlayer.Center.position;
            
            IEnumerator Return()
            {
                yield return null;
                var retrievalComplete = Vector3.Distance(transform.position, destination) < 0.2f;
                if (!retrievalComplete){
                    transform.position = Vector3.Lerp(transform.position, destination, this.smooth);
                    StartCoroutine(Return());
                }
                else
                    StopAllCoroutines();

            }
        }
    }
}

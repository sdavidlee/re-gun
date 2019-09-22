using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Supervisor;
using System.Collections;

namespace Assets.Environment
{
    public class Door : MonoBehaviour
    {
        private Animator Animator { get; set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();

            FindObjectOfType<GameManager>().CrystalQuestCleared += OnCrystalQuestCleared_OpenDoor;
        }

        private void OnCrystalQuestCleared_OpenDoor()
        {
            StartCoroutine(WaitThenOpenDoor());

            IEnumerator WaitThenOpenDoor()
            {
                yield return new WaitForSeconds(3f);
                Animator.SetTrigger("openDoor");
            }
        }
    }
}

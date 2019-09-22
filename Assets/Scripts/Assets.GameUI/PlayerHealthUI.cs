using Assets.CharacterInfo;
using Assets.Fundamentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameUI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        private Image UI_Health{ get; set; }
        private Image UI_Mana { get; set; }
        private PlayerStats Stats { get; set; }
        private float MaxHealth{ get; set; }
        private float MaxMana { get; set; }

        private void Awake()
        {
            UI_Health = GameObject.Find("Health Bar").GetComponent<Image>();
            UI_Mana = GameObject.Find("Mana Bar").GetComponent<Image>();
            Stats = FindObjectOfType<PlayerStats>();
        }

        private void Start()
        {
            MaxHealth= Stats.Health;
            UpdateValues();
        }

        public void UpdateValues()
        {
            UI_Health.fillAmount = Stats.Health / MaxHealth;
        }
    }
}

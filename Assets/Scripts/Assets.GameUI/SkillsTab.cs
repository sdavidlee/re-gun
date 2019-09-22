using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GameUI
{
    public class SkillsTab : MonoBehaviour
    {
        public SkillIcon[] SkillIcons { get; private set; }

        public static SkillsTab Instance { get; private set; }
        private void Awake()
        {
            SkillsTab.Instance = this;

            this.SkillIcons = GetComponentsInChildren<SkillIcon>();
        }

        public SkillIcon GetSkillIcon(SkillShortKeys shortKey) => this.SkillIcons.First(i => i.shortKey == shortKey);
    }
}

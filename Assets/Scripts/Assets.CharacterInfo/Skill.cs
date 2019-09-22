using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.GameUI;
using static Assets.CharacterInfo.Character;

namespace Assets.CharacterInfo
{
    public class Skill
    {
        public string SkillName { get; private set; }
        public float CoolTime { get; private set; }
        public float NextAvailableAttack { get; private set; }
        private SkillShortKeys shortKey;

        public Skill(string skillName, float cooltime, SkillShortKeys shortKey)
        {
            this.SkillName = skillName;
            this.CoolTime = cooltime;
            this.NextAvailableAttack = 0;
            this.shortKey = shortKey;
        }

        public void Use()
        {
            if (Time.time < NextAvailableAttack)
                return;

            ThePlayer.Animator.SetTrigger(SkillName);
            ThePlayer.CurrentAnimation = PlayerAnimations.UsingSkill;
            this.NextAvailableAttack = Time.time + CoolTime;
            //turn the following code back on when you have put the skill icons
            //SkillsTab.Instance.GetSkillIcon(this.shortKey).DisplayCool(cool: this.CoolTime);
        }
    }

    public static class Skills
    {
        //decaptialize the first letter
        public static Skill ThrowWeapon = new Skill("throwWeapon", 4.5f, SkillShortKeys.Q);
        public static Skill Roll = new Skill("roll", 5f, SkillShortKeys.F);
    }
}

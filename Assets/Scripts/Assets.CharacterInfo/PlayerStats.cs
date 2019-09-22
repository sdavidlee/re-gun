using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Fundamentals;

namespace Assets.CharacterInfo
{
    public class PlayerStats : Stats
    {
        public enum AttackTypes { Normal, ThrowWeapon}

        public float NextAvailabeAttack { get; set; }

        private void Awake()
        {
            NextAvailabeAttack = 0;
        }
    }
}

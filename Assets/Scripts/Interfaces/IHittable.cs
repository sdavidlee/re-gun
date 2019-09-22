using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Fundamentals;
using Assets.CharacterInfo;
using static Assets.CharacterInfo.PlayerStats;

namespace Assets.Interfaces
{
    public interface IHittable
    {
        void TakeHit(PlayerStats stats, AttackTypes attackType = AttackTypes.Normal);
    }
}

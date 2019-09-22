using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Pathfinding;
using Assets.CharacterInfo;
using Assets.RootPathfinding;

namespace Assets.EnemyAI
{
    public abstract class EnemyMeleeActions : EnemyVariousActions
    {
        public abstract Transform GetTargetInAtackRange();
        public abstract void Attack();
        public abstract void Move();
        public abstract void Turn();
    }
}

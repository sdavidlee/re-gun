using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Assets.Pathfinding;
using Assets.RootPathfinding;
using Assets.CharacterInfo;
using Assets.Spawning;

namespace Assets.EnemyAI
{
    public abstract class EnemyRangedActions : EnemyVariousActions
    {
        public Animator Animator { get; protected set; }
        protected NavMeshAgent ai { get; set; }

        public abstract bool TargetIsInRange();
        //attack should be abstract because some enemies's attacking pattern might be more advanced than simply shooting a trajectile
        public abstract void Attack();
        public abstract void Move();
        public abstract void Turn();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Fundamentals;

namespace Assets.EnemyAI
{
    public class EnemyStats : Stats
    {
        [SerializeField] private float _range;
        public float Range { get => _range; }
    }
}

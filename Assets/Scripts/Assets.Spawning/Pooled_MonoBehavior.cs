using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Spawning
{
    public class Pooled_MonoBehavior : MonoBehaviour
    {
        [SerializeField] private int _poolCount;
        public int PoolCount { get => _poolCount; }
        [SerializeField] private ParticleSystem _spawnEffect;
        public ParticleSystem SpawnEffect { get => _spawnEffect; }
        [SerializeField] private ParticleSystem _hitEffect;
        public ParticleSystem HitEffect { get => _hitEffect; }
    }
}

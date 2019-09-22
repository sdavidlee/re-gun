using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.EnemyAI;
using Assets.Pathfinding;

namespace Assets.Spawning
{
    public class Spawner_Dugneon1 : Spawner
    {
        private Pool pool_UndeadSwordsMan;

        [Header("Undead SwordsMan")]
        #region UndeadSwordsMan Variables
        [SerializeField] private Pooled_MonoBehavior prefab_UndeadSwordsMan;
        [SerializeField] private ParticleSystem spawnEffect_UndeadSwordsMan;
        [SerializeField] private Shader shader_UndeadSwordsMan;
        [SerializeField] private int quota_UndeadSwordsMan;
        [SerializeField] private float spawnRate_UndeadSwordsMan;
        private int count_UndeadSwordsManLowLevel = 0;
        private Queue<SpawnPoint> spawnPoints_UndeadSwordsMan = new Queue<SpawnPoint>();
        #endregion

        private void Awake()
        {
            pool_UndeadSwordsMan = Pool.GetPool(prefab_UndeadSwordsMan);

            foreach (var spawnPoint in GetComponentsInChildren<SpawnPoint>())
                spawnPoints_UndeadSwordsMan.Enqueue(spawnPoint);
        }

        private void OnEnable()
        {
            InvokeRepeating("Spawn_UndeadSwordsMan", time: 2f, repeatRate: spawnRate_UndeadSwordsMan);
        }

        private void Spawn_UndeadSwordsMan()
        {
            if (count_UndeadSwordsManLowLevel >= quota_UndeadSwordsMan)
            {
                CancelInvoke("Spawn_UndeadSwordsMan");
                return;
            }

            UndeadFadesIn();

            void UndeadFadesIn()
            {
                var spawnPos = spawnPoints_UndeadSwordsMan.Dequeue();
                spawnPoints_UndeadSwordsMan.Enqueue(spawnPos);

                var undeadSwordsMan = pool_UndeadSwordsMan.Get<UndeadSwordsManLowLevel>(spawnPos.Position, Quaternion.identity.eulerAngles);
                undeadSwordsMan.GetComponent<Enemy>().CurrentAnimation = KindsOfAnimation.BeingSummoned;
                var spawnEffect = Instantiate(spawnEffect_UndeadSwordsMan, spawnPos.Position + new Vector3(0, 0.3f, 0), Quaternion.LookRotation(Vector3.up));
                undeadSwordsMan.transform.position = spawnPos.Position;
                FadeIn(undeadSwordsMan.gameObject, spawnEffect, targetShader: shader_UndeadSwordsMan);
            }
        }
    }
}

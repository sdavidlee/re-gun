using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Spawning
{
    public class Pool : MonoBehaviour
    {
        public static Dictionary<Type, Pool> pools = new Dictionary<Type, Pool>();

        public Queue<Pooled_MonoBehavior> pool { get; private set; } = new Queue<Pooled_MonoBehavior>();
        public Pooled_MonoBehavior Pooled { get; private set; }

        public static Pool GetPool(Pooled_MonoBehavior pooled)
        {
            if (pools.ContainsKey(pooled.GetType()))
                return pools[pooled.GetType()];

            var poolGameObject = new GameObject($"Pool-{pooled.name}");
            poolGameObject.transform.position = Vector3.zero;
            var pool = poolGameObject.AddComponent(typeof(Pool)) as Pool;
            pool.Pooled = pooled;
            pools.Add(pooled.GetType(), pool);
            return pool.GrowPool();
        }

        public void ReturnToPool(Pooled_MonoBehavior pooled)
        {
            pool.Enqueue(pooled);
        }
    }
}

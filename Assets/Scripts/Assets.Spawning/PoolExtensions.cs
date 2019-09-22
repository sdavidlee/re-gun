using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Spawning
{
    public static class PoolExtensions
    {
        public static Pool GrowPool(this Pool @this)
        {
            if (@this.pool.Count > 0)
                return @this;

            for (int i = 0; i < @this.Pooled.PoolCount; i++)
            {
                var pooledGameObject = GameObject.Instantiate(@this.Pooled);
                pooledGameObject.name = $"{@this.Pooled.name} - {i}";
                pooledGameObject.transform.parent = @this.transform;
                pooledGameObject.gameObject.SetActive(false);
                @this.pool.Enqueue(pooledGameObject);
            }
            
            return @this;
        }

        public static Pooled_MonoBehavior Get<T>(this Pool @this, Vector3 spawnPos, Vector3 directionVector) where T : Pooled_MonoBehavior
        {
            var pooled = @this.pool.Dequeue();
            pooled.gameObject.SetActive(true);
            pooled.transform.position = spawnPos;
            pooled.transform.rotation = Quaternion.LookRotation(directionVector);

            return pooled;
        }
    }
}

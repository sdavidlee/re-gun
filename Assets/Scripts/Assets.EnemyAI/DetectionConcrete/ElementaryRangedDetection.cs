using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Fundamentals.Extensions;
using Assets.Pathfinding;
using Assets.RootPathfinding;
using static Assets.CharacterInfo.Character;

namespace Assets.EnemyAI
{
    [RequireComponent(typeof(RootPathFinding))]
    public class ElementaryRangedDetection : Detection
    {
        [SerializeField] private Transform eyes;
        public bool ObstacleIsBlocking { get; private set; }
        public Obstacle ObstacleHit { get; private set; }
        private Vector3 DetectDirection { get => ThePlayer.Center.position - eyes.position; }
        private RootPathFinding Pathfinder { get; set; }

        private void Awake()
        {
            base.Self = GetComponent<Enemy>();
            Pathfinder = GetComponent<RootPathFinding>();
        }

        public override void DetectEnemy()
        {
            ObstacleIsBlocking =
                Physics.Raycast(
                    origin: eyes.position,
                    direction: DetectDirection,
                    maxDistance: DetectDirection.magnitude,
                    hitInfo: out RaycastHit hit,
                    layerMask: LayerMask.GetMask("Obstacle"));

            Debug.DrawRay(eyes.position, DetectDirection, Color.red);
            ObstacleHit = hit.transform?.GetComponent<Obstacle>();
            GetTargetSpot();
        }

        public override void GetTargetSpot()
        {
            if (ObstacleHit)
            {
                //pseudo-code
                //if both spots have the same occupant count
                //then return whatever is closer and up the occupant count of that node by 1
                //else return a spot with a lower occupatn count
                base.TargetCoord =
                    ObstacleHit
                        .GetSpotsInRange(Self.Stats.Range)
                        .OrderBy(spot => Vector3.Distance(transform.position, spot))
                        .First();
            }
            else {
                var fromSelfToPlayerVector = (ThePlayer.Position - transform.position).FlatOut().normalized;
                base.TargetCoord =
                    ThePlayer.Position.FlatOut() - fromSelfToPlayerVector * Self.Stats.Range;
            }
        }
    }
}

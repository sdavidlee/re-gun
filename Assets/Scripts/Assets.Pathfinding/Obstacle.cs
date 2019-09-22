using Assets.Fundamentals.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.CharacterInfo.Character;

namespace Assets.Pathfinding
{
    [RequireComponent(typeof(BoxCollider))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameObject markPrefab;
        public List<Vector3> corners { get; private set; } = new List<Vector3>();
        private Vector3 Center { get; set; }

        private void Awake()
        {
            var boxCollider = GetComponent<BoxCollider>();
            var boxRadiusX = transform.localScale.x * boxCollider.size.x / 2;
            var boxRadiusZ = transform.localScale.z * boxCollider.size.z / 2;
            Center = (transform.position + boxCollider.center).FlatOut();

            //upper left
            corners.Add(new Vector3(Center.x - boxRadiusX, 0, Center.z + boxRadiusZ));
            //upper right
            corners.Add(new Vector3(Center.x + boxRadiusX, 0, Center.z + boxRadiusZ));
            //lower left
            corners.Add(new Vector3(Center.x - boxRadiusX, 0, Center.z - boxRadiusZ));
            //lower right
            corners.Add(new Vector3(Center.x + boxRadiusX, 0, Center.z - boxRadiusZ));
        }

        private void Start()
        {
            //var spots = GetSpotsInRange(8f);
            //foreach (var spot in spots)
            //    Instantiate(markPrefab, spot, Quaternion.identity);
        }

        public Vector3[] GetSpotsInRange(float range)
        {
            var playerPos = ThePlayer.transform.position.FlatOut();

            var cornersBySlope = corners.OrderBy(corner => (playerPos.z - corner.z) / (playerPos.x - corner.x)).ToList();
            var playerToCenterVector = (Center - playerPos).normalized;
            var playerToCornerVectors = cornersBySlope.Select(corner => corner - playerPos).ToList();
            var cornerAndItsVectorPairs =
                from c in cornersBySlope
                join v in playerToCornerVectors 
                on cornersBySlope.IndexOf(c) equals playerToCornerVectors.IndexOf(v)
                select new { corner = c, vector = v };

            List<Vector3> viableCorners = new List<Vector3>(){
                cornerAndItsVectorPairs
                    .Take(2)
                    .OrderBy(p =>
                        Vector3.Cross(p.vector, playerToCenterVector).magnitude / p.vector.magnitude)
                    .Last().corner,
                cornerAndItsVectorPairs
                    .Skip(2)
                    .OrderBy(p =>
                        Vector3.Cross(p.vector, playerToCenterVector).magnitude / p.vector.magnitude)
                    .Last().corner,
            };

            var normalVectors = viableCorners.Select(corner => (corner - playerPos).normalized);
            var hittableSpots = normalVectors.Select(v => playerPos + v * range).ToArray();

            return hittableSpots;
        }
    }
}

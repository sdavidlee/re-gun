using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.RootPathfinding;
using Assets.Fundamentals;
using Assets.Fundamentals.Extensions;
using Assets.EnemyAI;

namespace Assets.RootPathfinding
{
    public class RootPathFinding : MonoBehaviour
    {
        public List<Node> Path { get; private set; } = new List<Node>();
        public Transform TargetPos { get; set; }
        private PathfindingGrid grid;

        private EnemyRangedActions RangedAction { get; set; }

        private void Awake()
        {
            grid = FindObjectOfType<PathfindingGrid>();
            RangedAction = GetComponent<EnemyRangedActions>();
        }

        public void FindPath(Vector3 startingPos, Vector3 targetPos)
        {
            this.Path.Clear();

            Node startNode = grid.FromWorldToNode(startingPos);
            Node targetNode = grid.FromWorldToNode(targetPos);

            Heap<Node> openNodes = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedNodes = new HashSet<Node>();
            openNodes.Add(startNode);

            while (openNodes.Count > 0){
                Node currentNode = openNodes.RemoveFirst();
                closedNodes.Add(currentNode);
                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }
                
                foreach (Node neighbor in grid.GetNeighboringNodes(currentNode)){
                    if (!neighbor.walkable || closedNodes.Contains(neighbor))
                        continue;

                    int costToNeighbor = currentNode.gCost + GetDistanceBetweenNodes(currentNode, neighbor);
                    if (costToNeighbor < neighbor.gCost || !openNodes.Contains(neighbor)){
                        neighbor.gCost = costToNeighbor;
                        neighbor.hCost = GetDistanceBetweenNodes(neighbor, targetNode);
                        neighbor.Parent = currentNode;

                        if (!openNodes.Contains(neighbor))
                            openNodes.Add(neighbor);
                    }
                }
            }
        }

        public void RetracePath(Node startNode, Node endNode)
        {
            var currentNode = endNode;
            while (currentNode != startNode){
                this.Path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            this.Path.Reverse();
            this.grid.Path = this.Path;
        }

        public List<(Vector3 direction, Vector3 destination)> GetPathVectors()
        {
            var pathVectors = new List<(Vector3 dir, Vector3 dest)>();
            if (Path.Count > 0){
                pathVectors.Add((dir: (Path[0].worldPosition - transform.position).FlatOut(), dest: Path[0].worldPosition));

                for (int i = 0; i < Path.Count -1; i++){
                    var pathTuple =
                        (dir: (Path[i + 1].worldPosition - Path[i].worldPosition).FlatOut(),
                         dest: Path[i + 1].worldPosition);
                    pathVectors.Add(pathTuple);
                }
            }
            return pathVectors;
        }

        private int GetDistanceBetweenNodes(Node nodeA, Node nodeB)
        {
            int distanceX = Mathf.Abs(nodeB.GridCoordinateX - nodeA.GridCoordinateX);
            int distanceY = Mathf.Abs(nodeB.GridCoordinateY - nodeA.GridCoordinateY);

            return 14 * Mathf.Min(distanceX, distanceY) + 10 * (Mathf.Max(distanceX, distanceY) - Mathf.Min(distanceX, distanceY));
        }
    }
}

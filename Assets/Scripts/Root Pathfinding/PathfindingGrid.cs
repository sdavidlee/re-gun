using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.RootPathfinding
{
    public class PathfindingGrid : MonoBehaviour
    {
        public LayerMask unWalkableMask;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;
        public Transform player;

        public List<Node> Path { get; set; } = new List<Node>();
        public int MaxSize { get => gridSizeX * gridSizeY; }

        private Node[,] grid;
        private float nodeDiameter;
        private int gridSizeX, gridSizeY;

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1f, gridWorldSize.y));
        //    if (grid != null){
        //        foreach (Node node in grid){
        //            Gizmos.color = (node.walkable) ? Color.blue : Color.red;
        //            if (Path.Count > 0){
        //                if (Path.Contains(node))
        //                    Gizmos.color = Color.black;
        //            }
        //            Gizmos.DrawCube(center: node.worldPosition, size: Vector3.one * (nodeDiameter - 0.1f));
        //        }
        //    }
        //}

        private void Awake()
        {
            nodeDiameter = 2 * nodeRadius;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * (gridWorldSize.x / 2) - Vector3.forward * (gridWorldSize.y / 2);

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPosition = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool walkable = !Physics.CheckSphere(worldPosition, nodeRadius, unWalkableMask);
                    grid[x, y] = new Node(walkable, worldPosition, x, y);
                }
            }
        }

        public List<Node> GetNeighboringNodes(Node currentNode)
        {
            List<Node> neighbors = new List<Node>();

            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = currentNode.GridCoordinateX + x;
                    int checkY = currentNode.GridCoordinateY + y;

                    if (checkX >= 0 &&
                        checkX < gridSizeX &&
                        checkY >= 0 &&
                        checkY < gridSizeY)
                            neighbors.Add(grid[checkX, checkY]);
                }

            return neighbors;
        }

        public Node FromWorldToNode(Vector3 worldPos)
        {
            float percentageX = worldPos.x / gridSizeX;
            float percentageY = worldPos.z / gridSizeY;

            percentageX = Mathf.Clamp01(percentageX);
            percentageY = Mathf.Clamp01(percentageY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentageX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentageY);

            return grid[x, y];
        }
    }
}


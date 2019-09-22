using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Fundamentals;

namespace Assets.RootPathfinding
{
    public class Node : IHeapItem<Node>
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int GridCoordinateX { get; private set; }
        public int GridCoordinateY { get; private set; }
        public int gCost { get; set; }
        public int hCost { get; set; }
        public int fCost { get => gCost + hCost; }
        public Node Parent { get; set; }
        public int HeapIndex { get; set; }
        public int OccupantCount { get; set; }

        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY )
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
            this.GridCoordinateX = gridX;
            this.GridCoordinateY = gridY;
        }

        public int CompareTo(Node nodeToCompareTo)
        {
            int compare = fCost.CompareTo(nodeToCompareTo.fCost);
            if (compare == 0)
                compare = hCost.CompareTo(nodeToCompareTo.hCost);

            return -compare;
        }
    }
}

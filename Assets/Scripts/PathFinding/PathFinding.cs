using System.Collections.Generic;
using System.Linq;
using Grid;
using UnityEngine;
using Utils;

namespace PathFinding
{
    public class PathFindingHelper
    {
        const int MOVE_STRAIGHT_COST = 10;
        const int MOVE_DIAG_COST = 14;
        
        public Grid2D<PathNode> Grid;
        private List<PathNode> openList;
        private List<PathNode> closedList;

        public PathFindingHelper(int height, int width, Vector2 position, float cellSize)
        {
            Grid = new Grid2D<PathNode>(height, width, cellSize, position, ((grid2D, x, y) => new PathNode(grid2D, x, y)));
        }

        public List<PathNode> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            var startXY = this.Grid.WorldPositionToGridXY(startWorldPosition);
            var endXY = this.Grid.WorldPositionToGridXY(endWorldPosition);

            return this.FindPath(startXY.x, startXY.y, endXY.x, endXY.y);
        }
        
        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            var startNode = Grid.GetGridObjectAt(startX, startY);
            var endNode = Grid.GetGridObjectAt(endX, endY);

            openList = new List<PathNode>() {startNode};
            closedList = new List<PathNode>();

            for (int x = 0; x < Grid.Width; x++)
            {
                for (int y = 0; y < Grid.Height; y++)
                {
                    var node = Grid.GetGridObjectAt(x, y);
                    node.GCost = int.MaxValue;
                    node.CalculateFCost();
                    node.CameFromNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistance(startNode, endNode);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                var currentNode = GetNodeWithLowestFCost(openList);
                if (currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                var neighbourList = GetNeighbourList(currentNode);

                foreach (var neighborNode in neighbourList)
                {
                    if (closedList.Contains(neighborNode)) continue;
                    if (!neighborNode.IsWalkable)
                    {
                        closedList.Add(neighborNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost + CalculateDistance(currentNode, neighborNode);
                    if (tentativeGCost < neighborNode.GCost)
                    {
                        neighborNode.CameFromNode = currentNode;
                        neighborNode.GCost = tentativeGCost;
                        neighborNode.HCost = CalculateDistance(neighborNode, endNode);
                        neighborNode.CalculateFCost();

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }
            }
            
            // Out of nodes on the open list
            return new List<PathNode>();
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            var neighbourList = new List<PathNode>();
            if (currentNode.x - 1 >= 0)
            {
                neighbourList.Add(Grid.GetGridObjectAt(currentNode.x - 1, currentNode.y));
                
                if(currentNode.y - 1 >= 0)
                    neighbourList.Add(Grid.GetGridObjectAt(currentNode.x - 1, currentNode.y - 1));
                
                if(currentNode.y + 1 < Grid.Height)
                    neighbourList.Add(Grid.GetGridObjectAt(currentNode.x - 1, currentNode.y + 1));
            }
            
            if (currentNode.x + 1 < Grid.Width)
            {
                neighbourList.Add(Grid.GetGridObjectAt(currentNode.x + 1, currentNode.y));
                
                if(currentNode.y - 1 >= 0)
                    neighbourList.Add(Grid.GetGridObjectAt(currentNode.x + 1, currentNode.y - 1));
                
                if(currentNode.y + 1 < Grid.Height)
                    neighbourList.Add(Grid.GetGridObjectAt(currentNode.x + 1, currentNode.y + 1));
            }
            
            if(currentNode.y - 1 >= 0)
                neighbourList.Add(Grid.GetGridObjectAt(currentNode.x, currentNode.y - 1));
            
            if(currentNode.y + 1 < Grid.Height)
                neighbourList.Add(Grid.GetGridObjectAt(currentNode.x, currentNode.y + 1));

            return neighbourList.MixRandomly().ToList();
        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            var path = new List<PathNode> {endNode};

            var currentNode = endNode.CameFromNode;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.CameFromNode;
            }

            return path;
        }
        
        private PathNode GetNodeWithLowestFCost(List<PathNode> nodeList)
        {
            var lowestFCostNode = nodeList[0];

            foreach (var node in nodeList)
            {
                if (node.FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = node;
                }
            }

            return lowestFCostNode;
        }
        
        private int CalculateDistance(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);

            return (MOVE_DIAG_COST * Mathf.Min(xDistance, yDistance)) + (MOVE_STRAIGHT_COST * remaining);
        }
    }
}
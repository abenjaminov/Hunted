﻿using System.Collections.Generic;
using Grid;
using PathFinding;
using UnityEngine;

namespace Game.ScriptableObjects.GameLogic
{
    [CreateAssetMenu(fileName = "Path Finding Data", menuName = "Game Logic/Path Finding Data", order = 0)]
    public class PathFindingData : ScriptableObject
    {
        private PathFindingHelper _pathFinding;

        [SerializeField] private int Width;
        [SerializeField] private int Height;
        [SerializeField] private Vector2 Position;
        [SerializeField] private float CellSize;

        private void OnEnable()
        {
            _pathFinding = new PathFindingHelper(Height, Width, Position, CellSize);
        }

        public void SetVisuals(IGridVisuals<PathNode> gridVisuals)
        {
            _pathFinding.Grid.SetVisuals(gridVisuals);
        }
        
        public void SetNonWalkableArea(Vector3 centerWorldPosition, float width, float height)
        {
            var xIndex = 0;
            int yIndex;
            var xProgress = xIndex * CellSize;
            float yProgress;

            var worldPosition = centerWorldPosition - new Vector3(width / 2, height / 2);

            while (xProgress <= width + CellSize)
            {
                yIndex = 0;
                yProgress = yIndex * CellSize;
                while (yProgress <= height + CellSize)
                {
                    var gridPosition = _pathFinding.Grid.WorldPositionToGridXY(worldPosition + new Vector3(xProgress, yProgress,0));
                    var currentNode = _pathFinding.Grid.GetGridObjectAt(gridPosition.x, gridPosition.y);
                    currentNode.SetIsWalkable(false);
                    yIndex++;
                    yProgress = yIndex * CellSize;
                }

                xIndex++;
                xProgress = xIndex * CellSize;
            }
        }
        
        public List<Vector2> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            var nodes = _pathFinding.FindPath(startWorldPosition, endWorldPosition);
            List<Vector2> path = new List<Vector2>();
            
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                var node = nodes[i];
                var nextNode = nodes[i + 1];
            
                var firstNodeWorldPosition = _pathFinding.Grid.GetWorldPosition(node.x, node.y);
                var secondNodeWorldPosition = _pathFinding.Grid.GetWorldPosition(nextNode.x, nextNode.y);
            
                Debug.DrawLine(firstNodeWorldPosition, secondNodeWorldPosition, Color.red, 5);
            }
            
            foreach (var node in nodes)
            {
                var nodeWorldPosition = _pathFinding.Grid.GetWorldPosition(node.x, node.y);
                path.Add(nodeWorldPosition);    
            }

            return path;
        }

        public Vector2 WorldPositionToGridXY(Vector3 worldPosition)
        {
            return _pathFinding.Grid.WorldPositionToGridXY(worldPosition);
        }

        public bool IsWalkable(Vector3 worldPosition)
        {
            var gridPosition = _pathFinding.Grid.WorldPositionToGridXY(worldPosition);
            var gridObject = _pathFinding.Grid.GetGridObjectAt(gridPosition.x, gridPosition.y);

            return gridObject.IsWalkable;
        }
    }
}
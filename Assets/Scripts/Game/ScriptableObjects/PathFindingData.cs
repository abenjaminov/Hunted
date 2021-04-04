using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PathFinding;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Path Finding Data", menuName = "Game Logic/Path Finding Data", order = 0)]
    public class PathFindingData : ScriptableObject
    {
        private PathFindingHelper _pathFinding;

        [SerializeField] private int Width;
        [SerializeField] private int Height;
        [SerializeField] private Vector2 Position;
        [SerializeField] private float CellSize;

        private static int count;
        
        private void OnEnable()
        {
            _pathFinding = new PathFindingHelper(Height, Width, Position, CellSize);
        }


        public List<Vector2> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            var nodes = _pathFinding.FindPath(startWorldPosition, endWorldPosition);
            List<Vector2> path = new List<Vector2>();
            
            foreach (var node in nodes)
            {
                var nodeWorldPosition = _pathFinding.Grid.GetWorldPosition(node.x, node.y);
                path.Add(nodeWorldPosition);    
            }

            return path;
        }
    }
}
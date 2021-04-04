using System;
using System.Linq;
using PathFinding;
using UnityEngine;

namespace Grid
{
    public class GridTest : MonoBehaviour, IGridVisuals<PathNode>
    {
        private PathFinding.PathFindingHelper pathFinding;
        private void Start()
        {
            var enemyGridPosition = new Vector2(-20, -15);

            pathFinding = new PathFinding.PathFindingHelper(60, 80, enemyGridPosition, .5f);
            pathFinding.Grid.SetVisuals(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                print(mouseWorldPosition);
                var xyPosition = pathFinding.Grid.WorldPositionToGridXY(mouseWorldPosition);
                var path = pathFinding.FindPath(0, 0, xyPosition.x, xyPosition.y);
                
                if (path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        var node = path[i];
                        var nextNode = path[i + 1];

                        var firstNodeWorldPosition = pathFinding.Grid.GetWorldPosition(node.x, node.y);
                        var secondNodeWorldPosition = pathFinding.Grid.GetWorldPosition(nextNode.x, nextNode.y);

                        Debug.DrawLine(firstNodeWorldPosition, secondNodeWorldPosition, Color.red, 50f);
                    }
                }
            }
        }

        private void DrawGrid(Grid2D<PathNode> gridToDraw)
        {
            for (int i = 0; i <= gridToDraw.Width; i++)
            {
                Debug.DrawLine(gridToDraw.GetWorldPosition(i, 0), 
                    gridToDraw.GetWorldPosition(i, gridToDraw.Height),
                    Color.white,100f);
            }

            for (int i = 0; i <= gridToDraw.Height; i++)
            {
                Debug.DrawLine(gridToDraw.GetWorldPosition(0, i), 
                    gridToDraw.GetWorldPosition(gridToDraw.Width, i),
                    Color.white,100f);
            }
        }

        public void VisualizeGrid(Grid2D<PathNode> grid)
        {
            //DrawGrid(grid);
        }
    }
}
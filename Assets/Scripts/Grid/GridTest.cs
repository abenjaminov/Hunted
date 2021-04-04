using System;
using System.Linq;
using Game.ScriptableObjects;
using PathFinding;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    public class GridTest : MonoBehaviour, IGridVisuals<PathNode>
    {
        public PathFindingData pathFindingData;
        
        private void Start()
        {
            pathFindingData.SetVisuals(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var xyPosition = pathFindingData.WorldPositionToGridXY(mouseWorldPosition);
                Debug.Log("Mouse position on Grid : " + xyPosition);
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

            for (int i = 0; i < gridToDraw.Width; i++)
            {
                for (int j = 0; j < gridToDraw.Height; j++)
                {
                    var node = gridToDraw.GetGridObjectAt(i, j);
                    if (!node.IsWalkable)
                    {
                        var cellSizeHeight = new Vector3(0, gridToDraw.CellSize);
                        var cellSizeWidth = new Vector3(gridToDraw.CellSize, 0);
                        var worldPosition = gridToDraw.GetWorldPosition(i, j);
                        Debug.DrawLine(worldPosition, worldPosition + cellSizeHeight + cellSizeWidth, Color.blue, 100f);
                        Debug.DrawLine(worldPosition + cellSizeHeight + cellSizeWidth, worldPosition + cellSizeWidth, Color.blue, 100f);
                        Debug.DrawLine(worldPosition + cellSizeWidth, worldPosition + cellSizeHeight, Color.blue, 100f);
                        Debug.DrawLine(worldPosition + cellSizeWidth, worldPosition, Color.blue, 100f);
                    }
                }
            }
        }

        public void VisualizeGrid(Grid2D<PathNode> grid)
        {
            DrawGrid(grid);
        }
    }
}
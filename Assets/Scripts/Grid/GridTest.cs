using System;
using System.Linq;
using Game.ScriptableObjects;
using Game.ScriptableObjects.GameLogic;
using PathFinding;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

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
            var halfCellSize = Vector2.one.To3D() * (gridToDraw.CellSize / 2);
            
            for (int i = 0; i <= gridToDraw.Width; i++)
            {
                Debug.DrawLine(gridToDraw.GetWorldPosition(i, 0) - halfCellSize, 
                    gridToDraw.GetWorldPosition(i, gridToDraw.Height) - halfCellSize,
                    Color.white,100f);
            }
            
            for (int i = 0; i <= gridToDraw.Height; i++)
            {
                Debug.DrawLine(gridToDraw.GetWorldPosition(0, i) - halfCellSize, 
                    gridToDraw.GetWorldPosition(gridToDraw.Width, i) - halfCellSize,
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
                        Debug.Log("(" + i + ", " + j + ")");
                        Debug.DrawLine(worldPosition - halfCellSize, worldPosition + cellSizeHeight + cellSizeWidth - halfCellSize, Color.blue, 100f);
                        Debug.DrawLine(worldPosition + cellSizeHeight + cellSizeWidth - halfCellSize, worldPosition + cellSizeWidth - halfCellSize, Color.blue, 100f);
                        Debug.DrawLine(worldPosition + cellSizeWidth - halfCellSize, worldPosition + cellSizeHeight - halfCellSize, Color.blue, 100f);
                        Debug.DrawLine(worldPosition + cellSizeWidth - halfCellSize, worldPosition - halfCellSize, Color.blue, 100f);
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
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Grid
{
    public class Grid2D<T> where T : class, IGrid2DObject
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float CellSize { get; private set; }
        
        private readonly T[,] _grid;

        public Vector2 Position { get; private set; }
        private IGridVisuals<T> _visuals;
        
        public Grid2D(int height, 
            int width, 
            float cellSize, Vector2 position,
            Func<Grid2D<T>, int, int, T> createGridObject)
        {
            Height = height;
            Width = width;
            _grid = new T[height, width];
            CellSize = cellSize;

            Position = position;
            
            InitializeGrid(createGridObject);
        }

        private void InitializeGrid(Func<Grid2D<T>, int, int, T> createGridObject)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _grid[i, j] = createGridObject(this, j, i);
                    _grid[i,j].OnObjectChanged += OnOnObjectChanged;
                }
            }
        }

        private void OnOnObjectChanged(IGrid2DObject changedGridObject)
        {
            _visuals?.VisualizeGrid(this);
        }

        public void SetVisuals(IGridVisuals<T> visuals)
        {
            _visuals = visuals;
            
            _visuals.VisualizeGrid(this);
        }

        public T GetGridObjectAt(int x, int y)
        {
            return _grid[y, x];
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(Position.x + (x * CellSize), Position.y + (y * CellSize));
        }

        public Vector2Int WorldPositionToGridXY(Vector2 worldPosition)
        {
            var localPosition = (worldPosition - Position).Abs();
            var localPositionRemainder = new Vector2(localPosition.x % CellSize, localPosition.y % CellSize);

            var localGridPosition = localPosition - localPositionRemainder;

            var roundedLocalPosition = localGridPosition / CellSize;

            return roundedLocalPosition.ToInt();
        }
    }
}
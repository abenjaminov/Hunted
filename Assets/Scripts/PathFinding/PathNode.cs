using Grid;
using UnityEngine.Events;

namespace PathFinding
{
    public class PathNode : IGrid2DObject
    {
        public event UnityAction<IGrid2DObject> OnObjectChanged;
        
        private Grid2D<PathNode> _grid;
        public int x;
        public int y;
        
        public int GCost;
        public int HCost;
        public int FCost;
        
        public PathNode CameFromNode;

        public bool IsWalkable;
        
        public PathNode(Grid2D<PathNode> grid, int x, int y)
        {
            this.x = x;
            this.y = y;
            _grid = grid;
            IsWalkable = true;
        }

        public void CalculateFCost()
        {
            FCost = GCost + HCost;
        }

        public void SetIsWalkable(bool isWalkable)
        {
            this.IsWalkable = isWalkable;
            this.OnObjectChanged?.Invoke(this);
        }
    }
}
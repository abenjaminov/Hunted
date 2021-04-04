using Grid;

namespace PathFinding
{
    public class PathNode
    {
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
    }
}
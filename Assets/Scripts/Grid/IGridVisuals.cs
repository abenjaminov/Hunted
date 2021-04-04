namespace Grid
{
    public interface IGridVisuals<T> where T : class, IGrid2DObject
    {
        void VisualizeGrid(Grid2D<T> grid);
    }
}
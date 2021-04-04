namespace Grid
{
    public interface IGridVisuals<T> where T : class
    {
        void VisualizeGrid(Grid2D<T> grid);
    }
}
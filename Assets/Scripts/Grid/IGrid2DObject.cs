using UnityEngine.Events;

namespace Grid
{
    public interface IGrid2DObject
    {
        event UnityAction<IGrid2DObject> OnObjectChanged;
    }
}
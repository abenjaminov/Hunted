using UnityEngine;

namespace Game
{
    public class Shot : MonoBehaviour, IOutOfBoundsSubscriber
    {
        [SerializeField] private float _speed = 1;
        [HideInInspector] public Vector3 Direction;
    
        void Update()
        {
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));        
        }

        public void OnOutOfBounds(Bounds bounds)
        {
            Destroy(this.gameObject);
        }
    }
}

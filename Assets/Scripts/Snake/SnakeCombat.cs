using Game;
using UnityEngine;

namespace Snake
{
    public class SnakeCombat : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject shotPrefab;

        private Vector3 directionToMouse;

        void Update()
        {
            directionToMouse = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
            directionToMouse.z = 0;
            var angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        
            transform.localEulerAngles = new Vector3(0,0,angle);

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            var gameObject = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            gameObject.transform.localEulerAngles = new Vector3(0, 0, angle);
        
            var shot = gameObject.GetComponent<Shot>();
            shot.Direction = directionToMouse;
        }
    }
}

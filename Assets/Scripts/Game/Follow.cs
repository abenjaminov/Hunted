using System;
using Game.Enemies;
using UnityEngine;

namespace Game
{
    public class Follow : MonoBehaviour
    {
        [HideInInspector] private GameObject _objectToFollow;
        [SerializeField] private float _speed;

        // TODO : Remove this since it is not clean
        private EnemyFactory _enemyFactory;

        private void Awake()
        {
            _enemyFactory = GameObject.FindObjectOfType<EnemyFactory>();
        }

        private void Update()
        {
            if (_objectToFollow == null)
            {
                _objectToFollow = _enemyFactory.GetRandomSnakeBodyPart();
            }
            
            var direction = _objectToFollow.transform.position - transform.position;
            
            transform.Translate(direction.normalized * (_speed * Time.deltaTime));
        }

        public void FollowObject(GameObject objectToFollow)
        {
            _objectToFollow = objectToFollow;
        }
    }
}

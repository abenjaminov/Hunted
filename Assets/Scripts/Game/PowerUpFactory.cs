using System;
using System.Collections.Generic;
using Channels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class PowerUpFactory : MonoBehaviour
    {
        private Bounds _bounds;
        
        [SerializeField] private List<GameObject> _powerUpPrefabs;
        [SerializeField] private GameChannel _gameChannel;

        [SerializeField] private float _timeBetweenSpawns;
        private float _timeToNextSpawn;
        
        private void Awake()
        {
            var collider2D = GetComponent<Collider2D>();
            _bounds = collider2D.bounds;
            
            _gameChannel.collectedEvent += CollectedEvent;
        }

        private void Start()
        {
            SpawnCollectable();
        }

        private void CollectedEvent(Collectable collectable)
        {
            SpawnCollectable();
        }

        private void SpawnCollectable()
        {
            var randomX = Random.Range(_bounds.min.x + 5, _bounds.max.x - 5);
            var randomY = Random.Range(_bounds.min.y +2, _bounds.max.y - 2);

            var index = Random.Range(0, _powerUpPrefabs.Count);
            
            Instantiate(_powerUpPrefabs[index], new Vector3(randomX, randomY, 0), Quaternion.identity);

            _timeToNextSpawn = _timeBetweenSpawns;
        }

        private void Update()
        {
            _timeToNextSpawn -= Time.deltaTime;

            if (_timeToNextSpawn <= 0)
            {
                SpawnCollectable();
            }
        }
    }
}

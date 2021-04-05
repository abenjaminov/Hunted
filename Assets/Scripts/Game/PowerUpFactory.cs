using System;
using System.Collections.Generic;
using Channels;
using Game.ScriptableObjects;
using Game.ScriptableObjects.GameLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class PowerUpFactory : MonoBehaviour
    {
        private Bounds _bounds;
        [SerializeField] private PathFindingData _pathFindingData;

        [SerializeField] private List<GameObject> _powerUpPrefabs;
        [SerializeField] private GameChannel _gameChannel;

        [SerializeField] private float _timeBetweenSpawns;
        private float _timeToNextSpawn;
        private float _timeToNextSpawnAfterFailed = .075f;
        
        private void Awake()
        {
            var collider2D = GetComponent<Collider2D>();
            _bounds = collider2D.bounds;
            
            _gameChannel.collectedEvent += CollectedEvent;
        }

        private void Start()
        {
            TrySpawnCollectable();
        }

        private void CollectedEvent(Collectable collectable)
        {
            TrySpawnCollectable();
        }

        private void TrySpawnCollectable()
        {
            var randomX = Random.Range(_bounds.min.x + 5, _bounds.max.x - 5);
            var randomY = Random.Range(_bounds.min.y +2, _bounds.max.y - 2);

            var spawnPosition = new Vector3(randomX, randomY, 0);

            if (!_pathFindingData.IsWalkable(spawnPosition))
            {
                _timeToNextSpawn = _timeToNextSpawnAfterFailed;
            }
            else
            {
                SpawnRandomCollectable(spawnPosition);
                
                _timeToNextSpawn = _timeBetweenSpawns;
            }
        }

        private void SpawnRandomCollectable(Vector3 spawnPosition)
        {
            var index = Random.Range(0, _powerUpPrefabs.Count);
            
            Instantiate(_powerUpPrefabs[index], spawnPosition, Quaternion.identity);
        }

        private void Update()
        {
            _timeToNextSpawn -= Time.deltaTime;

            if (_timeToNextSpawn <= 0)
            {
                TrySpawnCollectable();
            }
        }
    }
}

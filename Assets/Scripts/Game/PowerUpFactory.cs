using System;
using System.Collections.Generic;
using Channels;
using Game.ScriptableObjects;
using Game.ScriptableObjects.GameLogic;
using Levels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class PowerUpFactory : MonoBehaviour
    {
        [SerializeField] private LevelChannel _levelChannel;
        private Bounds _bounds;
        [SerializeField] private PathFindingData _pathFindingData;

        [SerializeField] private List<GameObject> _powerUpPrefabs;
        [SerializeField] private GameChannel _gameChannel;

        [SerializeField] private float _timeBetweenSpawns;
        private float _timeToNextSpawn;
        private float _timeToNextSpawnAfterFailed = .075f;

        private bool IsSpawningPowerups;
        private List<Collectable> _allPowerups;
        
        private void Awake()
        {
            var collider2D = GetComponent<Collider2D>();
            _bounds = collider2D.bounds;
            
            _gameChannel.collectedEvent += CollectedEvent;
        }

        private void Start()
        {
            _levelChannel.levelChangingEvent += LevelChangedEvent;
            _levelChannel.levelCompletedEvent += LevelCompletedEvent;
        }

        private void Update()
        {
            if (!IsSpawningPowerups) return;
            
            _timeToNextSpawn -= Time.deltaTime;

            if (_timeToNextSpawn <= 0)
            {
                TrySpawnCollectable();
            }
        }
        
        private void LevelCompletedEvent(Level arg0, Level arg1)
        {
            IsSpawningPowerups = false;
        }
        
        private void LevelChangedEvent(Level oldLevel, Level newLevel)
        {
            IsSpawningPowerups = newLevel.Data.SpawnPowerups;
            _powerUpPrefabs = newLevel.Data.PowerupPrefabs;
        }

        private void CollectedEvent(Collectable collectable)
        {
            TrySpawnCollectable();
        }

        private void TrySpawnCollectable()
        {
            var randomX = Random.Range(0, _pathFindingData.Width);
            var randomY = Random.Range(0, _pathFindingData.Height);

            var spawnPosition = 
                _pathFindingData.GridXYToWorldPosition(randomX, randomY);

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
    }
}

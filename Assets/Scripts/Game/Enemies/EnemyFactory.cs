using System;
using System.Collections.Generic;
using System.Linq;
using Channels;
using Game.ScriptableObjects.GameLogic;
using Levels;
using Snake;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private LevelChannel _levelChannel;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<Transform> _spawnLocations;
        [SerializeField] private PathFindingData _pathFindingData;
        [SerializeField] private float _timeBetweenSpawns;
        private float _timeToNextSpawn;

        private int enemyNumber = 1;
        private bool IsSpawningEnemies;
        
        private void Awake()
        {
            _levelChannel.levelChangedEvent += LevelChangedEvent;
        }

        private void LevelChangedEvent(Level oldLevel, Level newLevel)
        {
            IsSpawningEnemies = newLevel.Data.SpawnEnemies;

            var spawnCircleRadius = Mathf.Min(newLevel.Data.GameZoneSize.x / 2, newLevel.Data.GameZoneSize.y / 2) * 0.9f;
            var angleBetweenSpawnLocations = (360f / _spawnLocations.Count) * Mathf.Deg2Rad;
            
            for (int i = 0; i < _spawnLocations.Count; i++)
            {
                var currentAngle = angleBetweenSpawnLocations * i;
                var xDistance = Mathf.Cos(currentAngle) * spawnCircleRadius;
                var yDistance = Mathf.Sin(currentAngle) * spawnCircleRadius;
            
                _spawnLocations[i].transform.position = new Vector3(xDistance, yDistance, 0);
            }
        }

        private void Update()
        {
            if (!IsSpawningEnemies) return;
            
            _timeToNextSpawn -= Time.deltaTime;
            
            if (_timeToNextSpawn <= 0)
            {
                SpawnEnemy();
                _timeToNextSpawn = _timeBetweenSpawns;
            }
        }

        private void SpawnEnemy()
        {
            var randomTransform = _spawnLocations[Random.Range(0, _spawnLocations.Count)];

            var newEnemy = Instantiate(_enemyPrefab, randomTransform.position, Quaternion.identity);

            // Debug
            newEnemy.name = "Enemy No. " + enemyNumber;
            enemyNumber++;
            
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            enemyAI.SetPathFindingData(_pathFindingData);
            
            var followObject = this.GetRandomSnakeBodyPart();
            enemyAI.FollowObject(followObject.gameObject);
        }

        public GameObject GetRandomSnakeBodyPart()
        {
            var snakeBodyParts = GameObject.FindObjectsOfType<SnakeBodyPart>().ToList();
            SnakeBodyPart snakeHead = GameObject.FindObjectOfType<SnakeHead>();
            
            snakeBodyParts.Add(snakeHead);

            int randomFollowIndex = Random.Range(0, snakeBodyParts.Count);
            return snakeBodyParts[randomFollowIndex].gameObject;
        }
    }
}

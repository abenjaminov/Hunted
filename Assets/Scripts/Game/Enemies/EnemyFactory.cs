using System;
using System.Collections.Generic;
using System.Linq;
using Snake;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<Transform> _spawnLocations;

        [SerializeField] private float _timeBetweenSpawns;
        private float _timeToNextSpawn;

        private int enemyNumber = 1;

        private void Update()
        {
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

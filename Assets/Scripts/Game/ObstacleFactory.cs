using System;
using System.Collections.Generic;
using Channels;
using Game.ScriptableObjects.GameLogic;
using Levels;
using UnityEngine;
using Utils;

namespace Game
{
    [CreateAssetMenu(fileName="Obstacle Factory",menuName = "Obstacles/Obstacle Factory",order = 0)]
    public class ObstacleFactory : ScriptableObject
    {
        private static ObstacleFactory singleFactory;

        [SerializeField] private LayerMask _sortingLayer;
        [SerializeField] private PathFindingData _pathFindingData;
        [SerializeField] private LevelChannel _levelChannel;

        private List<GameObject> _allObstacles;
        
        private void OnEnable()
        {
            if (singleFactory == null)
            {
                singleFactory = this;
                _levelChannel.levelChangedEvent += LevelChangedEvent;
            }
            else
            {
                Debug.LogError("This is not the first obstacle factory created in the game, please note that there is no need for more than one.");
            }
        }

        private void LevelChangedEvent(Level oldLevel, Level newLevel)
        {
            _pathFindingData.InitializePathFinding(newLevel.Data.GameZoneSize.x,newLevel.Data.GameZoneSize.y);
            
            for (int i = 0; i < _allObstacles.Count; i++)
            {
                Destroy(_allObstacles[i]);
            }

            foreach (var obstacle in newLevel.Data.Obstacles)
            {
                PlaceObstacle(obstacle);
            }
        }

        private void PlaceObstacle(Obstacle obstacle)
        {
            GameObject obstacleGameObject = new GameObject("obstacle");
            var renderer = obstacleGameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = obstacle.Sprite;

            renderer.sortingLayerName = "Game";
            obstacleGameObject.transform.localScale = new Vector3(obstacle.Width, obstacle.Height, 1);
            obstacleGameObject.transform.position = obstacle.Position;
            
            _pathFindingData.SetNonWalkableArea(obstacle.Position, obstacle.Width, obstacle.Height);
            
            _allObstacles.Add(obstacleGameObject);
        }
    }
}
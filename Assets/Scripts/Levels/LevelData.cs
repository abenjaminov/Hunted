using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", 
        menuName = "Levels/Level Data", 
        order = 0)]
    public class LevelData : ScriptableObject
    {
        public string name;
        [TextArea] public string description;
        public Vector2Int GameZoneSize;
        public LevelData NextLevel;
        public List<LevelObjectiveData> objectivesData;
        
        [Header("Enemies")]
        public bool SpawnEnemies;
        
        [Header("Power ups")]
        public bool SpawnPowerups;
        public List<GameObject> PowerupPrefabs;

        [Header("Obstacles")] 
        public ObstacleFactory ObstacleFactory;
        public List<Obstacle> Obstacles;
    }
}
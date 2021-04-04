using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", 
        menuName = "Levels/Level Data", 
        order = 0)]
    public class LevelData : ScriptableObject
    {
        public string name;
        public List<LevelObjectiveData> objectivesData;
    }
}
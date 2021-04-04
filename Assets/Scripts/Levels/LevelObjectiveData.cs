using System;
using Channels;
using Game.Enemies;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelObjectiveData", menuName = "Levels/Objective Data", order = 1)]
    public class LevelObjectiveData : ScriptableObject
    {
        public LevelType levelType;
        public LevelObjectiveEntityType objectType;
        public int amount;
    }
}
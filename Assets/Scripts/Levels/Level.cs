using System.Collections.Generic;
using Channels;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class Level
    {
        private LevelChannel _levelChannel;
        private GameChannel _gameChannel;
        
        public readonly LevelData Data;
        private List<LevelObjective> _objectives = new List<LevelObjective>();

        private int completedObjectivesCount;

        public UnityAction<Level> levelCompletedEvent;
        
        public Level(LevelData levelData, LevelChannel levelChannel, GameChannel gameChannel)
        {
            _levelChannel = levelChannel;
            _gameChannel = gameChannel;
            Data = levelData;
            
            foreach (var objectiveData in Data.objectivesData)
            {
                var levelObjective = new LevelObjective(objectiveData, levelChannel, gameChannel);
                levelObjective.objectiveCompletedEvent += ObjectiveCompleted;
                _objectives.Add(levelObjective);
            }
        }

        private void ObjectiveCompleted(LevelObjective objective)
        {
            completedObjectivesCount += 1;
            objective.objectiveCompletedEvent -= ObjectiveCompleted;
            
            if (completedObjectivesCount == _objectives.Count)
            {
                CompleteLevel();
            }
        }

        private void CompleteLevel()
        {
            OnLevelCompleted();
        }
        
        private void OnLevelCompleted() 
        {
            levelCompletedEvent?.Invoke(this);
        }
    }
}
using Levels;
using UnityEngine;
using UnityEngine.Events;

namespace Channels
{
    [CreateAssetMenu(fileName = "LevelChannel", menuName = "Channels/Level Channel", order = 0)]
    public class LevelChannel : ScriptableObject
    {
        public UnityAction<Level, Level> levelCompletedEvent;
        public UnityAction<LevelObjective> levelObjectiveComplete;
        public UnityAction<Level, Level> levelChangingEvent;

        public void OnLevelCompleted(Level oldLevel, Level newLevel)
        {
            levelCompletedEvent?.Invoke(oldLevel, newLevel);
        }
        
        public void OnLevelObjectiveCompleted(LevelObjective levelObject)
        {
            levelObjectiveComplete?.Invoke(levelObject);
        }

        public void OnLevelChanging(Level oldLevel, Level newLevel)
        {
            levelChangingEvent?.Invoke(oldLevel, newLevel);
        }
    }
}
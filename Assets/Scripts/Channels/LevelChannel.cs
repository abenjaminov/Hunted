using Levels;
using UnityEngine;
using UnityEngine.Events;

namespace Channels
{
    [CreateAssetMenu(fileName = "LevelChannel", menuName = "Channels/Level Channel", order = 0)]
    public class LevelChannel : ScriptableObject
    {
        public UnityAction<Level> levelCompleteEvent;
        public UnityAction<LevelObjective> levelObjectiveComplete;
        public UnityAction<Level, Level> levelChangedEvent;

        public void OnLevelCompleted(Level level)
        {
            levelCompleteEvent?.Invoke(level);
        }
        
        public void OnLevelObjectiveCompleted(LevelObjective levelObject)
        {
            levelObjectiveComplete?.Invoke(levelObject);
        }

        public void OnLevelChanged(Level oldLevel, Level newLevel)
        {
            levelChangedEvent?.Invoke(oldLevel, newLevel);
        }
    }
}
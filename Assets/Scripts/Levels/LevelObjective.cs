using Channels;
using Game.Enemies;
using Snake;
using UnityEngine.Events;

namespace Levels
{
    public class LevelObjective
    {
        private LevelChannel _levelChannel;
        private GameChannel _gameChannel;
        
        public LevelObjectiveData Data;
        
        private int amountLeft;
        
        public UnityAction<LevelObjective> objectiveCompletedEvent;
        
        public LevelObjective(LevelObjectiveData data, LevelChannel levelChannel, GameChannel gameChannel)
        {
            _gameChannel = gameChannel;
            _levelChannel = levelChannel;
            Data = data;
            amountLeft = data.amount;
            
            switch (data.levelType)
            {
                case LevelType.Destroy when data.objectType == LevelObjectiveEntityType.Enemy:
                    _gameChannel.enemyDestroyedEvent += 
                        enemy => AddAmountLeft(-1);;
                    break;
                case LevelType.Collect when data.objectType == LevelObjectiveEntityType.Grow:
                    _gameChannel.bodyPartDestroyedEvent += 
                        bodyPArt => AddAmountLeft(1);
                    _gameChannel.bodyPartCreatedEvent += 
                        bodyPArt => AddAmountLeft(-1);
                    break;
            }
        }

        private void AddAmountLeft(int addition)
        {
            amountLeft += addition;

            if (amountLeft <= 0)
            {
                OnObjectiveCompleted();
                _levelChannel.OnLevelObjectiveCompleted(this);
            }
        }
        
        private void OnObjectiveCompleted()
        {
            objectiveCompletedEvent?.Invoke(this);
        }
    }
}
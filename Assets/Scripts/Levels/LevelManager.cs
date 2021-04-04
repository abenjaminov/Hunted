using System;
using System.Collections.Generic;
using Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelChannel levelChannel;
        [SerializeField] private GameChannel _gameChannel;
        
        [SerializeField] private List<LevelData> _allLevelsData;
        
        private Level _activeLevel;
        private int _activeLevelIndex;
        
        private void Awake()
        {
            _activeLevelIndex = -1;
            IncreaseLevel();
        }

        private void IncreaseLevel()
        {
            _activeLevelIndex++;
            
            if (_activeLevelIndex < _allLevelsData.Count)
            {
                GoToNextLevel();
            }
            else
            {
                WinGame();
            }
        }

        private void WinGame()
        {
            // TODO : Win!
        }

        private void GoToNextLevel()
        {
            _activeLevel = new Level(_allLevelsData[_activeLevelIndex], levelChannel, _gameChannel);
            _activeLevel.levelCompletedEvent += ActiveLevelCompletedEvent;
        }

        private void ActiveLevelCompletedEvent(Level completedLevel)
        {
            completedLevel.levelCompletedEvent -= ActiveLevelCompletedEvent;
            print("The Active level has been completed");

            
            IncreaseLevel();
        }
    }
}
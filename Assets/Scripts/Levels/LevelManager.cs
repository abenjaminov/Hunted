using System;
using System.Collections.Generic;
using Channels;
using UnityEngine;
using Utils;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelChannel _levelChannel;
        [SerializeField] private GameChannel _gameChannel;
        
        [SerializeField] private List<LevelData> _allLevelsData;

        private Level _activeLevel;
        private int _activeLevelIndex;
        
        private void Start()
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
            print("You won the game!");
        }

        private void GoToNextLevel()
        {
            var previousLevel = _activeLevel;
            _activeLevel = new Level(_allLevelsData[_activeLevelIndex], _levelChannel, _gameChannel);
            _activeLevel.levelCompletedEvent += ActiveLevelCompletedEvent;

            transform.localScale = _activeLevel.Data.GameZoneSize.To3D();
            
            _levelChannel.OnLevelChanged(previousLevel, _activeLevel);
        }

        private void ActiveLevelCompletedEvent(Level completedLevel)
        {
            completedLevel.levelCompletedEvent -= ActiveLevelCompletedEvent;
            print("The Active level has been completed");
            
            IncreaseLevel();
        }
    }
}
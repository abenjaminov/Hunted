﻿using System;
using System.Collections.Generic;
using Channels;
using Game.ScriptableObjects;
using UnityEngine;
using Utils;

namespace Game.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private MovementChannel _movementChannel;
        [SerializeField] private PathFindingData pathFindingData;

        private GameObject _objectToFollow;
        [SerializeField] private float timeBetweenPulses;
        [SerializeField] private float _speed;
        
        private float _timeUntillNextPulse;

        private List<Vector2> _movementPositions;

        private int nextPositionIndex = 0;
        private Vector3 movementDirection;
        
        
        private void Awake()
        {
            _movementPositions = new List<Vector2>();
            _movementChannel.movementDirectionEvent += MovementDirectionEvent;
        }

        private void MovementDirectionEvent(Vector3 arg0)
        {
            _timeUntillNextPulse = timeBetweenPulses;   
            CreatePath();
        }

        private void Update()
        {
            _timeUntillNextPulse -= Time.deltaTime;

            if (_timeUntillNextPulse <= 0)
            {
                _timeUntillNextPulse = timeBetweenPulses;   
                CreatePath();
            }

            if (_movementPositions.Count <= 0 || nextPositionIndex < 0) return;

            var nextPosition = _movementPositions[nextPositionIndex];

            transform.position =
                Vector3.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
            {
                this.AdvanceNextPosition();
            }
        }

        private void CreatePath()
        {
            var followPosition = _objectToFollow.transform.position;

            _movementPositions = pathFindingData.FindPath(transform.position,followPosition);
            _movementPositions.Insert(0,followPosition.To2D());
            
            SetupNextPosition();
        }

        private void SetupNextPosition()
        {
            nextPositionIndex = _movementPositions.Count - 1;

            var currentEnemyGridPosition =
                pathFindingData.WorldPositionToGridXY(transform.position);

            var nextPositionOnGrid =
                pathFindingData.WorldPositionToGridXY(_movementPositions[nextPositionIndex]);

            if (currentEnemyGridPosition == nextPositionOnGrid)
            {
                this.AdvanceNextPosition();
            }
        }

        private void AdvanceNextPosition()
        {
            nextPositionIndex--;
        }
        
        public void FollowObject(GameObject objectToFollow)
        {
            _objectToFollow = objectToFollow;
        }

        private void OnDestroy()
        {
            _movementChannel.movementDirectionEvent -= MovementDirectionEvent;
        }
    }
}
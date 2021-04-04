using Channels;
using Game;
using Game.Enemies;
using UnityEditor;
using UnityEngine;

namespace Snake
{
    public class SnakeHead : SnakeBodyPart
    {
        protected override void Start()
        {
            base.Start();
            _inputChannel.moveEvent += MoveEvent;
        }

        private void MoveEvent(Vector2 input)
        {
            this._snakeBodyPartMovement.Move(input);
            this.MovementDirection = this._snakeBodyPartMovement.MovementDirection;
        }

        public override void EnemyCollided(Enemy enemy)
        {
            print("Game Over");

            enemy.Die();
        }
    }
}

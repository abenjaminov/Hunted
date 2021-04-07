using System;
using Game.ScriptableObjects;
using Game.ScriptableObjects.GameLogic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Obstacle
    {
        public Sprite Sprite;
        public Vector2 Position;
        public int Width;
        public int Height;
    }
}

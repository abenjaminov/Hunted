using System;
using Channels;
using UnityEngine;

namespace Game
{
    public class GameInput : MonoBehaviour
    {
        public InputChannel _InputChannel;

        private float _horizontal;
        private float _vertical;
    
        private void Update()
        {
            var tempHorizontal = Input.GetAxisRaw("Horizontal");
            var tempVertical = Input.GetAxisRaw("Vertical");

            if ((tempVertical != 0 || tempHorizontal != 0) && 
                (Math.Abs(tempVertical - _vertical) > float.Epsilon || 
                 Math.Abs(tempHorizontal - _horizontal) > float.Epsilon))
            {
                _horizontal = tempHorizontal;
                _vertical = tempVertical;
                _InputChannel.OnMoveEvent(new Vector2(_horizontal, _vertical));
            }
        }
    }
}

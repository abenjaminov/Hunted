using System;
using UnityEngine;

namespace Game
{
    public class SpeedBoostPU : PowerUp<ISpeedBoostPowerUpAcceptor>
    {
        public float SpeedMultiplier;
        
        protected override void Expired()
        {
            _acceptor.BoostSpeed(1 / SpeedMultiplier);
        }

        protected override void Payload()
        {
            _acceptor.BoostSpeed(SpeedMultiplier);   
        }
    }
}
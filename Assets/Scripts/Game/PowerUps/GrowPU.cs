using UnityEngine;

namespace Game
{
    public class GrowPU : PowerUp <IGrowPowerupAcceptor>
    {
        protected override void Expired()
        {
            // Nothing to do here for now
        }

        protected override void Payload()
        {
            this._acceptor.Grow();
        }
    }
}
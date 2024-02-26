using System;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Observers
{
    class ShipReadyObserver : ColObserver
    {
        public override void dClean()
        {
           
        }

        public override void Notify()
        {
            ShipManager.GetShip().SetMiState(ShipManager.eMiState.Ready);
        }
    }
}

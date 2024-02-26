using System;

namespace SpaceInvaders.GameObjects
{
    class MvLeftOnly : MvState
    {
        public override void Handle(Ship pShip)
        {
            
        }

        public override void moveLeft(Ship pShip)
        {
            pShip.incrementX(-pShip.getSpeed());
            pShip.SetMvState(ShipManager.eMvState.Either);
        }

        public override void moveRight(Ship pShip)
        {
            //pShip.incrementX(pShip.getSpeed());
        }
    }
}

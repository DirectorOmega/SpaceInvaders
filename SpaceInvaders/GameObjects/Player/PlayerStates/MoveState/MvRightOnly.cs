
using System;

namespace SpaceInvaders.GameObjects
{
    class MvRightOnly : MvState
    {
        public override void Handle(Ship pShip)
        {
            throw new NotImplementedException();
        }

        public override void moveLeft(Ship pShip)
        {
           // pShip.incrementX(-pShip.getSpeed());
        }

        public override void moveRight(Ship pShip)
        {
            pShip.incrementX(pShip.getSpeed());
            pShip.SetMvState(ShipManager.eMvState.Either);
        }
    }
}

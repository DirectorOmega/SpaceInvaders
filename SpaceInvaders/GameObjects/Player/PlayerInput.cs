using System;
using SpaceInvaders.InputSystem;

namespace SpaceInvaders.GameObjects
{
    class ShipInput : InputObserver
    {
        private Ship pShip;

        ShipInput()
        {
            pShip = null;
        }

        public void setShip(Ship p)
        {
            pShip = p;
        }
        
        public Ship getShip()
        {
            return pShip;
        }
        public override void dClean()
        {
          
        }

        public override void Notify()
        {
          
        }
    }
}

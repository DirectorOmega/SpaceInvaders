using SpaceInvaders.GameObjects;

namespace SpaceInvaders.InputSystem
{
    class MoveRightObserver : InputObserver
    {

        public override void dClean()
        {
            
        }

        public override void Notify()
        {
            ShipManager.GetShip().MoveRight();
        }
    }
}

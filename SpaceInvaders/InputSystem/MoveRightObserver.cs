using SpaceInvaders.GameObjects;

namespace SpaceInvaders.InputSystem
{
    internal sealed class MoveRightObserver : InputObserver
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

using SpaceInvaders.GameObjects;

namespace SpaceInvaders.InputSystem
{
    internal sealed class MoveLeftObserver : InputObserver
    {
        //bad smell probably want to push this up into Input observer.
        public override void dClean()
        {
        
        }

        public override void Notify()
        {
            ShipManager.GetShip().MoveLeft();
        }
    }
}

using SpaceInvaders.GameObjects;

namespace SpaceInvaders.InputSystem
{
    internal sealed class ShootObserver : InputObserver
    {
        //bad smell probably want to push up into inputObserver
        public override void dClean() { }
        public override void Notify() => ShipManager.GetShip().ShootMissile();
    }
}

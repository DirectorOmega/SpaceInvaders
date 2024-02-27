namespace SpaceInvaders.GameObjects
{
    internal sealed class MvRightOnly : MvState
    {
        public override void Handle(Ship pShip) { }

        public override void moveLeft(Ship pShip) {}

        public override void moveRight(Ship pShip)
        {
            pShip.incrementX(pShip.getSpeed());
            pShip.SetMvState(ShipManager.eMvState.Either);
        }
    }
}

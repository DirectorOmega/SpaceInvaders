namespace SpaceInvaders.GameObjects
{
    internal sealed class MvNeither : MvState
    {
        public override void Handle(Ship pShip) 
            => pShip.SetMvState(ShipManager.eMvState.Either);

        public override void moveLeft(Ship pShip) { }
        public override void moveRight(Ship pShip) { }
    }
}

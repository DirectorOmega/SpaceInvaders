namespace SpaceInvaders.GameObjects.Player.PlayerStates.MoveState
{
    internal sealed class NullMvState : MvState
    {
        public override void Handle(Ship pShip) { }
        public override void moveLeft(Ship pShip) { }
        public override void moveRight(Ship pShip) { }
    }
}

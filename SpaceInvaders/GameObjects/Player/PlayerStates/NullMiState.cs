namespace SpaceInvaders.GameObjects.Player.PlayerStates
{
    internal sealed class NullMiState : MissileState
    {
        public override void Handle(Ship pShip) { }
        public override void ShootMissile(Ship pShip) { }
    }
}

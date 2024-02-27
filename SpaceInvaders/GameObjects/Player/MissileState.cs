namespace SpaceInvaders.GameObjects
{
    abstract class MissileState
    {
        public abstract void Handle(Ship pShip);
        public abstract void ShootMissile(Ship pShip);

    }
}

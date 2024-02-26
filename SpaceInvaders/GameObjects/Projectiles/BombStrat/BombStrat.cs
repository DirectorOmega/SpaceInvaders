namespace SpaceInvaders.GameObjects
{
    abstract class BombStrat
    {
        public abstract void Fall(Bomb b);

        public virtual void Reset()
        {

        }

    }
}

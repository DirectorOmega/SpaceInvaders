namespace SpaceInvaders.CollisionSystem
{
    abstract class ColObserver : CoObLink
    {
        public abstract void Notify();
        public ColSubject pSubject;

        public virtual void Execute()
        {
          //default nothing
        }
    }
}

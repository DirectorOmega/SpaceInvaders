using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameState;

namespace SpaceInvaders.Observers
{
    class IncreaseScoreObserver : ColObserver
    {
        public override void dClean()
        {
        
        }

        public override void Notify()
        {
            //Debug.WriteLine("KillAlienObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());
            Alien a = (Alien)this.pSubject.getA();
            GameStateManager.incrementScore(a.getScore());
        }
    }

    class IncreaseScoreUFOObserver : ColObserver
    {
        public override void dClean()
        {

        }

        public override void Notify()
        {
            //Debug.WriteLine("KillAlienObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());
            Alien a = (Alien)this.pSubject.getB();
            GameStateManager.incrementScore(a.getScore());
        }
    }



}

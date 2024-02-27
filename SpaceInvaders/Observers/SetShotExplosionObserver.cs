using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Time;
using SpaceInvaders.Commands;

namespace SpaceInvaders.Observers
{
    class SetShotExplosionObserver : ColObserver
    {

        private GameSprite expSprite;

        public SetShotExplosionObserver(GameSprite bs)
        {
            expSprite = bs;
        }

        public override void dClean()
        {
           
        }

        public override void Notify()
        {
            //Debug.WriteLine("ShotExplosionObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            ProxySprite pSprite = ProxyManager.Add(expSprite);
            pSprite.setCoords(pSubject.getA().getX(), pSubject.getA().getY());
            pSprite.Update();
            this.pSubject.getA().getPSprite().getSBNode().getSBNM().Attach(pSprite);
            TimerManager.Add(TimeEventID.exp, new ExpRemove(pSprite), 0.5f);
            
        }
    }

    class SetShotExplosionObserverB : ColObserver
    {

        private GameSprite expSprite;

        public SetShotExplosionObserverB(GameSprite bs)
        {
            expSprite = bs;
        }

        public override void dClean()
        {

        }

        public override void Notify()
        {
            //Debug.WriteLine("ShotExplosionObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            ProxySprite pSprite = ProxyManager.Add(expSprite);
            pSprite.setCoords(pSubject.getB().getX(), pSubject.getB().getY());
            pSprite.Update();
            this.pSubject.getB().getPSprite().getSBNode().getSBNM().Attach(pSprite);
            TimerManager.Add(TimeEventID.exp, new ExpRemove(pSprite), 0.5f);
        }
    }

}

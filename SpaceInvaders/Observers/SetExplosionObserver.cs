using SpaceInvaders.CollisionSystem;
using System.Diagnostics;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Time;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Commands;

namespace SpaceInvaders.Observers
{
    class SetAlienExplosionObserver : ColObserver
    {
        private GameSprite expSprite;


        public SetAlienExplosionObserver(GameSprite exp)
        {
            expSprite = exp;
        }

        public override void dClean()
        {
            
        }

        public override void Notify()
        {
            //Debug.WriteLine("AlienExplosion: {0} {1}", this.pSubject.getA(), this.pSubject.getB());


            GameObject obj = this.pSubject.getA();
            ProxySprite pSprite = ProxyManager.Add(expSprite);
            pSprite.setCoords(obj.getX(), obj.getY());
           // pSprite.Update();
            obj.getPSprite().getSBNode().getSBNM().Attach(pSprite);
            TimerManager.Add(TimeEventID.exp, new ExpRemove(pSprite), 0.5f);

        }
    }

    class SetUFOExplosionObserver : ColObserver
    {
        private GameSprite expSprite;


        public SetUFOExplosionObserver(GameSprite exp)
        {
            expSprite = exp;
        }

        public override void dClean()
        {

        }

        public override void Notify()
        {
            //Debug.WriteLine("AlienExplosion: {0} {1}", this.pSubject.getA(), this.pSubject.getB());


            GameObject obj = this.pSubject.getB();
            ProxySprite pSprite = ProxyManager.Add(expSprite);
            pSprite.setCoords(obj.getX(), obj.getY());
            // pSprite.Update();
            obj.getPSprite().getSBNode().getSBNM().Attach(pSprite);
            TimerManager.Add(TimeEventID.exp, new ExpRemove(pSprite), 0.5f);
            //TimerManager.Add(TimeEventID.exp, new UFOScoreExp())

        }
    }
}

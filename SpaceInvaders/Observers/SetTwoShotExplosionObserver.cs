using SpaceInvaders.CollisionSystem;
using SpaceInvaders.Commands;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Time;

namespace SpaceInvaders.Observers
{
    class SetTwoShotExplosionObserver : ColObserver
    {

        private GameSprite expSprite1;
        private GameSprite expSprite2;

        public SetTwoShotExplosionObserver(GameSprite exps1,GameSprite exps2)
        {
            expSprite1 = exps1;
            expSprite2 = exps2;
        }

        public override void dClean()
        {

        }

        public override void Notify()
        {
            //Debug.WriteLine("ShotExplosionObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            ProxySprite pSprite1 = ProxyManager.Add(expSprite1);
            ProxySprite pSprite2 = ProxyManager.Add(expSprite2);

            pSprite1.setCoords(pSubject.GetA().GetX(), pSubject.GetA().getY());
            pSprite1.Update();
            this.pSubject.GetA().getPSprite().getSBNode().GetSBNM().Attach(pSprite1);
            TimerManager.Add(TimeEventID.exp, new ExpRemove(pSprite1), 0.5f);

            pSprite2.setCoords(pSubject.GetB().GetX(), pSubject.GetB().getY());
            pSprite2.Update();
            this.pSubject.GetB().getPSprite().getSBNode().GetSBNM().Attach(pSprite2);
            TimerManager.Add(TimeEventID.exp, new ExpRemove(pSprite2), 0.5f);

        }
    }
}

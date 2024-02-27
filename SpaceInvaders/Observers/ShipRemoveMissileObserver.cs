using SpaceInvaders.CollisionSystem;
using System.Diagnostics;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Observers
{
    internal sealed class ShipRemoveMissileObserver : ColObserver
    {
        public override void dClean() { }

        public override void Notify()
        {
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            //this.pMissile = MissileCategory.GetMissile(this.pSubject.pObjA, this.pSubject.pObjB);
            Missile pMissile = ShipManager.GetMissile();
            //Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);

            if (!pMissile.IsMarked())
            {
                pMissile.MarkForDeath();
                //   Delay
                //TODO: clean up this new
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver();
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            Missile pMissile = ShipManager.GetMissile();
            pMissile.setCoords(-50f, -50f);
            pMissile.Update();
            pMissile.Remove();
            pMissile.ClearMark();
        }
    }
}

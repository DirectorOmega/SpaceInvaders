using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using System.Diagnostics;
using System;

namespace SpaceInvaders.Observers
{
    class RemoveBObserver : ColObserver
    {


        public RemoveBObserver()
        {

        }

        public override void dClean()
        {

        }

        public override void Notify()
        {
            GameObject two = this.pSubject.getB();
            if (!two.getMarked())
            {
                two.markForDeath();

                RemoveObserver pObserver = new RemoveObserver(two);
                DelayedObjectManager.Attach(pObserver);
            }
        }
    }
    
    class RemoveObserver : ColObserver
    {
        GameObject toRemove;

        public RemoveObserver(GameObject toRemove)
        {
            this.toRemove = toRemove;
        }

        public override void dClean()
        {
            toRemove = null;
        }
        //stange I know but this is basically a friend to the class below, so I can cooridnate some extra stuff.
        public override void Notify()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {

            toRemove.Remove();
            toRemove.clearMark();

        }
    }

    class RemoveBothObserver : ColObserver
    {
      

        public RemoveBothObserver()
        {
        }

        public override void dClean()
        {
        }

        public override void Notify()
        {
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            GameObject one = this.pSubject.getA();
            GameObject two = this.pSubject.getB();
            Debug.Assert(this.pSubject.getA() != null && this.pSubject.getB() != null);

            if (!one.getMarked())
            {
                one.markForDeath();
    
                //TODO: clean up this new
                RemoveObserver pObserver = new RemoveObserver(one);
                DelayedObjectManager.Attach(pObserver);
            }
            if(!two.getMarked())
            {
                two.markForDeath();

                RemoveObserver pObserver = new RemoveObserver(two);
                DelayedObjectManager.Attach(pObserver);
            }
        }

 
    }
}

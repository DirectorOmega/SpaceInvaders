using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using System.Diagnostics;

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
            GameObject two = this.pSubject.GetB();
            if (!two.IsMarked())
            {
                two.MarkForDeath();

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
        //strange I know but this is basically a friend to the class below, so I can coordinate some extra stuff.
        public override void Notify()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {

            toRemove.Remove();
            toRemove.ClearMark();

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

            GameObject one = this.pSubject.GetA();
            GameObject two = this.pSubject.GetB();
            Debug.Assert(this.pSubject.GetA() != null && this.pSubject.GetB() != null);

            if (!one.IsMarked())
            {
                one.MarkForDeath();
    
                //TODO: clean up this new
                RemoveObserver pObserver = new RemoveObserver(one);
                DelayedObjectManager.Attach(pObserver);
            }
            if(!two.IsMarked())
            {
                two.MarkForDeath();

                RemoveObserver pObserver = new RemoveObserver(two);
                DelayedObjectManager.Attach(pObserver);
            }
        }
    }
}

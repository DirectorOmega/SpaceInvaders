using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.Observers
{
    internal sealed class BombRemoveObserver : ColObserver
    {
        Bomb toRemove;

        public BombRemoveObserver()
        {
            toRemove = null;
        }

        public BombRemoveObserver(Bomb b)
        {
            this.toRemove = b;
        }
        public override void dClean()
        {

        }

        public override void Notify()
        {
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            Bomb B = (Bomb)this.pSubject.GetA();
            Debug.Assert(this.pSubject.GetA() != null && this.pSubject.GetB() != null);
           
            if (!B.IsMarked())
            {
                B.MarkForDeath();
                //   Delay
                //TODO: clean up this new
                BombRemoveObserver pObserver = new BombRemoveObserver(B);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            toRemove.Remove();
            toRemove.ClearMark();
        }
    }
}

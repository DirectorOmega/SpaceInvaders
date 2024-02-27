using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Observers
{
    internal sealed class KillAlienObserver : ColObserver
    {
        private Alien toRemove;

        public KillAlienObserver()
        {

        }

        public KillAlienObserver(Alien a)
        {
            this.toRemove = a;
        }

        public override void dClean()
        {
           
        }

        //Aliens always come first.
        public override void Notify()
        {

            //Debug.WriteLine("KillAlienObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            // OK do some magic
            Alien a = (Alien)this.pSubject.GetA();
            //this.pSubject.getA().Remove();


            if (!a.IsMarked())
            {
                a.MarkForDeath();
                //   Delay
                //TODO: clean up this new
                KillAlienObserver pObserver = new KillAlienObserver(a);
                DelayedObjectManager.Attach(pObserver);
            }
        }


              public override void Execute()
        {
            //prevents the 1 frame ghost of the spritebox
            //toRemove.setCoords(-50f, -50f);
            //toRemove.Update();

            toRemove.Remove();
            toRemove.ClearMark();

        }
        //decrease time between jumps


        // this.pSubject.getB();
    }
    
}

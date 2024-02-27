using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.Observers
{
    class GridObserver : ColObserver
    {

        public GridObserver()
        {

        }

        public override void dClean()
        {
        
        }

        public override void Notify()
        {
            //Debug.WriteLine("GridObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            // OK do some magic
            Grid pGrid = (Grid)this.pSubject.getA();

            pGrid.reverse();
            pGrid.MoveGrid();
            pGrid.MoveGridDown();
        }
    }
}

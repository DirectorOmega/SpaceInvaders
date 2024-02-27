using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Observers
{
    internal sealed class GridObserver : ColObserver
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
            Grid pGrid = (Grid)this.pSubject.GetA();

            pGrid.reverse();
            pGrid.MoveGrid();
            pGrid.MoveGridDown();
        }
    }
}

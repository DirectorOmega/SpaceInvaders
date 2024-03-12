using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    //Just a wrapper for baseManager to allow for generation of clearer class diagrams.
    abstract class TexMan : baseManager
    {
#if DEBUG
        private TexLink contains;
#endif
        public TexMan(int numStart, int deltaAdd): base(numStart,deltaAdd)
        {

        }
    }
}

using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    abstract class SBMan : baseManager
    {
        public SBMan(int numStart = 5,int DeltaAdd = 3) : base(numStart,DeltaAdd)
        { }
    }
}

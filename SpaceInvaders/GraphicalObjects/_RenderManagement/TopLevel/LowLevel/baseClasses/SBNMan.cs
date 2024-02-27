using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    abstract class SBNMan : baseManager
    {
        public SBNMan(int numStart = 5 ,int deltaAdd = 3) : base(numStart,deltaAdd)
        { }
    }
}

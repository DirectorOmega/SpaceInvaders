using SpaceInvaders.Manager;

namespace SpaceInvaders.GameObjects
{
    abstract class GoMan : baseManager
    {
        public GoMan(int numStart, int deltaAdd) 
            : base(numStart, deltaAdd) { }
    }
}

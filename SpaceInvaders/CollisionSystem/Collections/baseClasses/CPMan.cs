using SpaceInvaders.Manager;

namespace SpaceInvaders.CollisionSystem
{
    abstract class CPMan : baseManager
    {
        public CPMan(int numStart, int deltaAdd): base(numStart,deltaAdd)
        {
        }
    }
}
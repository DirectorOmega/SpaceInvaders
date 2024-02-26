
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    class Player : GameObject
    {
        public Player(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
        }

        public override void Accept(ColVistor other)
        {
            other.VisitPlayer(this);
        }

        public override void cClean()
        {

        }
    }
}

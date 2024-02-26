using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    class Topwall : GameObject
    {
        public Topwall(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
        }

        public override void Accept(ColVistor other)
        {
            other.VisitTopwall(this);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(m, this);
        }

        public override void cClean()
        {
            
        }
    }
}

using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    class Sidewall : GameObject
    {
        public Sidewall(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
        }

        public override void Accept(ColVistor other)
        {
            other.VisitSidewall(this);
        }

        public override void VisitUFORoot(UFORoot ur)
        {
            Reactions.Reaction(this, ur);
        }

        public override void VisitUFO(UFO u)
        {
            Reactions.Reaction(this, u);
        }

        public override void VisitGrid(Grid g)
        {
             Reactions.Reaction(g, this);
        }

        public override void cClean()
        {
            
        }
    }
}

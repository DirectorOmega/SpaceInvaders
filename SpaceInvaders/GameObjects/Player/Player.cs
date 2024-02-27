using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class PlayerAvatar : GameObject
    {
        public PlayerAvatar(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
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
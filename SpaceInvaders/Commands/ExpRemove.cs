using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.Commands
{
    internal sealed class ExpRemove : Command
    {
        ProxySprite toRemove;
        private ExpRemove() { }

        public ExpRemove(ProxySprite p) => toRemove = p;
        public override void execute(float deltaTime) 
            => toRemove.getSBNode().RemoveSelf();
    }
}

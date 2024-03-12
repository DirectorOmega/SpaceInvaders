using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Commands
{
    internal sealed class UFODropBombCMD : Command
    {
        private readonly UFORoot dropper;

        public UFODropBombCMD(UFORoot u) => dropper = u;

        public override void execute(float deltaTime)
        {
            if (null != dropper)
                dropper.DropBomb();
        }
    }
    
    internal sealed class DropBombCMD : Command
    {
        private readonly Column dropper;

        public DropBombCMD(Column c) => dropper = c;

        public override void execute(float deltaTime)
        {
            if (null != dropper)
                dropper.DropBomb();
        }
    }
}

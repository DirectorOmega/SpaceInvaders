using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Commands
{
    internal sealed class UFODropBombCMD : Command
    {
        UFORoot dropper;

        public UFODropBombCMD(UFORoot u) => dropper = u;

        public override void execute(float deltaTime)
        {
            if (null != dropper)
                dropper.DropBomb();
        }
    }
    
    internal sealed class DropBombCMD : Command
    {
        //Alien dropper;
        //BombRoot br;
        Column dropper;

        public DropBombCMD(Column c)
        {
            dropper = c;
            // this.br = br;
        }

        public override void execute(float deltaTime)
        {
            if (null != dropper)
                dropper.DropBomb();
        }
    }
}

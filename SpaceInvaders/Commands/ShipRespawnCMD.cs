using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Commands
{
    class ShipRespawnCMD : Command
    {
        public override void execute(float deltaTime)
        {
            ShipManager.ActivateShip();
        }
    }
}

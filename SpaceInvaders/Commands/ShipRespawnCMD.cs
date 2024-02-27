using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Commands
{
    internal sealed class ShipRespawnCMD : Command
    {
        public override void execute(float deltaTime) 
            => ShipManager.ActivateShip();
    }
}

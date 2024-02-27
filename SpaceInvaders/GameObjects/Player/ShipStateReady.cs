using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    internal sealed class ShipStateReady : MissileState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMiState(ShipManager.eMiState.MissileFlying);
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissle = ShipManager.ActivateMissile();
            Debug.Assert(null != pMissle);
            pMissle.setCoords(pShip.GetX(), pShip.getY() + 20);
            this.Handle(pShip);
        }
    }
}

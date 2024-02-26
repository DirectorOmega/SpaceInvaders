using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.Observers
{
    class BumperObserver : ColObserver
    {
        public override void dClean()
        {
            
        }

        public override void Notify()
        {
            //Debug.WriteLine("BumperObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            //this.pSubject.getA();
            Bumper pB = (Bumper)this.pSubject.getA();
            Ship pS = (Ship)this.pSubject.getB();

            if((GameObjectTypeEnum) pB.getName() == GameObjectTypeEnum.LeftBumper)
            {
                pS.incrementX(pS.getSpeed());
                pS.SetMvState(ShipManager.eMvState.RightOnly);
            }
            else
            {
                pS.incrementX(-pS.getSpeed());
                pS.SetMvState(ShipManager.eMvState.LeftOnly);
            }


        }
    }
}

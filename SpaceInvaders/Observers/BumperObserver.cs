using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Observers
{
    internal sealed class BumperObserver : ColObserver
    {
        public override void dClean()
        {
            
        }

        public override void Notify()
        {
            //Debug.WriteLine("BumperObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            //this.pSubject.getA();
            Bumper pB = (Bumper)this.pSubject.GetA();
            Ship pS = (Ship)this.pSubject.GetB();

            if((GameObjectType) pB.getName() == GameObjectType.LeftBumper)
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

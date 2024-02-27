using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameState;


namespace SpaceInvaders.Observers
{
    class ShieldHitObserver : ColObserver
    {

        
        public ShieldHitObserver()
        {
     //       pGNR = (GNoiseRoot) GameObjectManager.Find(GameObjectTypeEnum.Noise).getGameObject();
      //      pBatch = SpriteBatchManager.Find(SpriteBatchID.CBox);
        }

        //TODO: delay removal
        //TODO: add shield vs noise also add in the delay for the shield remove I can add in the noise start,
        //to push the secondary collisions to the next frame, to keep the number of collisions per-frame down.
        //todo this I should push the noise into the execute of the shield remove observer.

        public override void Notify()
        {
            //this.pSubject.getB().Remove();

            GameObject pGameObj = (GameObject)this.pSubject.getB();
            GNoisePoint gp;
            float x = pGameObj.getX();
            float y = pGameObj.getY();
            int numNoise = GameStateManager.getRandomNumber(3, 11);
            for(int i = 0;i<numNoise;i++)
            {
                gp = GNoiseFactory.getGNPoint();
                gp.setCoords(x + GameStateManager.getRandomNumber(-10, 10), y + (GameStateManager.getRandomNumber(-10, 10)));
               
                pGameObj.getPSprite().getSBNode().getSBNM().Attach(gp.getPSprite());
                gp.ActivateCollisionSprite();
                gp.Update();  
            }


            if(!pGameObj.getMarked())
            {
                pGameObj.markForDeath();

                RemoveObserver pObserver = new RemoveObserver(pGameObj);
                DelayedObjectManager.Attach(pObserver);

            }

        }

        public override void dClean()
        {
          
        }
    }
}

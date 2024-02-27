using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;
using SpaceInvaders.Commands;
using SpaceInvaders.Time;

namespace SpaceInvaders.GameObjects
{
    class ShipManager
    {
        public enum eMiState
        {
            Ready,
            MissileFlying,
            End
        }

        public enum eMvState
        {
            Either,
            LeftOnly,
            RightOnly,
            Neither
        }

        //keep missile here and add and remove it from sprite batch and game object manager as needed.
        //store a refrence to the game object node as well.
        private ShipManager()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying();
            this.pStateEnd = new ShipStateEnd();

            this.pEither = new MvEither();
            this.pLeftO = new MvLeftOnly();
            this.pRightO = new MvRightOnly();
            this.pNeither = new MvNeither();

            //this.pDeathAnim = new ShipDeathAnimCMD();

            this.pShip = new Ship(SpriteID.Hero, 256, 60);
            pShip.setName(GameObjectTypeEnum.Hero);
            pShip.getPSprite().setColor(255.0f, 255.0f, 0);


            this.pMissile = new Missile(SpriteID.Missile, 0, 0);
            pMissile.setName(GameObjectTypeEnum.Missile);
            pMissile.getPSprite().setColor(0.0f, 255.0f, 0.0f);
        }

        public static void Create()
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipManager();
                instance.pMissleSFX = new MissileSFXCMD();
             
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
           
        }

        private static ShipManager privInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        internal static void DeathAnim(float v)
        {
            ShipManager pShipMan = ShipManager.privInstance();

          //  pShipMan.pDeathAnim.setAnim(pShipMan.pShip.getX(),pShipMan.pShip.getY(),2.5f, SpriteBatchManager.Find(SpriteBatchID.Ship));
            //TimerManager.Add(TimeEventID.Anim, pShipMan.pDeathAnim, 0.0f);
        }

        public static Ship GetShip()
        {
            ShipManager pShipMan = ShipManager.privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static MissileState GetMiState(eMiState state)
        {
            ShipManager pShipMan = ShipManager.privInstance();
            Debug.Assert(pShipMan != null);

            MissileState pShipState = null;

            switch (state)
            {
                case ShipManager.eMiState.Ready:
                  pShipState = pShipMan.pStateReady;
                    break;

                case ShipManager.eMiState.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipManager.eMiState.End:
                    pShipState = pShipMan.pStateEnd;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static MvState GetMvState(eMvState state)
        {
            ShipManager pShipMan = ShipManager.privInstance();
            Debug.Assert(pShipMan != null);

            MvState pShipState = null;

            switch (state)
            {
                case ShipManager.eMvState.Either:
                    pShipState = pShipMan.pEither;
                    break;

                case ShipManager.eMvState.LeftOnly:
                    pShipState = pShipMan.pLeftO;
                    break;

                case ShipManager.eMvState.RightOnly:
                    pShipState = pShipMan.pRightO;
                    break;
                case ShipManager.eMvState.Neither:
                    pShipState = pShipMan.pNeither;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }


        public static Missile GetMissile()
        {
            ShipManager pShipMan = ShipManager.privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {

            ShipManager pShipMan = ShipManager.privInstance();
            Debug.Assert(pShipMan != null);
            
            // copy over safe copy
           
            SpriteBatchManager.Find(SpriteBatchID.Shots).Attach(pShipMan.pMissile.getPSprite());
            GameObjectManager.Insert(pShipMan.pMissile);

            pShipMan.pMissile.ActivateCollisionSprite();
            //pShipMan.pMissile.clearMark();

             pShipMan.pMissleSFX.execute();
            
            return pShipMan.pMissile;
        }

        public static Ship ActivateShip()
        {
            ShipManager pShipMan = ShipManager.privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetMiState(ShipManager.eMiState.Ready);
            pShipMan.pShip.SetMvState(ShipManager.eMvState.Either);

            SpriteBatchManager.Find(SpriteBatchID.Ship).Attach(pShipMan.pShip.getPSprite());
            GameObjectManager.Insert(pShipMan.pShip);
           
            pShipMan.pShip.ActivateCollisionSprite();
            pShipMan.pShip.setCoords(256, 60);
            pShipMan.pShip.clearMark();
            return pShipMan.pShip;
        }

        public static void ReactivateShip()
        {
            ShipManager pShipMan = ShipManager.privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetMiState(ShipManager.eMiState.Ready);
            pShipMan.pShip.SetMvState(ShipManager.eMvState.Either);

            SpriteBatchManager.Find(SpriteBatchID.Ship).Attach(pShipMan.pShip.getPSprite());
            pShipMan.pShip.ActivateCollisionSprite();
            pShipMan.pShip.setCoords(256, 60);
        }

        public static void DeactivateShip()
        {
            ShipManager pShipMan = ShipManager.privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetMiState(ShipManager.eMiState.End);
            pShipMan.pShip.SetMvState(ShipManager.eMvState.Neither);

            //DeathAnim(3.0f);
            //  pShipMan.pShip.Remove();

            //   pShipMan.pShip.getPSprite().getSBNode().RemoveSelf();
            //  pShipMan.pShip.getCollisionObject().getColSprite().getSBNode().RemoveSelf();
        }

        // Data: ----------------------------------------------
        private static ShipManager instance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;
        private MissileSFXCMD pMissleSFX;
       // private ShipDeathAnimCMD pDeathAnim;

        // Reference
        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private ShipStateEnd pStateEnd;

        private MvEither pEither;
        private MvLeftOnly pLeftO;
        private MvRightOnly pRightO;
        private MvNeither pNeither;
    }
}

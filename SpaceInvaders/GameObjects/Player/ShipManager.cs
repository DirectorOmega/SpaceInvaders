using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;
using SpaceInvaders.Commands;

namespace SpaceInvaders.GameObjects
{
    internal sealed class ShipManager
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
        //store a reference to the game object node as well.
        private ShipManager()
        {
            // Store the states
            pStateReady = new ShipStateReady();
            pStateMissileFlying = new ShipStateMissileFlying();
            pStateEnd = new ShipStateEnd();

            pEither = new MvEither();
            pLeftO = new MvLeftOnly();
            pRightO = new MvRightOnly();
            pNeither = new MvNeither();

            //this.pDeathAnim = new ShipDeathAnimCMD();

            pShip = new Ship(SpriteID.Hero, 256, 60);
            pShip.setName(GameObjectType.Hero);
            pShip.getPSprite().setColor(255.0f, 255.0f, 0);


            pMissile = new Missile(SpriteID.Missile, 0, 0);
            pMissile.setName(GameObjectType.Missile);
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
                instance.pMissileSFX = new MissileSFXCMD();
             
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
            ShipManager pShipMan = privInstance();

          //  pShipMan.pDeathAnim.setAnim(pShipMan.pShip.getX(),pShipMan.pShip.getY(),2.5f, SpriteBatchManager.Find(SpriteBatchID.Ship));
            //TimerManager.Add(TimeEventID.Anim, pShipMan.pDeathAnim, 0.0f);
        }

        public static Ship GetShip()
        {
            ShipManager pShipMan = privInstance();

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
            ShipManager pShipMan = privInstance();
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
            ShipManager pShipMan = privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {

            ShipManager pShipMan = privInstance();
            Debug.Assert(pShipMan != null);
            
            // copy over safe copy
           
            SpriteBatchManager.Find(SpriteBatchID.Shots).Attach(pShipMan.pMissile.getPSprite());
            GameObjectManager.Insert(pShipMan.pMissile);

            pShipMan.pMissile.ActivateCollisionSprite();
            //pShipMan.pMissile.clearMark();

            pShipMan.pMissileSFX.execute();
            
            return pShipMan.pMissile;
        }

        public static Ship ActivateShip()
        {
            ShipManager pShipMan = privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetMiState(eMiState.Ready);
            pShipMan.pShip.SetMvState(eMvState.Either);

            SpriteBatchManager.Find(SpriteBatchID.Ship).Attach(pShipMan.pShip.getPSprite());
            GameObjectManager.Insert(pShipMan.pShip);
           
            pShipMan.pShip.ActivateCollisionSprite();
            pShipMan.pShip.setCoords(256, 60);
            pShipMan.pShip.ClearMark();
            return pShipMan.pShip;
        }

        public static void ReactivateShip()
        {
            ShipManager pShipMan = privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetMiState(eMiState.Ready);
            pShipMan.pShip.SetMvState(eMvState.Either);

            SpriteBatchManager.Find(SpriteBatchID.Ship).Attach(pShipMan.pShip.getPSprite());
            pShipMan.pShip.ActivateCollisionSprite();
            pShipMan.pShip.setCoords(256, 60);
        }

        public static void DeactivateShip()
        {
            ShipManager pShipMan = privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip.SetMiState(eMiState.End);
            pShipMan.pShip.SetMvState(eMvState.Neither);

            //DeathAnim(3.0f);
            //  pShipMan.pShip.Remove();

            //   pShipMan.pShip.getPSprite().getSBNode().RemoveSelf();
            //  pShipMan.pShip.getCollisionObject().getColSprite().getSBNode().RemoveSelf();
        }

        // Data: ----------------------------------------------
        private static ShipManager instance;

        // Active
        private readonly Ship pShip;
        private readonly Missile pMissile;
        private readonly MissileSFXCMD pMissileSFX;
       // private ShipDeathAnimCMD pDeathAnim;

        // Reference
        private readonly ShipStateReady pStateReady;
        private readonly ShipStateMissileFlying pStateMissileFlying;
        private readonly ShipStateEnd pStateEnd;

        private readonly MvEither pEither;
        private readonly MvLeftOnly pLeftO;
        private readonly MvRightOnly pRightO;
        private readonly MvNeither pNeither;
    }
}

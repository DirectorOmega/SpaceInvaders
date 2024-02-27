using SpaceInvaders.GameState;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Time;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.Commands
{
    class PlayerACommand : Command
    {
        public PlayerACommand()
        {
         
        }

        public override void execute(float deltaTime)
        {
            DelayedObjectManager.Process();

            GameObjectManager.StorePlayerB();
            GameObjectManager.SetPlayerA();

            CollisionPairManager.StorePlayerB();
            CollisionPairManager.SetPlayerA();

            SpriteBatchManager.StorePlayerB();
            SpriteBatchManager.SetPlayerA();

            TimerManager.Clear(TimeEventID.bombDrop);

            TimerManager.Clear(TimeEventID.GridMove);

            TimerManager.Clear(TimeEventID.GridMoveSFX);

            TimerManager.Clear(TimeEventID.Anim);
            TimerManager.Clear(TimeEventID.NextWave);
            TimerManager.Clear(TimeEventID.create);
            TimerManager.Clear(TimeEventID.ShipRespawn);
            SndEngine.getIKEngine().StopAllSounds();

            GameStateManager.setStatePA(); 
        }
    }

    class PlayerBCommand : Command
    {

        public PlayerBCommand()
        {

        }

        public override void execute(float deltaTime)
        {
            //todo: would be helpful to not clear everything if the other player is out of lives.
            DelayedObjectManager.Process();

            GameObjectManager.StorePlayerA();
            GameObjectManager.SetPlayerB();

            CollisionPairManager.StorePlayerA();
            CollisionPairManager.SetPlayerB();

            SpriteBatchManager.StorePlayerA();
            SpriteBatchManager.SetPlayerB();

            TimerManager.Clear(TimeEventID.bombDrop);

            TimerManager.Clear(TimeEventID.GridMove);

            TimerManager.Clear(TimeEventID.GridMoveSFX);

            TimerManager.Clear(TimeEventID.Anim);
            TimerManager.Clear(TimeEventID.NextWave);
            TimerManager.Clear(TimeEventID.create);
            TimerManager.Clear(TimeEventID.ShipRespawn);
            SndEngine.getIKEngine().StopAllSounds();

            GameStateManager.setStatePB();
        }
    }

    class NextWaveCommand : Command
    {
        private GameState.GameState currentState;

        public NextWaveCommand(GameState.GameState currentState)
        {
            this.currentState = currentState;
        }

        public override void execute(float deltaTime)
        {
            GameObjectManager.Clear();
            DelayedObjectManager.Clear();
            CollisionPairManager.Clear();

            TimerManager.Clear(TimeEventID.bombDrop);

            TimerManager.Clear(TimeEventID.GridMove);

            TimerManager.Clear(TimeEventID.GridMoveSFX);

            TimerManager.Clear(TimeEventID.Anim);
            TimerManager.Clear(TimeEventID.NextWave);
            TimerManager.Clear(TimeEventID.create);

            GameStateManager.nextWaveReady();
            currentState.prepNextWave();
            currentState.Init();
        }
    }
}

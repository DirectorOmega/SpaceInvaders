using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.InputSystem;
using SpaceInvaders.Time;
using SpaceInvaders.Commands;
using SpaceInvaders.Observers;
using SpaceInvaders.PCS;
using SpaceInvaders.GameObjects.Projectiles;

namespace SpaceInvaders.GameState
{
    internal sealed class StatePlayerA : GameState
    {
        //move these out of this to prevent skew when adding playerB
        //TODO: player b is in but these methods are duplicated I NEED to remove these to prevent skew
        private static void CreateGrid(float startx = 50, float starty = 800, float offsetx = 55.0f, float offsety = -55.0f)
        {
            BombRoot br = new BombRoot(SpriteID.NullSprite, -50, -50);
            br.setName(GameObjectType.bombRoot);
            GameObjectManager.AttachTree(br, new PCSTree());
            SpriteBatchManager.Find(SpriteBatchID.Shots).Attach(br.getPSprite());
            br.ActivateCollisionSprite();

            GameStateManager.setActiveBombRoot(GameObjectManager.Find(GameObjectType.bombRoot));

            PCSTree pAlienTree = new PCSTree();

            //create the factory
            AlienFactory AF = new AlienFactory(SpriteBatchManager.Find(SpriteBatchID.Alien), pAlienTree);

            //set the parent for hierarchy inside the factory

            //AF.setParent(pAlienTree.getRoot());
            //AF.setParent(null);

            //attach to Root(this is the Grid)

            Grid pGrid = (Grid)AF.Create(GameObjectType.Grid, startx, starty);
            GameStateManager.setActiveGrid(GameObjectManager.Find(GameObjectType.Grid));
            //Grid pGrid = GameStateManager.getActiveGrid();

            //set Parent
            AF.setParent(pGrid);

            Column pCol;

            for (int i = 0; i < 11; i++)
            {
                // pAlienTree.dumpTree();
                AF.setParent(pGrid);

                pCol = (Column)AF.Create(GameObjectType.Column, startx + i * offsetx, starty);

                AF.setParent(pCol);

                AF.Create(GameObjectType.Octo, startx + i * offsetx, starty);

                AF.Create(GameObjectType.Crab, startx + i * offsetx, starty + 1 * offsety);

                AF.Create(GameObjectType.Crab, startx + i * offsetx, starty + 2 * offsety);

                AF.Create(GameObjectType.Squid, startx + i * offsetx, starty + 3 * offsety);

                AF.Create(GameObjectType.Squid, startx + i * offsetx, starty + 4 * offsety);

            }
        }

        private static void SetupShields()
        {
            BoxSpriteManager.Add(SpriteID.ShieldBrick, new Azul.Rect(0.0f, 0.0f, 6.0f, 6.0f));

            PCSTree pShieldTree = new PCSTree();

            ShieldFactory SF = new ShieldFactory(SpriteBatchManager.Find(SpriteBatchID.Shield), pShieldTree);

            SF.setShieldRoot(new ShieldRoot(SpriteID.NullSprite,0,0));
            SF.Create(225, 225);
            SF.Create(375, 225);
            SF.Create(525, 225);
            SF.Create(675, 225);
        }

        private static void SetupWalls()
        {
            int sh = GameStateManager.getScreenHeight();
            int sw = GameStateManager.getScreenWidth();

            BoxSpriteManager.Add(SpriteID.VerticalWall, new Azul.Rect(0.0f, 0.0f, 25.0f,sh));
            //BoxSpriteManager.Add(SpriteID.SideWall, new Azul.Rect(0.0f, 0.0f, 25.0f, this.GetScreenHeight()));
            //TODO: change sprite ID to horizontal wall since I am reusing for top and bottom
            BoxSpriteManager.Add(SpriteID.HorizontalWall, new Azul.Rect(0.0f, 0.0f,sw, 25.0f));

            BoxSpriteManager.Add(SpriteID.Bumper, new Azul.Rect(0.0f, 0.0f, 25.0f, 200.0f));

            Sidewall rw = new Sidewall(SpriteID.NullSprite, sw - (24.0f / 2), sh / 2);
            Sidewall lw = new Sidewall(SpriteID.NullSprite, 25.0f / 2, sh / 2);
            Topwall tw = new Topwall(SpriteID.NullSprite, sw / 2, sh - 25);
            BottomWall bw = new BottomWall(SpriteID.NullSprite, sw / 2, 25);
            Bumper lb = new Bumper(SpriteID.NullSprite, sw / 10, 0);
            Bumper rb = new Bumper(SpriteID.NullSprite, sw - sw / 10, 0);

            lw.CollisionObject.setColRect(BoxSpriteManager.Find(SpriteID.VerticalWall));
            rw.CollisionObject.setColRect(BoxSpriteManager.Find(SpriteID.VerticalWall));

            tw.CollisionObject.setColRect(BoxSpriteManager.Find(SpriteID.HorizontalWall));
            bw.CollisionObject.setColRect(BoxSpriteManager.Find(SpriteID.HorizontalWall));

            lb.CollisionObject.setColRect(BoxSpriteManager.Find(SpriteID.Bumper));
            rb.CollisionObject.setColRect(BoxSpriteManager.Find(SpriteID.Bumper));

            SpriteBatch barrier = SpriteBatchManager.Find(SpriteBatchID.Shield);

            barrier.Attach(lw.getPSprite());
            barrier.Attach(rw.getPSprite());

            barrier.Attach(tw.getPSprite());
            barrier.Attach(bw.getPSprite());

            barrier.Attach(lb.getPSprite());
            barrier.Attach(rb.getPSprite());

            lw.ActivateCollisionSprite();
            rw.ActivateCollisionSprite();

            tw.ActivateCollisionSprite();
            bw.ActivateCollisionSprite();

            lb.ActivateCollisionSprite();
            rb.ActivateCollisionSprite();

            rw.setName(GameObjectType.RightWall);
            lw.setName(GameObjectType.LeftWall);

            tw.setName(GameObjectType.TopWall);
            bw.setName(GameObjectType.BottomWall);

            lb.setName(GameObjectType.LeftBumper);
            rb.setName(GameObjectType.RightBumper);

            rw.Update();
            lw.Update();

            tw.Update();
            bw.Update();

            lb.Update();
            rb.Update();

            CollisionPair pColPair;
            Grid pGrid = GameStateManager.getActiveGrid();
            BombRoot br = GameStateManager.getActiveBombRoot();

            //TODO: create the observers for other events 
            //TODO: extract out common Find methods to remove duplicate searches.

            pColPair = CollisionPairManager.Add(CollisionPairName.GridvsRightWall, pGrid, rw);
            pColPair.Attach(new GridObserver());
            //  pColPair.Attach(new SndObserver(sndEngine));

            pColPair = CollisionPairManager.Add(CollisionPairName.GridvsLeftWall, pGrid, lw);
            pColPair.Attach(new GridObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.PlayervsLBumper, ShipManager.GetShip(), lb);
            pColPair.Attach(new BumperObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.PlayervsRBumper, ShipManager.GetShip(), rb);
            pColPair.Attach(new BumperObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.MissilevsTopWall, ShipManager.GetMissile(), tw);
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.MissileExp)));

            pColPair = CollisionPairManager.Add(CollisionPairName.GridvsMissile, pGrid, ShipManager.GetMissile());
            pColPair.Attach(new KillAlienObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new SetAlienExplosionObserver(GameSpriteManager.Find(SpriteID.AlienExp)));
            pColPair.Attach(new SetShotExplosionObserverB(GameSpriteManager.Find(SpriteID.MissileExp)));
            pColPair.Attach(new IncreaseScoreObserver());
            pColPair.Attach(new playAlienDeathSoundObserver(SndEngine.getInstance()));

            //pColPair.Attach(new refreshColObjs());

            pColPair = CollisionPairManager.Add(CollisionPairName.BombsvsBottom, br, bw);
            pColPair.Attach(new BombRemoveObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.AlienShotExp)));

            pColPair = CollisionPairManager.Add(CollisionPairName.BombvsShip, br, ShipManager.GetShip());
            pColPair.Attach(new BombRemoveObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.AlienShotExp)));
            pColPair.Attach(new PlayerLoseLifeObserver());
            pColPair.Attach(new ShipDeathObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.MissilevsShield, (GameObject)GameObjectManager.Find(GameObjectType.ShieldRoot).GetGameObject(), ShipManager.GetMissile());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.MissileExp)));
            pColPair.Attach(new ShieldHitObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.BombvsShield, (GameObject)GameObjectManager.Find(GameObjectType.bombRoot).GetGameObject(), (GameObject)GameObjectManager.Find(GameObjectType.ShieldRoot).GetGameObject());
            pColPair.Attach(new ShieldHitObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.AlienShotExp)));
            pColPair.Attach(new BombRemoveObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.BombvsMissile, (GameObject)GameObjectManager.Find(GameObjectType.bombRoot).GetGameObject(), (GameObject)ShipManager.GetMissile());
            pColPair.Attach(new BombRemoveObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SetTwoShotExplosionObserver(GameSpriteManager.Find(SpriteID.AlienShotExp), GameSpriteManager.Find(SpriteID.MissileExp)));

            GNoiseRoot gnr = new GNoiseRoot(SpriteID.NullSprite);
            gnr.setName(GameObjectType.NoiseRoot);
            GameObjectManager.AttachTree(gnr, new PCSTree());
            SpriteBatchManager.Find(SpriteBatchID.Undef).Attach(gnr.getPSprite());
            GameStateManager.setActiveNoiseRoot(GameObjectManager.Find(GameObjectType.NoiseRoot));
            gnr.ActivateCollisionSprite();

            pColPair = CollisionPairManager.Add(CollisionPairName.NoisevsShield, (GameObject) GameObjectManager.Find(GameObjectType.NoiseRoot).GetGameObject(), (GameObject) GameObjectManager.Find(GameObjectType.ShieldRoot).GetGameObject());
            pColPair.Attach(new RemoveBothObserver());

            UFORoot ufr = new UFORoot(SpriteID.NullSprite);
            ufr.setName(GameObjectType.UFORoot);
            GameObjectManager.AttachTree(ufr, new PCSTree(ufr));
            SpriteBatchManager.Find(SpriteBatchID.Alien).Attach(ufr.getPSprite());
            ufr.ActivateCollisionSprite();

            pColPair = CollisionPairManager.Add(CollisionPairName.UFOvsMissile, (GameObject)GameObjectManager.Find(GameObjectType.UFORoot).GetGameObject(), (GameObject)ShipManager.GetMissile());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new IncreaseScoreUFOObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.MissileExp)));
            pColPair.Attach(new UFORemoveObserver());
            pColPair.Attach(new UFODeathSoundObserver());
            //todo: add a ufo death sound start oberver.
            pColPair.Attach(new SetUFOExplosionObserver(GameSpriteManager.Find(SpriteID.UFOExp)));

            pColPair = CollisionPairManager.Add(CollisionPairName.UFOvsLeftWall, (GameObject)GameObjectManager.Find(GameObjectType.UFORoot).GetGameObject(), lw);
            pColPair.Attach(new UFORemoveObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.GridvsShield, (GameObject)GameObjectManager.Find(GameObjectType.Grid).GetGameObject(),(GameObject)GameObjectManager.Find(GameObjectType.ShieldRoot).GetGameObject());
            pColPair.Attach(new RemoveBObserver());
        }

        public StatePlayerA()
        {
            numLives = 0;
            nextlifeProgress = 0;
        }

        public override void Handle()
        {
         
        }

        public override void Reset()
        {
            nextlifeProgress = 0;
            SpawnField = true;
        }


        public override void setLives(int numLives)
        {
            this.numLives = numLives;
        }

        public override void loseLife()
        {
            numLives--;
            lc.removeLife();
        }


        public override void addLife()
        {
            numLives++;
            lc.addLife();
        }

        public override int getLifeCount()
        {
            return numLives;
        }

        bool SpawnField = true;
        int numLives;
        int nextlifeProgress;
        int waveNum;
        readonly float gridStartx = 50;
        readonly float gridStarty = 700;
        readonly float GridYwaveOffset = 20;
        readonly float waveTimeMultiplier = .5f;

        public override float getWaveMult() => waveTimeMultiplier * waveNum;

        LifeCounter lc;
        AnimationSprite SquidAnim, OctoAnim, CrabAnim;
        GridMoveCmd gridMv;

        public override void Resume()
        {
            ResumeCommands();
            ShipManager.ActivateShip();
        }

        public override void prepNextWave()
        {
            SpawnField = true;
        }

        public override void Init()
        {           
            if (SpawnField)
            {
                waveNum++;

                SpawnField = false;
                CreateGrid(gridStartx, gridStarty - (waveNum * GridYwaveOffset));
                SetupShields();
                SetupWalls();

                SquidAnim = new AnimationSprite();

                SquidAnim.Attach(GameSpriteManager.Find(SpriteID.Squid));
                SquidAnim.Attach(ImageManager.Find(ImageID.SquidF1));
                SquidAnim.Attach(ImageManager.Find(ImageID.SquidF2));

                OctoAnim = new AnimationSprite();

                OctoAnim.Attach(GameSpriteManager.Find(SpriteID.Octo));
                OctoAnim.Attach(ImageManager.Find(ImageID.OctoF1));
                OctoAnim.Attach(ImageManager.Find(ImageID.OctoF2));

                CrabAnim = new AnimationSprite();

                CrabAnim.Attach(GameSpriteManager.Find(SpriteID.Crab));
                CrabAnim.Attach(ImageManager.Find(ImageID.CrabF1));
                CrabAnim.Attach(ImageManager.Find(ImageID.CrabF2));

                TimerManager.Add(TimeEventID.Anim, SquidAnim, 1.0f);
                TimerManager.Add(TimeEventID.Anim, CrabAnim, 1.0f);
                TimerManager.Add(TimeEventID.Anim, OctoAnim, 1.0f);

                gridMv = new GridMoveCmd();
                gridMv.SetGrid(GameStateManager.getActiveGrid());

                TimerManager.Add(TimeEventID.GridMoveSFX, new MvSoundCMD(), 1.0f);
                TimerManager.Add(TimeEventID.GridMove, gridMv, 1.0f);
                TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectType.UFORoot).GetGameObject()), GameStateManager.getRandomNumber(12, 36));
                ShipManager.ActivateShip();
            }
            else
            {
                //resetting actives to player A
                GameStateManager.setActiveBombRoot(GameObjectManager.Find(GameObjectType.bombRoot));
                GameStateManager.setActiveGrid(GameObjectManager.Find(GameObjectType.Grid));
                GameStateManager.setActiveNoiseRoot(GameObjectManager.Find(GameObjectType.NoiseRoot));

                gridMv.SetGrid(GameStateManager.getActiveGrid());
                TimerManager.Add(TimeEventID.Anim, SquidAnim, 3.0f);
                TimerManager.Add(TimeEventID.Anim, CrabAnim, 3.0f);
                TimerManager.Add(TimeEventID.Anim, OctoAnim, 3.0f);
                TimerManager.Add(TimeEventID.GridMoveSFX, new MvSoundCMD(), 3.0f);
                TimerManager.Add(TimeEventID.GridMove, gridMv, 3.0f);
                TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectType.UFORoot).GetGameObject()), GameStateManager.getRandomNumber(12, 36));
                TimerManager.Add(TimeEventID.ShipRespawn, new ShipRespawnCMD(), 3.0f);

                GameStateManager.getActiveGrid().RestartBombDrop();

            }

          //  ShipManager.ActivateShip();
            
            lc = new LifeCounter(SpriteID.NullSprite);
            lc.setLives();

            //TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectTypeEnum.UFORoot).getGameObject()), GameStateManager.getRandomNumber(20, 60));
        }

        public override void Render()
        {
            SpriteBatchManager.Draw();
        }

        public override void ResumeCommands()
        {
            TimerManager.Add(TimeEventID.Anim, SquidAnim, 3.0f);
            TimerManager.Add(TimeEventID.Anim, CrabAnim, 3.0f);
            TimerManager.Add(TimeEventID.Anim, OctoAnim, 3.0f);
            TimerManager.Add(TimeEventID.GridMoveSFX, new MvSoundCMD(), 3.0f);
            TimerManager.Add(TimeEventID.GridMove, gridMv, 3.0f);
            TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectType.UFORoot).GetGameObject()), GameStateManager.getRandomNumber(12, 36));

            GameStateManager.getActiveGrid().RestartBombDrop();
            TimerManager.Add(TimeEventID.ShipRespawn, new ShipRespawnCMD(), 3.0f);
        }

        public override void incrementScore(int points)
        {
            GameStateManager.incrementScore1(points);
            nextlifeProgress += points;
            if(nextlifeProgress >= 1000)
            {
                nextlifeProgress -= 1000;
                GameStateManager.AddLife();
            }
        }

        public override void Update(float time)
        {
            Simulation.Update(time);
          
            if (Simulation.getTimeStep() > 0.0f)
            {
                InputManager.Update();

                TimerManager.Update(Simulation.getTimeStep());

                GameObjectManager.Update();

                CollisionPairManager.Process();

                DelayedObjectManager.Process();
            }
        }
    }
}

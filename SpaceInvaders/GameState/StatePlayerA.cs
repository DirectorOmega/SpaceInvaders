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
    class StatePlayerA : GameState
    {
        //move theese out of this to prevent skew when adding playerB
        //TODO: player b is in but theese methods are duplicated I NEED to remove theese to prevent skew
        private void CreateGrid(float startx = 50, float starty = 800, float offsetx = 55.0f, float offsety = -55.0f)
        {
            BombRoot br = new BombRoot(SpriteID.NullSprite, -50, -50);
            br.setName(GameObjectTypeEnum.bombRoot);
            GameObjectManager.AttachTree(br, new PCSTree());
            SpriteBatchManager.Find(SpriteBatchID.Shots).Attach(br.getPSprite());
            br.ActivateCollisionSprite();

            GameStateManager.setActiveBombRoot(GameObjectManager.Find(GameObjectTypeEnum.bombRoot));

            PCSTree pAlienTree = new PCSTree();

            //create the factory
            AlienFactory AF = new AlienFactory(SpriteBatchManager.Find(SpriteBatchID.Alien), pAlienTree);

            //set the parent for hierarchy inside the factory

            //AF.setParent(pAlienTree.getRoot());
            //AF.setParent(null);

            //attach to Root(this is the Grid)

            Grid pGrid = (Grid)AF.Create(GameObjectTypeEnum.Grid, startx, starty);
            GameStateManager.setActiveGrid(GameObjectManager.Find(GameObjectTypeEnum.Grid));
            //Grid pGrid = GameStateManager.getActiveGrid();

            //set Parent
            AF.setParent(pGrid);

            Column pCol;

            for (int i = 0; i < 11; i++)
            {
                // pAlienTree.dumpTree();
                AF.setParent(pGrid);

                pCol = (Column)AF.Create(GameObjectTypeEnum.Column, startx + i * offsetx, starty);

                AF.setParent(pCol);

                AF.Create(GameObjectTypeEnum.Octo, startx + i * offsetx, starty);

                AF.Create(GameObjectTypeEnum.Crab, startx + i * offsetx, starty + 1 * offsety);

                AF.Create(GameObjectTypeEnum.Crab, startx + i * offsetx, starty + 2 * offsety);

                AF.Create(GameObjectTypeEnum.Squid, startx + i * offsetx, starty + 3 * offsety);

                AF.Create(GameObjectTypeEnum.Squid, startx + i * offsetx, starty + 4 * offsety);

            }
        }


        private void setupShields()
        {
            BoxSpriteManager.Add(SpriteID.ShieldBrick, new Azul.Rect(0.0f, 0.0f, 6.0f, 6.0f));

            PCSTree pShieldTree = new PCSTree();

            ShieldFactory SF = new ShieldFactory(SpriteBatchManager.Find(SpriteBatchID.Shield), pShieldTree);

            SF.setShieldRoot(new ShieldRoot(SpriteID.NullSprite,0,0));

            Shield s;
            s = SF.Create(225, 225);
            s = SF.Create(375, 225);
            s = SF.Create(525, 225);
            s = SF.Create(675, 225);

        }

        private void setupWalls()
        {
            int sh = GameStateManager.getScreenHeight();
            int sw = GameStateManager.getScreenWidth();

            BoxSpriteManager.Add(SpriteID.VerticalWall, new Azul.Rect(0.0f, 0.0f, 25.0f,sh));
            //BoxSpriteManager.Add(SpriteID.SideWall, new Azul.Rect(0.0f, 0.0f, 25.0f, this.GetScreenHeight()));
            //TODO: change sprite ID to horizontal wall since I am reusing for top and bottom
            BoxSpriteManager.Add(SpriteID.HorizontalWall, new Azul.Rect(0.0f, 0.0f,sw, 25.0f));

            BoxSpriteManager.Add(SpriteID.Bumper, new Azul.Rect(0.0f, 0.0f, 25.0f, 200.0f));

            GameObject rw = new Sidewall(SpriteID.NullSprite, sw - (24.0f / 2), sh / 2);
            GameObject lw = new Sidewall(SpriteID.NullSprite, 25.0f / 2, sh / 2);
            GameObject tw = new Topwall(SpriteID.NullSprite, sw / 2, sh - 25);
            GameObject bw = new BottomWall(SpriteID.NullSprite, sw / 2, 25);
            GameObject lb = new Bumper(SpriteID.NullSprite, sw / 10, 0);
            GameObject rb = new Bumper(SpriteID.NullSprite, sw - sw / 10, 0);

            lw.getCollisionObject().setColRect(BoxSpriteManager.Find(SpriteID.VerticalWall));
            rw.getCollisionObject().setColRect(BoxSpriteManager.Find(SpriteID.VerticalWall));

            tw.getCollisionObject().setColRect(BoxSpriteManager.Find(SpriteID.HorizontalWall));
            bw.getCollisionObject().setColRect(BoxSpriteManager.Find(SpriteID.HorizontalWall));

            lb.getCollisionObject().setColRect(BoxSpriteManager.Find(SpriteID.Bumper));
            rb.getCollisionObject().setColRect(BoxSpriteManager.Find(SpriteID.Bumper));

            SpriteBatchManager sbmt = SpriteBatchManager.getInstance();
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

            rw.setName(GameObjectTypeEnum.RightWall);
            lw.setName(GameObjectTypeEnum.LeftWall);

            tw.setName(GameObjectTypeEnum.TopWall);
            bw.setName(GameObjectTypeEnum.BottomWall);

            lb.setName(GameObjectTypeEnum.LeftBumper);
            rb.setName(GameObjectTypeEnum.RightBumper);

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

            pColPair = CollisionPairManager.Add(CollisionPairName.MissilevsShield, (GameObject)GameObjectManager.Find(GameObjectTypeEnum.ShieldRoot).getGameObject(), ShipManager.GetMissile());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.MissileExp)));
            pColPair.Attach(new ShieldHitObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.BombvsShield, (GameObject)GameObjectManager.Find(GameObjectTypeEnum.bombRoot).getGameObject(), (GameObject)GameObjectManager.Find(GameObjectTypeEnum.ShieldRoot).getGameObject());
            pColPair.Attach(new ShieldHitObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.AlienShotExp)));
            pColPair.Attach(new BombRemoveObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.BombvsMissile, (GameObject)GameObjectManager.Find(GameObjectTypeEnum.bombRoot).getGameObject(), (GameObject)ShipManager.GetMissile());
            pColPair.Attach(new BombRemoveObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SetTwoShotExplosionObserver(GameSpriteManager.Find(SpriteID.AlienShotExp), GameSpriteManager.Find(SpriteID.MissileExp)));

            GNoiseRoot gnr = new GNoiseRoot(SpriteID.NullSprite);
            gnr.setName(GameObjectTypeEnum.NoiseRoot);
            GameObjectManager.AttachTree(gnr, new PCSTree());
            SpriteBatchManager.Find(SpriteBatchID.Undef).Attach(gnr.getPSprite());
            GameStateManager.setActiveNoiseRoot(GameObjectManager.Find(GameObjectTypeEnum.NoiseRoot));
            gnr.ActivateCollisionSprite();

            pColPair = CollisionPairManager.Add(CollisionPairName.NoisevsShield, (GameObject) GameObjectManager.Find(GameObjectTypeEnum.NoiseRoot).getGameObject(), (GameObject) GameObjectManager.Find(GameObjectTypeEnum.ShieldRoot).getGameObject());
            pColPair.Attach(new RemoveBothObserver());

            UFORoot ufr = new UFORoot(SpriteID.NullSprite);
            ufr.setName(GameObjectTypeEnum.UFORoot);
            GameObjectManager.AttachTree(ufr, new PCSTree(ufr));
            SpriteBatchManager.Find(SpriteBatchID.Alien).Attach(ufr.getPSprite());
            ufr.ActivateCollisionSprite();

            pColPair = CollisionPairManager.Add(CollisionPairName.UFOvsMissile, (GameObject)GameObjectManager.Find(GameObjectTypeEnum.UFORoot).getGameObject(), (GameObject)ShipManager.GetMissile());
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new IncreaseScoreUFOObserver());
            pColPair.Attach(new SetShotExplosionObserver(GameSpriteManager.Find(SpriteID.MissileExp)));
            pColPair.Attach(new UFORemoveObserver());
            pColPair.Attach(new UFODeathSoundObserver());
            //todo: add a ufo death sound start oberver.
            pColPair.Attach(new SetUFOExplosionObserver(GameSpriteManager.Find(SpriteID.UFOExp)));

            pColPair = CollisionPairManager.Add(CollisionPairName.UFOvsLeftWall, (GameObject)GameObjectManager.Find(GameObjectTypeEnum.UFORoot).getGameObject(), lw);
            pColPair.Attach(new UFORemoveObserver());

            pColPair = CollisionPairManager.Add(CollisionPairName.GridvsShield, (GameObject)GameObjectManager.Find(GameObjectTypeEnum.Grid).getGameObject(),(GameObject)GameObjectManager.Find(GameObjectTypeEnum.ShieldRoot).getGameObject());
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
        int waveNum=0;
        float gridStartx = 50;
        float gridStarty = 700;
        float GridYwaveOffset = 20;
        float waveTimeMultiplier = .5f;

        public override float getWaveMult()
        {
            return waveTimeMultiplier*waveNum;
        }
        
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
                setupShields();
                setupWalls();

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
                gridMv.setGrid(GameStateManager.getActiveGrid());

                TimerManager.Add(TimeEventID.GridMoveSFX, new MvSoundCMD(), 1.0f);
                TimerManager.Add(TimeEventID.GridMove, gridMv, 1.0f);
                TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectTypeEnum.UFORoot).getGameObject()), GameStateManager.getRandomNumber(12, 36));
                ShipManager.ActivateShip();
            }
            else
            {
                //resetting actives to player A
                GameStateManager.setActiveBombRoot(GameObjectManager.Find(GameObjectTypeEnum.bombRoot));
                GameStateManager.setActiveGrid(GameObjectManager.Find(GameObjectTypeEnum.Grid));
                GameStateManager.setActiveNoiseRoot(GameObjectManager.Find(GameObjectTypeEnum.NoiseRoot));

                gridMv.setGrid(GameStateManager.getActiveGrid());
                TimerManager.Add(TimeEventID.Anim, SquidAnim, 3.0f);
                TimerManager.Add(TimeEventID.Anim, CrabAnim, 3.0f);
                TimerManager.Add(TimeEventID.Anim, OctoAnim, 3.0f);
                TimerManager.Add(TimeEventID.GridMoveSFX, new MvSoundCMD(), 3.0f);
                TimerManager.Add(TimeEventID.GridMove, gridMv, 3.0f);
                TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectTypeEnum.UFORoot).getGameObject()), GameStateManager.getRandomNumber(12, 36));
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
            TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectTypeEnum.UFORoot).getGameObject()), GameStateManager.getRandomNumber(12, 36));

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

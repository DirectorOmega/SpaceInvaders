using System.Diagnostics;
using SpaceInvaders.GameObjects;
using SpaceInvaders.FontSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Time;
using SpaceInvaders.InputSystem;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.Commands;

namespace SpaceInvaders.GameState
{
    internal sealed class GameStateManager
    {
        //todo reorganize these variables.
        private static GameStateManager pInstance;

        StateIntro SI;
        StateAttract SA;
        StatePlayerA SPA;
        StatePlayerB SPB;
        StateGameOver GO;
        Azul.Game pgame;
  
        private static Random random;

        GameState currentState;
        GameObjectNode pActiveGridNode;
        GameObjectNode pActiveBombRootNode;
        GameObjectNode pActiveNoiseRootNode;

        int Score1, Score2, HighScore;
        bool nextwavepending;
        Font S1, S2, HS;

        //common for all states
        private void ManagerSetup()
        {
        
            TextureManager.Create(3, 2);
            ImageManager.Create();
            GameSpriteManager.Create();
            FontManager.Create(1, 1);
            GlyphManager.Create(1, 1);

            TimerManager.Create();
        }

        //need to do for each player.
        private void addSpriteBatches()
        {
            SpriteBatchManager.Add(SpriteBatchID.Shield);
            SpriteBatchManager.Add(SpriteBatchID.Shots);
            SpriteBatchManager.Add(SpriteBatchID.Ship);
            SpriteBatchManager.Add(SpriteBatchID.Score).setUniversal(true);
            SpriteBatchManager.Add(SpriteBatchID.Alien);
            SpriteBatchManager.Add(SpriteBatchID.Undef);
            SpriteBatchManager.Add(SpriteBatchID.CBox);
        }

        private void loadAssets()
        {
            Texture t = TextureManager.Add(TextureID.Invaders, ISS.GetTexture());

            Azul.Rect aSize = ISS.GetAlienAdjSize();

            Image Squid = ImageManager.Add(ImageID.SquidF1, ISS.GetSquidF1(), t);
            ImageManager.Add(ImageID.SquidF2, ISS.GetSquidF2(), t);

            Image Crab = ImageManager.Add(ImageID.CrabF1, ISS.GetCrabF1(), t);
            ImageManager.Add(ImageID.CrabF2, ISS.GetCrabF2(), t);

            Image Octo = ImageManager.Add(ImageID.OctoF1, ISS.GetOctoF1(), t);
            ImageManager.Add(ImageID.OctoF2, ISS.GetOctoF2(), t);

            Image UFO = ImageManager.Add(ImageID.UFO, ISS.GetUFO(), t);
            Image UFOExp = ImageManager.Add(ImageID.UFOExp, ISS.GetUFOExp(), t);

            Image AlienExp = ImageManager.Add(ImageID.AlienExp, ISS.GetAlienExp(), t);

            Image Hero = ImageManager.Add(ImageID.Hero, ISS.getHero(), t);
            Image HeroShot = ImageManager.Add(ImageID.HeroShot, ISS.getHeroShot(), t);
            Image HeroShotExp = ImageManager.Add(ImageID.MissileExp, ISS.getHeroShotExp(), t);

            Image HeroExp = ImageManager.Add(ImageID.HeroExp1, ISS.getHeroExp1(), t);
            ImageManager.Add(ImageID.HeroExp2, ISS.getHeroExp2(), t);

            GameSpriteManager.Add(SpriteID.ShipExp, HeroExp, ISS.getHeroAdjSize());

            Image AlienShotExp = ImageManager.Add(ImageID.AlienShotExp, ISS.getAlienShotExp(), t);

            Image Rollerf1 = ImageManager.Add(ImageID.Rollerf1, ISS.getRollerShotF1(), t);
            Image Rollerf2 = ImageManager.Add(ImageID.Rollerf2, ISS.getRollerShotF2(), t);
            Image Rollerf3 = ImageManager.Add(ImageID.Rollerf3, ISS.getRollerShotF3(), t);
            Image Rollerf4 = ImageManager.Add(ImageID.Rollerf4, ISS.getRollerShotF4(), t);

            Image ZigZagf1 = ImageManager.Add(ImageID.ZigZagf1, ISS.getZigZagShotF1(), t);
            Image ZigZagf2 = ImageManager.Add(ImageID.ZigZagf2, ISS.getZigZagShotF2(), t);
            Image ZigZagf3 = ImageManager.Add(ImageID.ZigZagf3, ISS.getZigZagShotF3(), t);
            Image ZigZagf4 = ImageManager.Add(ImageID.ZigZagf4, ISS.getZigZagShotF4(), t);

            Image ThirdShotf1 = ImageManager.Add(ImageID.Shot3f1, ISS.getThirdShotF1(), t);
            Image ThirdShotf2 = ImageManager.Add(ImageID.Shot3f2, ISS.getThirdShotF2(), t);
            Image ThirdShotf3 = ImageManager.Add(ImageID.Shot3f3, ISS.getThirdShotF3(), t);
            Image ThirdShotf4 = ImageManager.Add(ImageID.Shot3f4, ISS.getThirdShotF4(), t);

            Image ShieldBrick = ImageManager.Add(ImageID.ShieldBrick, ISS.GetShieldBrick(), t);

            GameSpriteManager.Add(SpriteID.Squid, Squid, aSize);
            GameSpriteManager.Add(SpriteID.Crab, Crab, aSize);
            GameSpriteManager.Add(SpriteID.Octo, Octo, aSize);
            GameSpriteManager.Add(SpriteID.UFO, UFO, ISS.getUFOAdjSize());
            GameSpriteManager.Add(SpriteID.UFOExp, UFOExp, ISS.getUFOAdjSize());

            GameSpriteManager.Add(SpriteID.Hero, Hero, ISS.getHeroAdjSize());
            GameSpriteManager.Add(SpriteID.Missile, HeroShot, ISS.getShotAdjSize());
            GameSpriteManager.Add(SpriteID.MissileExp, HeroShotExp, ISS.getMissileExpAdjSize());
            GameSpriteManager.Add(SpriteID.AlienExp, AlienExp, ISS.GetAlienExpAdjSize());
            GameSpriteManager.Add(SpriteID.AlienShotExp, AlienShotExp, ISS.getAlienShotExpAdjSize());
            GameSpriteManager.Add(SpriteID.ShieldBrick, ShieldBrick, ISS.GetShieldBrickAdjSize());

            GameSpriteManager.Add(SpriteID.RollerF1, Rollerf1, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.RollerF2, Rollerf2, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.RollerF3, Rollerf3, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.RollerF4, Rollerf4, ISS.getAlienShotAdjSize());

            GameSpriteManager.Add(SpriteID.ZigZagF1, ZigZagf1, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.ZigZagF2, ZigZagf2, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.ZigZagF3, ZigZagf3, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.ZigZagF4, ZigZagf4, ISS.getAlienShotAdjSize());

            GameSpriteManager.Add(SpriteID.ThirdShotF1, ThirdShotf1, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.ThirdShotF2, ThirdShotf2, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.ThirdShotF3, ThirdShotf3, ISS.getAlienShotAdjSize());
            GameSpriteManager.Add(SpriteID.ThirdShotF4, ThirdShotf4, ISS.getAlienShotAdjSize());

            //this is a color test
            baseSprite SquidSprite = GameSpriteManager.Add(SpriteID.Squid, Squid, aSize);
            baseSprite CrabSprite = GameSpriteManager.Add(SpriteID.Crab, Crab, aSize);
            baseSprite OctoSprite = GameSpriteManager.Add(SpriteID.Octo, Octo, aSize);
            baseSprite HeroSprite = GameSpriteManager.Add(SpriteID.Hero, Hero, ISS.getHeroAdjSize());

            SquidSprite.setColor(255.0f, 0.0f, 0.0f);
            CrabSprite.setColor(0.0f, 255.0f, 0.0f);
            OctoSprite.setColor(0.0f, 0.0f, 255.0f);
  
            Texture pTexture = TextureManager.Add(TextureID.InvadersText, "SpaceInvadersMono4.tga");

            FontManager.AddXml(Glyph.Name.InvadersText, "SpaceInvadersMono4.xml", TextureID.InvadersText);

            FontManager.Add(FontName.ScoreOneLabel, SpriteBatchID.Score, "SCORE<1>", Glyph.Name.InvadersText, pgame.GetScreenWidth() / 10 - 50, pgame.GetScreenHeight() - 30);
            FontManager.Add(FontName.ScoreTwoLabel, SpriteBatchID.Score, "SCORE<2>", Glyph.Name.InvadersText, pgame.GetScreenWidth() - 265, pgame.GetScreenHeight() - 30);
            FontManager.Add(FontName.HighScoreLabel, SpriteBatchID.Score, "HI-SCORE", Glyph.Name.InvadersText, pgame.GetScreenWidth() / 2 - 115, pgame.GetScreenHeight() - 30);

            FontManager.Add(FontName.CreditLabel, SpriteBatchID.Score, "CREDIT", Glyph.Name.InvadersText, pgame.GetScreenWidth() - 450, 30);
           
            FontManager.Add(FontName.ScoreOne, SpriteBatchID.Score, "0000", Glyph.Name.InvadersText, pgame.GetScreenWidth() / 10 - 10, pgame.GetScreenHeight() - 70);
            FontManager.Add(FontName.ScoreTwo, SpriteBatchID.Score, "0000", Glyph.Name.InvadersText, pgame.GetScreenWidth() - 225, pgame.GetScreenHeight() - 70);
            FontManager.Add(FontName.HighScore, SpriteBatchID.Score, "    ", Glyph.Name.InvadersText, pgame.GetScreenWidth() / 2 - 75, pgame.GetScreenHeight() - 70);

            FontManager.Add(FontName.CreditCount, SpriteBatchID.Score, "00", Glyph.Name.InvadersText, pgame.GetScreenWidth() - 200, 30);

            FontManager.Add(FontName.LifeCount, SpriteBatchID.Score, "0", Glyph.Name.InvadersText, 80, 25);
        }

        internal static void destroy()
        {
         //TODO: implement unload content.
        }

        private void setupInput()
        {
            InputSubject pInputSubject;
            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

            pInputSubject = InputManager.GetEnterSubject();
            pInputSubject.Attach(new EnterObserver());

            pInputSubject = InputManager.GetOSubject();
            pInputSubject.Attach(new OKeyObserver());

            pInputSubject = InputManager.GetKey1Subject();
            pInputSubject.Attach(new Num1KeyObserver());
        }

        //TODO: Add more null objects, anywhere null is a default
        //I want a null object to prevent null from being an issue.
        Font creditCountFont;
        private GameStateManager(Azul.Game game)
        {
            pgame = game;
            ManagerSetup();
            addSpriteBatches();
            loadAssets();
            setupInput();
            //move grid creation into state.
            //CreateGrid();
            ShipManager.Create();

            SI = new StateIntro();
            SA = new StateAttract();
            SPA = new StatePlayerA();
         
            SPB = new StatePlayerB();
           // SPB.Init();
            GO = new StateGameOver();

            random = new Random();
            nextwavepending = false;

            S1 = FontManager.Find(FontName.ScoreOne);
            S2 = FontManager.Find(FontName.ScoreTwo);
            HS = FontManager.Find(FontName.HighScore);

            creditCountFont = FontManager.Find(FontName.CreditCount);

            Score1 = 0;
            Score2 = 0;
            HighScore = 0;
            currentState = SI;
            currentState.Init();
            Simulation.Create();
            //HighScore.updateScore(GameStateManager.getHighScore());
        }

        public static void PlayerOneCredit()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            pMan.creditCount--;
            pMan.creditCountFont.updateCredit(pMan.creditCount);
            pMan.SPA.setLives(3);
        }

        public static void PlayerTwoCredit()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            pMan.creditCount--;
            pMan.creditCountFont.updateCredit(pMan.creditCount);
            pMan.SPB.setLives(3);
        }

        public static void ResumeCommands()
        {
            GameStateManager.getInstance().currentState.ResumeCommands();
        }
        
        public static void nextWaveReady()
        {
            GameStateManager.getInstance().nextwavepending = false;
        }

        public static int getScreenHeight()
        {
           return GameStateManager.getInstance().pgame.GetScreenHeight();
        }

        public static int getScreenWidth()
        {
           return GameStateManager.getInstance().pgame.GetScreenWidth();
        }

        public static void incrementScore(int points)
        {
            GameStateManager.getInstance().currentState.incrementScore(points);
        }

        internal static void AddLife()
        {
            GameStateManager.getInstance().currentState.addLife();
        }

        internal static int getLifeCount()
        {
            return GameStateManager.getInstance().currentState.getLifeCount();
        }

        public static int getScore1()
        {
            return GameStateManager.getInstance().Score1;
        }

        public static void incrementScore1(int points)
        {
            GameStateManager gMan = GameStateManager.getInstance();
            gMan.S1.updateScore(gMan.Score1 += points);
        }

        public static void incrementScore2(int points)
        {
            GameStateManager gMan = GameStateManager.getInstance();
            gMan.S2.updateScore(gMan.Score2 += points);
        }

        public static int getScore2()
        {
            return GameStateManager.getInstance().Score2;
        }

        public static int getHighScore()
        {
            return GameStateManager.getInstance().HighScore;
        }

        public static void updateHighScore()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            if(pMan.HighScore < pMan.Score1)
            {
                pMan.HighScore = pMan.Score1;
            }
            if (pMan.HighScore < pMan.Score2)
            {
                pMan.HighScore = pMan.Score2;
            }
            pMan.HS.updateScore(pMan.HighScore);
        }

        public static void ResetPlayers()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            pMan.SPA.Reset();

            pMan.S1.updateScore(0);
            pMan.SPB.Reset();
            pMan.S2.updateScore(0);
        }

        public static void Create(Azul.Game g)
        {
            
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GameStateManager(g);                
            }
        }

        public static GameStateManager getInstance()
        {
            if (pInstance == null)
            {
                Debug.Assert(false);
                //GameStateManager.Create();
            }
            return pInstance;
        }

        public static void setStateAttract()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            Debug.Assert(pMan != null);

            pMan.currentState = pMan.SA;
            pMan.currentState.Init();
        }

        int creditCount;
        public static void addCredit()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            pMan.creditCount++;
            pMan.creditCountFont.updateCredit(pMan.creditCount);
        }

        public static int getCredits()
        {
            return GameStateManager.getInstance().creditCount;
        }

        //TODO: change to ask the state for the time. I need to get rid of theese stupid
        //gamestate held refrences to shit.
        internal static float getTimeDelta()
        {
            return 0.01f + (.015f*GameStateManager.getInstance().pActiveGridNode.GetTree().numNodes);
        }

        internal static float getWaveMult()
        {
            return GameStateManager.getInstance().currentState.getWaveMult();
        }
        
        //expand on this later.
        //also optimize if time permits.
        //this is where I want to add the cmd for the end of the level.
        internal static bool GridEmpty()
        {
            //if(GameStateManager.getInstance().pActiveGridNode.getTree().numNodes > 0)
            if (GameStateManager.getInstance().pActiveGridNode.GetTree() != null)
            {
                return false;
            }
            else
            {
                if (GameStateManager.getInstance().nextwavepending != true)
                {
                    TimerManager.Add(TimeEventID.NextWave, new NextWaveCommand(GameStateManager.getInstance().currentState), 2.0f);
                    GameStateManager.getInstance().nextwavepending = true;
                    //ShipManager.GetShip().SetMvState(ShipManager.eMvState.Neither);
                    ShipManager.GetShip().SetMiState(ShipManager.eMiState.End);
                }
                return true;
            }
           // return GameStateManager.getInstance().pActiveGridNode.getTree().numNodes > 0 ? false : true;
        }

        public static void setStateGameOver()
        {
            GameStateManager.updateHighScore();


            GameStateManager pMan = GameStateManager.getInstance();
            pMan.Score1 = 0;
            pMan.Score2 = 0;
            pMan.S1.updateScore(0);

            //pMan.SPA.Reset();
            //pMan.SPB.Reset();
         
            Debug.Assert(pMan != null);
            pMan.currentState = pMan.GO;

            pMan.currentState.Init();

        //  pMan.pActiveGridNode = null;
        //  pMan.pActiveBombRootNode = null;
        //   pMan.pActiveNoiseRootNode = null;
        }

        internal static void LoseLife()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            pMan.currentState.loseLife();


            if (pMan.currentState == pMan.SPA)
            {
                if (pMan.SPB.getLifeCount() > 0)
                {
                    TimerManager.Add(TimeEventID.NextPlayer, new PlayerBCommand(), 1.5f);
                }
            }
            if (pMan.currentState == pMan.SPB)
            {
                if (pMan.SPA.getLifeCount() > 0)
                {
                    TimerManager.Add(TimeEventID.NextPlayer, new PlayerACommand(), 1.5f);
                }
            }

            if (pMan.SPA.getLifeCount() == 0 && pMan.SPB.getLifeCount() == 0)
            {
                // GameStateManager.setStateGameOver();
                TimerManager.Add(TimeEventID.GameOver, new GameOverCMD(),.2f);
            }



            //if (pMan.currentState == pMan.SPA)
            //{
            //    if (pMan.SPB.getLifeCount() > 0)
            //    {
            //        pMan.currentState.Store();
            //        pMan.currentState = pMan.SPB;
            //        GameStateManager.setStatePB();
            //        pMan.currentState.Resume();
            //    }
            //}
            //if (pMan.currentState == pMan.SPB)
            //{
            //    if (pMan.SPA.getLifeCount() > 0)
            //    {
            //        pMan.currentState.Store();
            //        //pMan.currentState = pMan.SPA;
            //        GameStateManager.setStatePA();
            //        pMan.currentState.Resume();
            //    }
            //}

        }

        public static void setStatePA()
        {
            //add the multi level and swapping player logic.
            GameStateManager pMan = GameStateManager.getInstance();
            Debug.Assert(pMan != null);
            pMan.currentState = pMan.SPA;
            pMan.currentState.Init();

            pMan.pActiveGridNode = GameObjectManager.Find(GameObjectType.Grid);

            pMan.pActiveBombRootNode = GameObjectManager.Find(GameObjectType.bombRoot);

            pMan.pActiveNoiseRootNode = GameObjectManager.Find(GameObjectType.NoiseRoot);   
        }

        public static void setStatePB()
        {
            //add the multi level and swapping player logic.
            GameStateManager pMan = GameStateManager.getInstance();
            Debug.Assert(pMan != null);
            pMan.currentState = pMan.SPB;
            pMan.currentState.Init();

            pMan.pActiveGridNode = GameObjectManager.Find(GameObjectType.Grid);

            pMan.pActiveBombRootNode = GameObjectManager.Find(GameObjectType.bombRoot);

            pMan.pActiveNoiseRootNode = GameObjectManager.Find(GameObjectType.NoiseRoot);
        }



        internal static void setActiveNoiseRoot(GameObjectNode gnr)
        {
            GameStateManager.getInstance().pActiveNoiseRootNode = gnr; 
        }
        internal static GNoiseRoot getActiveNoiseRoot()
        {
            return (GNoiseRoot)GameStateManager.getInstance().pActiveNoiseRootNode.GetGameObject();
        }
        internal static Grid getActiveGrid()
        {
            // return (Grid) GameObjectManager.Find(GameObjectTypeEnum.Grid).getGameObject();
            return (Grid) GameStateManager.getInstance().pActiveGridNode.GetGameObject();
        }

        internal static void setActiveGrid(GameObjectNode gridnode)
        {
            GameStateManager.getInstance().pActiveGridNode = gridnode;
        }

        internal static BombRoot getActiveBombRoot()
        {
            return (BombRoot)GameStateManager.getInstance().pActiveBombRootNode.GetGameObject();
        }

        internal static void setActiveBombRoot(GameObjectNode bombrootnode)
        {
            GameStateManager.getInstance().pActiveBombRootNode = bombrootnode;
        }

        public void setStateGO()
        {
            currentState = GO;
        }

        public static void Update(float time)
        {
            GameStateManager pMan = GameStateManager.getInstance();
            Debug.Assert(pMan != null);
            pMan.currentState.Update(time);
        }

        public static void Render()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            Debug.Assert(pMan != null);
            pMan.currentState.Render();
        }

        public static void Enter()
        {
            GameStateManager pMan = GameStateManager.getInstance();
            Debug.Assert(pMan != null);
            pMan.currentState.Handle();
        }


        public static int getRandomNumber(int lower,int upper)
        {
            return random.Next(lower, upper);
        }
        //internal static void Create(mySpaceInvaders mySpaceInvaders)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

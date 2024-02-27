using SpaceInvaders.FontSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.InputSystem;
using SpaceInvaders.Time;

namespace SpaceInvaders.GameState
{
    class StateAttract : GameState
    {
        Font DefaultMessage;
        Font Score1Label, Score2Label, HighScoreLabel, Score1, Score2, HighScore;
        Font Play, Space, Invaders, SAT;
        Font UFOLine, SquidLine, OctoLine, CrabLine;

        Font Push, SinglePlayer, MultiPlayer;
        //I'm using game sprites, because I found that if I use proxies here the animation applies to theese
        //so if the game ends on the wrong animation frame, theese guys are the second frame of thier anim.
        //I  know I could swap it but on the last day I'm trying to change as little as possibble.

        GameSprite UFO, OCTO, CRAB, SQUID;
        public override void Handle()
        {

            int creds = GameStateManager.getCredits();
            if (creds>0)
            {
                if(creds > 1)
                {
                    GameStateManager.PlayerTwoCredit();
                }

                GameStateManager.PlayerOneCredit();

                GameStateManager.setStatePA();
            }
           // GameStateManager.setStatePA();
        }

        public override void Init()
        {
            DefaultMessage = new Font();
            Play = new Font();
            Space = new Font();
            Invaders = new Font();
            SAT = new Font();
            UFOLine = new Font();
            SquidLine = new Font();
            OctoLine = new Font();
            CrabLine = new Font();

            SinglePlayer = new Font();
            Push = new Font();
            MultiPlayer = new Font();

            UFO = GameSpriteManager.Add(SpriteID.AttractUFO, ImageManager.Find(ImageID.UFO), ISS.getUFOAdjSize());
            OCTO = GameSpriteManager.Add(SpriteID.AttractOCTO, ImageManager.Find(ImageID.OctoF1), ISS.getAlienAdjSize());
            SQUID = GameSpriteManager.Add(SpriteID.AttractSQUID, ImageManager.Find(ImageID.SquidF1), ISS.getAlienAdjSize());
            CRAB = GameSpriteManager.Add(SpriteID.AttractCRAB, ImageManager.Find(ImageID.CrabF1), ISS.getAlienAdjSize());
            
            // DefaultMessage.Set(FontName.GameOverLabel, "Press Enter to Start", Glyph.Name.Consolas36pt, 200, 400);
            //DefaultMessage.Set(FontName.GameOverLabel, "Press Enter to Start", Glyph.Name.InvadersText, 150, 400);

            Play.Set(FontName.AttractPlay, "PLAY", Glyph.Name.InvadersText, 370, 800);
            Space.Set(FontName.AttractSpace, "SPACE", Glyph.Name.InvadersText, 170, 700);
            Invaders.Set(FontName.AttractInvaders, "INVADERS", Glyph.Name.InvadersText, 450, 700);
            SAT.Set(FontName.SAT, "*SCORE ADVANCE TABLE*", Glyph.Name.InvadersText, 120, 500);

            SinglePlayer.Set(FontName.AttractSpace, "FOR SINGLEPLAYER",Glyph.Name.InvadersText,220f,500f);
            MultiPlayer.Set(FontName.AttractSpace, "FOR MULTIPLAYER",Glyph.Name.InvadersText,220f,500f);
            Push.Set(FontName.AttractPush, "PUSH ENTER",Glyph.Name.InvadersText,300,600);
            UFOLine.Set(FontName.UFOLine, "=? MYSTERY", Glyph.Name.InvadersText, 300, 450);
            UFO.setCoords(225, 450);
            UFO.Update();
            OctoLine.Set(FontName.OctoLine, "=30 POINTS", Glyph.Name.InvadersText, 300, 400);
            OCTO.setCoords(225, 400);
            OCTO.Update();
            CrabLine.Set(FontName.CrabLine, "=20 POINTS", Glyph.Name.InvadersText, 300, 350);
            CRAB.setCoords(225, 350);
            CRAB.Update();
            SquidLine.Set(FontName.SquidLine, "=10 POINTS", Glyph.Name.InvadersText, 300, 300);
            SQUID.setCoords(225, 300);
            SQUID.Update();

            Score1Label = FontManager.Find(FontName.ScoreOneLabel);
            Score2Label = FontManager.Find(FontName.ScoreTwoLabel);
            HighScoreLabel = FontManager.Find(FontName.HighScoreLabel);

            Score1 = FontManager.Find(FontName.ScoreOne);
            Score2 = FontManager.Find(FontName.ScoreTwo);
            HighScore = FontManager.Find(FontName.HighScore);
        }

        public override void Render()
        {
            SpriteBatchManager.Draw();

            if (GameStateManager.getCredits() == 0)
            {
              //  DefaultMessage.Render();
                Play.Render();
                Space.Render();
                Invaders.Render();
                SAT.Render();

                UFOLine.Render();
                SquidLine.Render();
                CrabLine.Render();
                OctoLine.Render();
                SquidLine.Render();

                UFO.Render();
                SQUID.Render();
                OCTO.Render();
                CRAB.Render();

            }
            else
            {
                Push.Render();
                if(GameStateManager.getCredits() == 1)
                {
                    SinglePlayer.Render();
                }
                else
                {
                    MultiPlayer.Render();
                }
            }
            



            //Score1Label.Render();
            //Score2Label.Render();
         
            ///HighScoreLabel.Render();
            //Score1.Render();
            //Score2.Render();
            //HighScore.Render();
        }

        public override void Update(float time)
        {
            Simulation.Update(time);

            if (Simulation.getTimeStep() > 0.0f)
            {
                InputManager.Update();

                TimerManager.Update(Simulation.getTimeStep());
            }

           
        }
    }
}

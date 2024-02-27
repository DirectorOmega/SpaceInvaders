using SpaceInvaders.CollisionSystem;
using SpaceInvaders.FontSystem;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.InputSystem;
using SpaceInvaders.Time;

namespace SpaceInvaders.GameState
{
    internal sealed class StateGameOver : GameState
    {
        Font DefaultMessage;
        Font Score1Label, Score2Label, HighScoreLabel, Score1, Score2, HighScore;

        public override void Handle()
        {
            GameStateManager.setStateAttract();
        }

        public override void Init()
        {

            DefaultMessage = new Font();
//            DefaultMessage.Set(FontName.GameOverLabel, "GAME OVER", Glyph.Name.Consolas36pt, 200, 400);

            DefaultMessage.Set(FontName.GameOverLabel, "GAME OVER", Glyph.Name.InvadersText, 250, 400);

            //DefaultMessage.pFontSprite.setScale(5, 5);
            DefaultMessage.pFontSprite.SetColor(255.0f, 0.0f, 0.0f);
            DefaultMessage.pFontSprite.Update();
            Score1Label = FontManager.Find(FontName.ScoreOneLabel);
            Score2Label = FontManager.Find(FontName.ScoreTwoLabel);
            HighScoreLabel = FontManager.Find(FontName.HighScoreLabel);

            Score1 = FontManager.Find(FontName.ScoreOne);
            Score2 = FontManager.Find(FontName.ScoreTwo);
            HighScore = FontManager.Find(FontName.HighScore);
            // GameStateManager.updateHighScore();

            //TimerManager.Clear(TimeEventID.bombDrop);

            //TimerManager.Clear(TimeEventID.GridMove);

            //TimerManager.Clear(TimeEventID.GridMoveSFX);
            DelayedObjectManager.Clear();

            GameObjectManager.Clear();
            GameObjectManager.ClearStored();

            CollisionPairManager.Clear();
            CollisionPairManager.ClearStored();

            SpriteBatchManager.ClearStored();

            GameStateManager.ResetPlayers();

            //TimerManager.Clear();
            TimerManager.Clear(TimeEventID.bombDrop);

            TimerManager.Clear(TimeEventID.create);

            TimerManager.Clear(TimeEventID.GridMove);

            TimerManager.Clear(TimeEventID.ShipRespawn);

            TimerManager.Clear(TimeEventID.GridMoveSFX);

            TimerManager.Clear(TimeEventID.Anim);

            //TimerManager.Add(TimeEventID.Clear, new TimeEventClearCMD(), 5.0f);

            //TimerManager.Add
        }

        public override void Render()
        {
            SpriteBatchManager.Draw();

            DefaultMessage.Render();
            //Score1Label.Render();
            //Score2Label.Render();
            //HighScoreLabel.Render();
            //Score1.Render();
            //Score2.Render();
            //HighScore.Render();
        }

        public override void Update(float time)
        {
            //Fix this, I need to clear the time events after the game ends so I can use
            //the time to restart.
            if (Simulation.getTimeStep() > 0.0f)
            {
                InputManager.Update();

                TimerManager.Update(Simulation.getTimeStep());

                //CollisionPairManager.Process();
                //todo setup clear's for managers below.
                GameObjectManager.Update();

                CollisionPairManager.Process();

                DelayedObjectManager.Process();

                //TimerManager.Update(Simulation.getTimeStep());
            }
            //InputManager.Update();
        }
    }
}

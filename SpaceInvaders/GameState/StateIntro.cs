using SpaceInvaders.FontSystem;
using SpaceInvaders.Time;
using SpaceInvaders.Commands;

namespace SpaceInvaders.GameState
{
    internal sealed class StateIntro : GameState
    {

        Font Line1, Line2, Line3, Line4;

        public override void Handle()
        {
           GameStateManager.setStateAttract();
        }

        //private void setupInput()
        //{
        //    InputSubject pInputSubject;
        //    pInputSubject = InputManager.GetArrowRightSubject();
        //    pInputSubject.Attach(new MoveRightObserver());

        //    pInputSubject = InputManager.GetArrowLeftSubject();
        //    pInputSubject.Attach(new MoveLeftObserver());

        //    pInputSubject = InputManager.GetSpaceSubject();
        //    pInputSubject.Attach(new ShootObserver());

        //    pInputSubject = InputManager.GetEnterSubject();
        //    pInputSubject.Attach(new EnterObserver());

        //    pInputSubject = InputManager.GetOSubject();
        //    pInputSubject.Attach(new OKeyObserver());

        //    pInputSubject = InputManager.GetKey1Subject();
        //    pInputSubject.Attach(new Num1KeyObserver());

        //}

        public override void Init()
        {
            Line1 = new Font();
            Line2 = new Font();
            Line3 = new Font();
            Line4 = new Font();

            //DefaultMessage.Set(FontName.IntroLabel, "Intro : wait 5 seconds", Glyph.Name.Consolas36pt, 200, 400);

            Line1.Set(FontName.IntroLabel, "   SE456", Glyph.Name.InvadersText, 256, 800);
            Line2.Set(FontName.IntroLabel, "Robert Cole", Glyph.Name.InvadersText, 256, 700);
            Line3.Set(FontName.IntroLabel, "Space Invaders", Glyph.Name.InvadersText, 225, 500);

            TimerManager.Add(TimeEventID.GameStateAdv, new AdvanceStateCMD(), 2.0f);
        }

        public override void Render()
        {
            Line1.Render();
            Line2.Render();
            Line3.Render();
        }

        public override void Update(float time)
        {
            Simulation.Update(time);

            if (Simulation.getTimeStep() > 0.0f)
            {
                TimerManager.Update(Simulation.getTimeStep());
            }

        }
    }
}

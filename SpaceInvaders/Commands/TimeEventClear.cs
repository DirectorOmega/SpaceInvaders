using SpaceInvaders.Time;

namespace SpaceInvaders.Commands
{
    class TimeEventClearCMD : Command
    {
        public override void execute(float deltaTime)
        {
            TimerManager.Clear(TimeEventID.bombDrop);

            TimerManager.Clear(TimeEventID.GridMove);

            TimerManager.Clear(TimeEventID.GridMoveSFX);

        }
    }
}

using SpaceInvaders.GameObjects;
using SpaceInvaders.GameState;
using SpaceInvaders.Time;

namespace SpaceInvaders.Commands
{
    internal sealed class GridMoveCmd : Command
    {
        private Grid pGrid;

        public GridMoveCmd() => pGrid = null;

        public void SetGrid(Grid grid) => pGrid = grid;

        public override void execute(float deltaTime)
        { 
            pGrid.MoveGrid();
            if (!GameStateManager.GridEmpty())
               TimerManager.Add(TimeEventID.Anim, this, GameStateManager.getTimeDelta());
        }
    }
}

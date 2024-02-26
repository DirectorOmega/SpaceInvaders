using SpaceInvaders.GameObjects;
using SpaceInvaders.GameState;

namespace SpaceInvaders.Commands
{
    class UFOSpawnCMD : Command
    {
        private GameObject gameObject;

        public UFOSpawnCMD(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        //TODO: cleanup new here, use factory to bypass object creation.
        public override void execute(float deltaTime)
        {
            if (gameObject.getChild() == null)
            {
                UFO ufo = new UFO(GraphicalObjects.SpriteID.UFO);
                ufo.setName(GameObjectTypeEnum.UFO);
                GameObjectManager.Insert(ufo, gameObject);
                ufo.startSound();

                ufo.setCoords(GameStateManager.getScreenWidth() - 50, 900);
                gameObject.getPSprite().getSBNode().getSBNM().Attach(ufo.getPSprite());
                ufo.ActivateCollisionSprite();
            }

        }
    }
}

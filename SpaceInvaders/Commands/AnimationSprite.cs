using SpaceInvaders.GameState;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Manager;
using SpaceInvaders.Time;

namespace SpaceInvaders.Commands
{
    class AnimationSprite : Command
    {
        GameSprite pSprite;
        ImageHolder pCurrImage;
        DLink pFirstImage;

        public AnimationSprite()
        {
             pSprite = null;
             pCurrImage = null;
             pFirstImage = null;
        }

        public void Attach(GameSprite pSprite)
        {
            this.pSprite = pSprite;
        }

        public void Attach(Image toAdd)
        {
            ImageHolder tA = new ImageHolder();
            tA.setImage(toAdd);

            if (null == pCurrImage)
            {
                pCurrImage = tA;
            }
            DLink.addToFront(ref pFirstImage, tA);
        }

        public override void execute(float deltaTime)
        {
            if (null == pCurrImage.pNext)
            {
                pCurrImage = (ImageHolder)pFirstImage;

            }
            else
            {
                pCurrImage = (ImageHolder)pFirstImage.pNext;
            }
            pSprite.swapImage(pCurrImage.getImage());
            if (!GameStateManager.GridEmpty())
            {
                TimerManager.Add(TimeEventID.Anim, this, GameStateManager.getTimeDelta());
            }
        }
    }
}

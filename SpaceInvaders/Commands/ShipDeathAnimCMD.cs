//using SpaceInvaders.GraphicalObjects;
//using SpaceInvaders.Manager;
//using SpaceInvaders.Time;

//namespace SpaceInvaders.Commands
//{
//    internal class ShipDeathAnimCMD : Command
//    {
//        float duration;
//        GameSprite pDeathSprite;
//        ImageHolder pCurrImage;
//        DLink pFirstImage;

//       public ShipDeathAnimCMD()
//        {
//            pDeathSprite = GameSpriteManager.Find(SpriteID.ShipExp);
//            Attach(ImageManager.Find(ImageID.HeroExp1));
//            Attach(ImageManager.Find(ImageID.HeroExp2));
//        }

//        private void Attach(Image toAdd)
//        {
//            ImageHolder tA = new ImageHolder();
//            tA.setImage(toAdd);

//            if (null == pCurrImage)
//            {
//                pCurrImage = tA;
//            }
//            DLink.addToFront(ref pFirstImage, tA);
//        }

//        public void setAnim(float x, float y, float d,SpriteBatch sb)
//        {
//            sb.Attach(pDeathSprite);
//            pDeathSprite.setCoords(x, y);
//            pDeathSprite.Update();
//            duration = d;
//        }

//        public override void execute(float deltaTime)
//        {
//            duration -= deltaTime;
//            if (null == pCurrImage.pNext)
//            {
//                pCurrImage = (ImageHolder)pFirstImage;
//            }
//            else
//            {
//                pCurrImage = (ImageHolder)pFirstImage.pNext;
//            }
//            pDeathSprite.swapImage(pCurrImage.getImage());
//            if (duration >0)
//            {
//                pDeathSprite.Update();
//                TimerManager.Add(TimeEventID.Anim, this, 0.5f);
//            }
//            else
//            {
//                pDeathSprite.getSBNode().RemoveSelf();
//            }
//        }
//    }
//}
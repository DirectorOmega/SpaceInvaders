using System.Diagnostics;

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class ProxySprite : baseSprite
    {
        private GameSprite pSprite;
        //I could add a color here, to override the base sprite color.
        //but I would have to assign it to every proxysprite.

        private void pPushToReal()
        {
            this.pSprite.setCoords(this.x, this.y);
        }

        //kind of a bad smell, but I am doing this to let the bombs switch on thier own timer.
        public override void swapSprite(GameSprite gs)
        {
            pSprite = gs;
        }
   

        public override void swapImage(Image I)
        {
            //currently I don't want the proxy image to access the underlying sprites Image, 
            //but I thought I might want to so I set it up
            Debug.Assert(false);
            pSprite.swapImage(I);
        }

        protected override void cClean()
        {
           pSprite = null;
           x = 0.0f;
           y = 0.0f;
        }

        public override void Render()
        {
                this.pPushToReal();

                this.pSprite.Update();
                this.pSprite.Render();
        }

        internal void Set(GameSprite realSprite)
        {
            Debug.Assert(null != realSprite);
            pSprite = realSprite;
        }

        public override float getAngle()
        {
            return pSprite.getAngle();
        }

        public override Azul.Rect getScreenRect()
        {
            return pSprite.getScreenRect();
        }

        public override void setScreenRect(Azul.Rect screenRect)
        {
            pSprite.setScreenRect(screenRect);
        }

        public override float getSX()
        {
            return pSprite.getSX();
        }

        public override float getSY()
        {
            return pSprite.getSY();
        }

        public override void setAngle(float angle)
        {
            this.pSprite.setAngle(angle);
        }

        public override void setScale(float sx, float sy)
        {
            pSprite.setScale(sx, sy);
        }

        public override void Update()
        {/*Empty because it is a proxy*/}

        public override void setColor(float red, float green, float blue, float alpha = 1)
        {
            pSprite.setColor(red, green, blue, alpha);
        }

        public override SpriteID getName()
        {
            return pSprite.getName();
        }
        //WARNING!: this would change the name of all instances of the base sprite.
        public override void setName(SpriteID name)
        {
            pSprite.setName(name);
        }
    }
}

using System.Diagnostics;

namespace SpaceInvaders.GraphicalObjects
{
    abstract class baseSprite : SLink
    {
        protected float x, y;
        private SpriteBatchNode pSBNode;

        protected baseSprite() { }
        ~baseSprite() { pSBNode = null; }

        public virtual void swapSprite(GameSprite gs)
        {
            Debug.Assert(false);
        }

        public virtual void swapImage(Image i)
        {
            Debug.Assert(false);
        }

        public void setSBNode(SpriteBatchNode p)
        {
            pSBNode = p;
        }

        public float getX()
        {
            return x;
        }
        public float getY()
        {
            return y;
        }
        public void setCoords(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public override void dClean()
        {
            pSBNode = null;
            this.cClean();
        }

        public SpriteBatchNode getSBNode()
        {
            return pSBNode;
        }
        protected abstract void cClean();

        public abstract SpriteID getName();
        public abstract void setName(SpriteID name);
        public abstract Azul.Rect getScreenRect();
        public abstract void setScreenRect(Azul.Rect r);
        public abstract void setColor(float red, float green, float blue, float alpha = 1);

        public abstract float getAngle();

        public abstract void setAngle(float angle);

        public abstract float getSX();

        public abstract float getSY();

        public abstract void setScale(float sx, float sy);

        public abstract void Update();

        public abstract void Render();

    }
}

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class GameSprite : baseSprite
    {
        private SpriteID Name;
        private float sx, sy;
        private float angle;
        private Azul.Rect poScreenRect;
        private Image pImage;
        private Azul.Sprite poSprite;
        private Azul.Color poColor;
        
        public GameSprite()
        {
            Name = SpriteID.Undef;
            sx = 1;
            sy = 1;
            poScreenRect = new Azul.Rect();
            poSprite = new Azul.Sprite();
            poColor = new Azul.Color(255f,255f,255f);
        }

        ~GameSprite()
        {
            Name = SpriteID.Undef;
            poSprite = null;
            pImage = null;
            poScreenRect = null;
            poColor = null;
        }

        internal void Set(SpriteID name, Image image, Azul.Rect SRect)
        {
            Name = name;
            pImage = image;

            poSprite.SwapTexture(image.getAzTex());
            poSprite.SwapTextureRect(image.getAzRect());
            poScreenRect.Set(SRect);
            poSprite.SwapScreenRect(poScreenRect);
        }

        public GameSprite(SpriteID name)
        {
            cClean();
            Name = name;
        }

        public override void swapImage(Image i)
        {
            pImage = i;
            poSprite.SwapTextureRect(pImage.getAzRect());
        }

        public void Set(SpriteID name, Image image,Azul.Rect screenRect, Azul.Color color)
        {
            Name = name;
            pImage = image;

            poSprite.SwapTexture(image.getAzTex());
            poSprite.SwapTextureRect(image.getAzRect());
            poSprite.SwapScreenRect(screenRect);
            poScreenRect.Set(screenRect);
            poColor.Set(color);
        }

        public override void Render()
        {
            poSprite.Render();  
        }
     
        public override void Update()
        {
            poSprite.x = this.getX();
            poSprite.y = this.getY();
            
            poSprite.Update();
        }

        protected override void cClean()
        {
            Name = SpriteID.Undef;
            sx = 1;
            sy = 1;
            pImage = null;
        }

        public override float getAngle()
        {
            return angle;
        }

        public override float getSX()
        {
            return sx;
        }

        public override float getSY()
        {
            return sy;
        }

        public override void setAngle(float angle)
        {
            this.angle = angle;
            poSprite.angle = this.angle;
        }

        public override void setScale(float sx, float sy)
        {
            this.sx = sx;
            this.sy = sy;
            poSprite.sx = this.sx;
            poSprite.sy = this.sy;
        }

        override public Azul.Rect getScreenRect()
        {
            return poScreenRect;
        }

        public override void setScreenRect(Azul.Rect screenRect)
        {
            poScreenRect.Set(screenRect);
            poSprite.SwapScreenRect(screenRect);
        }

        public override void setColor(float red, float green, float blue, float alpha = 1)
        {
            poColor.Set(red, green, blue, alpha);
            poSprite.SwapColor(poColor);
        }

        public override SpriteID getName()
        {
           return Name;
        }

        public override void setName(SpriteID name)
        {
            this.Name = name;
        }
    }
}

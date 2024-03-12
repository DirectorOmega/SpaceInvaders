namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class BoxSprite : baseSprite
    {
        private SpriteID Name;
        private float sx, sy;
        private float angle;
        private bool hidden;
        private Azul.Color poColor;
        private Azul.SpriteBox poBox;
        private Azul.Rect poScreenRect;  
    
        public BoxSprite()
        {
            Name = SpriteID.Undef;
            hidden = false;
            poBox = new Azul.SpriteBox();
            sx = 1.0f;
            sy = 1.0f;
            poColor = new Azul.Color(255.0f, 0.0f, 0.0f);
            poScreenRect = new Azul.Rect();
            poBox.SwapColor(poColor);
        }
        
        ~BoxSprite()
        {
            poBox = null;
            poScreenRect = null;
            poColor = null;
        }   

        public void Set(SpriteID name,Azul.Rect screenRect)
        {
            Name = name;
            this.poScreenRect.Set(screenRect);
            poBox.Swap(screenRect,poColor);
        }

        public void Set(SpriteID name,Azul.Color color,Azul.Rect screenRect)
        {
            Name = name;
            poColor = color;
            this.poScreenRect.Set(screenRect);
            poBox.SwapScreenRect(poScreenRect);
            poBox.SwapColor(color);
        }

        public void hide()
        {
            hidden = true;
        }

        public void show()
        {
            hidden = false;
        }

        protected override void cClean()
        {
            Name = SpriteID.Undef;
            sx = 1;
            sy = 1;
            angle = 0;
            hidden = false;
        }

        public override void Render()
        {
            if (!hidden)
            {
                this.poBox.Render();
            }
        }

        public override void Update()
        { 
            poBox.x = this.getX();
            poBox.y = this.getY();
            poBox.Update();
        }
        public override SpriteID getName()
        {
            return Name;
        }
        public override void setName(SpriteID name)
        {
            this.Name = name;
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
            poBox.angle = this.angle;
        }

        public override void setScale(float sx, float sy)
        {
            this.sx = sx;
            this.sy = sy;
            poBox.sx = this.sx;
            poBox.sy = this.sy;
        }

        public override Azul.Rect getScreenRect()
        {
            return poScreenRect;
        }

        public override void setScreenRect(Azul.Rect screenRect)
        {
            poScreenRect.Set(screenRect);
            poBox.SwapScreenRect(screenRect);
        }


        public override void setColor(float red, float green, float blue, float alpha = 1)
        {
            poColor.Set(red, green, blue, alpha);
            poBox.SwapColor(poColor);
        }

      
    }
}

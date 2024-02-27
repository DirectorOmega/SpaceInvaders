using SpaceInvaders.FontSystem;
using System.Diagnostics;

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class FontSprite : baseSprite
    {
        public Glyph.Name GetGlyphName()
        {
            return this.glyphName;
        }

        override public SpriteID getName()
        {
            return this.Name;
        }

        public FontSprite()
            : base()
        {
            // Create a dummy sprite, it will get correctly linked in Set()

            this.pAzulSprite = new Azul.Sprite();
            this.pScreenRect = new Azul.Rect();
            this.pColor = new Azul.Color(1.0f, 1.0f, 1.0f);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;

        }

        ~FontSprite()
        {
#if (TRACK_DESTRUCTOR)   
            Debug.WriteLine("~GameSprite():{0} ", this.GetHashCode());
#endif
            this.pAzulSprite = null;
            this.pScreenRect = null;
            this.pColor = null;
            this.pMessage = null;
        }

        public void Set(FontName name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;

            this.x = xStart;
            this.y = yStart;

            this.name = name;
            this.glyphName = glyphName;

            // Force color to white
            Debug.Assert(this.pColor != null);
            this.pColor.Set(1.0f, 1.0f, 1.0f);
        }

        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.pColor != null);
            this.pColor.Set(red, green, blue, alpha);
        }

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;
        }

        override public void Update()
        {
            Debug.Assert(this.pAzulSprite != null);

        }

        override public void Render()
        {
            Debug.Assert(this.pAzulSprite != null);
            Debug.Assert(this.pColor != null);
            Debug.Assert(this.pScreenRect != null);
            Debug.Assert(this.pMessage != null);
            Debug.Assert(this.pMessage.Length > 0);

            float xTmp = this.x;
            float yTmp = this.y;

            float xEnd = this.x;

            for (int i = 0; i < this.pMessage.Length; i++)
            {
                int key = Convert.ToByte(pMessage[i]);

                Glyph pGlyph = GlyphManager.Find(this.glyphName, key);
                Debug.Assert(pGlyph != null);

                xTmp = xEnd + pGlyph.GetAzulSubRect().width / 2;
                this.pScreenRect.Set(xTmp, yTmp, pGlyph.GetAzulSubRect().width, pGlyph.GetAzulSubRect().height);

                pAzulSprite.Swap(pGlyph.GetAzulTexture(), pGlyph.GetAzulSubRect(), this.pScreenRect, this.pColor);

                pAzulSprite.Update();
                pAzulSprite.Render();

                // move the starting to the next character
                xEnd = pGlyph.GetAzulSubRect().width / 2 + xTmp;

            }
        }

        //public void Dump()
        //{

        //}

        protected override void cClean()
        {
            Name = SpriteID.Undef;
            sx = 1;
            sy = 1;
            angle = 0;
        
        }

        public override void setName(SpriteID name)
        {
         
        }

        public override Azul.Rect getScreenRect()
        {
            return this.pScreenRect;
        }

        public override void setScreenRect(Azul.Rect r)
        {
            this.pScreenRect.Set(r);
        }

        public override void setColor(float red, float green, float blue, float alpha = 1)
        {
            this.pColor.Set(red, green, blue, alpha);
        }

        public override float getAngle()
        {
            return this.angle;
        }

        public override void setAngle(float angle)
        {
            this.angle = angle;
        }

        public override float getSX()
        {
            return this.sx;
        }

        public override float getSY()
        {
            return this.sy;
        }

        public override void setScale(float sx, float sy)
        {
            this.sx = sx;
            this.sy = sy;
        }


        // ---------------------------------------------------------------------------------
        // Data
        // ---------------------------------------------------------------------------------

        public FontName name;
        public SpriteID Name;
        private Azul.Sprite pAzulSprite;
        private Azul.Rect pScreenRect;
        private Azul.Color pColor;   // this color is multplied by the texture

        private String pMessage;
        public Glyph.Name glyphName;
        public float sx, sy;
        public float angle;
       
        //public float x;
        //public float y;


    }
}

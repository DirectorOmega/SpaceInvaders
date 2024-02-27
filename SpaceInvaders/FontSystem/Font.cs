using SpaceInvaders.GraphicalObjects;
using System;
using System.Diagnostics;

namespace SpaceInvaders.FontSystem
{
    class Font : FLink
    {

        public Font()
                : base()
        {
            this.name = FontName.Uninitialized;
            this.pFontSprite = new FontSprite();
        }

        ~Font()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Font():{0} ", this.GetHashCode());
#endif
            this.name = FontName.Uninitialized;
            this.pFontSprite = null;
        }

        public void updateScore(int score)
        {
            Debug.Assert(this.pFontSprite != null);
            if(score < 10) { this.pFontSprite.UpdateMessage("000" + score); }
            else if(score < 100) { this.pFontSprite.UpdateMessage("00" + score); }
            else if(score < 1000) { this.pFontSprite.UpdateMessage("0" + score); }
            else { this.pFontSprite.UpdateMessage(""+score); }
        }

        public void updateCredit(int credits)
        {
            Debug.Assert(this.pFontSprite != null);
            if (credits < 10) { this.pFontSprite.UpdateMessage("0" + credits); }
            else { this.pFontSprite.UpdateMessage("" + credits); }
        }

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.pFontSprite != null);
            this.pFontSprite.UpdateMessage(pMessage);
        }

        public void Set(FontName name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            pFontSprite.Set(name, pMessage, glyphName, xStart, yStart);
        }

        override public void dClean()
        {
            this.name = FontName.Uninitialized;
            this.pFontSprite.Set(FontName.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void Render()
        {
            pFontSprite.Render();
        }

        public void Dump()
        {
        }


        // ----------------------------------------------------------------
        // Data 
        // ----------------------------------------------------------------
        public FontName name;
        public FontSprite pFontSprite;
        static private String pNullString = "null";
    }
}

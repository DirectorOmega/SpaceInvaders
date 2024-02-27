using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.FontSystem
{
    internal sealed class Font : FLink
    {

        public Font()
                : base()
        {
            name = FontName.Uninitialized;
            pFontSprite = new FontSprite();
        }

        ~Font()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Font():{0} ", this.GetHashCode());
#endif
            name = FontName.Uninitialized;
            pFontSprite = null;
        }

        public void updateScore(int score)
        {
            Debug.Assert(pFontSprite != null);
            if(score < 10) { pFontSprite.UpdateMessage("000" + score); }
            else if(score < 100) { pFontSprite.UpdateMessage("00" + score); }
            else if(score < 1000) { pFontSprite.UpdateMessage("0" + score); }
            else { pFontSprite.UpdateMessage(""+score); }
        }

        public void updateCredit(int credits)
        {
            Debug.Assert(pFontSprite != null);
            if (credits < 10) { pFontSprite.UpdateMessage("0" + credits); }
            else { pFontSprite.UpdateMessage("" + credits); }
        }

        public void UpdateMessage(string pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(pFontSprite != null);
            pFontSprite.UpdateMessage(pMessage);
        }

        public void Set(FontName name, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            pFontSprite.Set(name, pMessage, glyphName, xStart, yStart);
        }

        override public void dClean()
        {
            name = FontName.Uninitialized;
            pFontSprite.Set(FontName.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void Render() 
            => pFontSprite.Render();

        public void Dump()
        {
        }


        // ----------------------------------------------------------------
        // Data 
        // ----------------------------------------------------------------
        public FontName name;
        public FontSprite pFontSprite;
        static private string pNullString = "null";
    }
}

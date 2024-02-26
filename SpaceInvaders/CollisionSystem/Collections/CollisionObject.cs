using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.CollisionSystem
{ 
   class CollisionObject
    {
        private CollisionRect poColRect;
        //hint use a box sprite; I could force it but I don't want too just yet.
        baseSprite pColSprite;

        public CollisionObject(baseSprite pProxySprite)
        {
            Debug.Assert(pProxySprite != null);
            // Origin is in the UPPER RIGHT 

            Azul.Rect t = new Azul.Rect(pProxySprite.getX(), pProxySprite.getY(), pProxySprite.getScreenRect().width, pProxySprite.getScreenRect().height);

            this.poColRect = new CollisionRect(t);

            //this.poColRect = new CollisionRect(pProxySprite.getScreenRect());
            //this.poColRect = new CollisionRect(pProxySprite.get)
            Debug.Assert(this.poColRect != null);

            // Create the sprite
            this.pColSprite = BoxSpriteManager.Add(SpriteID.Box, this.poColRect);
            Debug.Assert(this.pColSprite != null);
            this.pColSprite.setColor(255.0f, 0.0f, 0.0f);
        }

        public baseSprite getColSprite()
        {
            return pColSprite;
        }

        public void setColRect(baseSprite t)
        {
            this.pColSprite.setScreenRect(t.getScreenRect());
            this.poColRect.Set(t.getScreenRect());
        }

        public CollisionRect getColRect()
        {
            return poColRect;
        }

        //I need to chage how this is called so it will only be called on add or remove of a object.
        public void Union(CollisionObject o)
        {
            this.poColRect.Union(o.poColRect);

            pColSprite.setScreenRect(poColRect);
        }
        
        
        public void UpdatePos(float x, float y)
        {
            this.poColRect.x = x;
            this.poColRect.y = y;

            this.pColSprite.setCoords(this.poColRect.x, this.poColRect.y);

            this.pColSprite.setScreenRect(this.poColRect);
            //will need to remove this when it's a proxy box sprite.
            this.pColSprite.Update();
        }

    }
}

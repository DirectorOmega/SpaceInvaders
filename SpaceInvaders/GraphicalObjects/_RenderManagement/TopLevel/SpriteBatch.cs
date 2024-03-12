using System.Diagnostics;

namespace SpaceInvaders.GraphicalObjects
{
    internal class SpriteBatch : SBLink
    {
        private SpriteBatchID Name;
        private SpriteBatchNodeManager poNodeMan;

        public SpriteBatch()
        {
            Name = SpriteBatchID.Undef;
            poNodeMan = new SpriteBatchNodeManager();
        }

        //horrible I know
        public void setUniversal(bool  flag)
        {
            poNodeMan.setUniversal(flag);
        }

        public void StorePlayerA()
        {
            poNodeMan.StorePlayerA();
        }

        public void SetPlayerA()
        {
          poNodeMan.SetPlayerA();
        }

        public void StorePlayerB()
        {
            poNodeMan.StorePlayerB();
        }

        public void SetPlayerB()
        {
            poNodeMan.SetPlayerB();
        }

        public void ClearStored()
        {
            poNodeMan.ClearStored();
        }

        public void Set(SpriteBatchID name)
        {
            Name = name;
            poNodeMan.Set(name, this);
        }

        public SpriteBatchID getName()
        {
            return Name;
        }

        public void Toggle()
        {
            Debug.Assert(null != poNodeMan);
            poNodeMan.Toggle();
        }

        public override void dClean()
        {
            Name = SpriteBatchID.Undef;
        }

        public void Draw()
        {
            poNodeMan.Draw();
        }

        internal void Attach(baseSprite baseSprite)
        {
            poNodeMan.Attach(baseSprite);
        }
    }
}
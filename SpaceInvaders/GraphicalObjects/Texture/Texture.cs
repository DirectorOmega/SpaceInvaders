namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class Texture : TexLink
    {
        private TextureID Name;
        private Azul.Texture poTex;

        //see if I can get rid of this new, because I will always pass in a texture from the heap in the set parameter
        public Texture()
        {
            Name = TextureID.Undef;
            poTex = new Azul.Texture();
        }

        public override void dClean()
        {
            Name = TextureID.Undef;
            poTex = null;
        }

        public TextureID GetName() => Name;
        public void SetName(TextureID name) => this.Name = name;
        public Azul.Texture GetTex() => poTex;

        public void Set(TextureID name, Azul.Texture Tex)
        {
            Name = name;
            poTex = Tex;
        }
    }
}

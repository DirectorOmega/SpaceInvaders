using System.Diagnostics;

//TODO: cleanup private, add printing, cleanup destructors.
//Cleanup usings in all classes I think I have been pretty good about cleaning them up but I want to double check.
//Technical debt almost entirely elimintaed.

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the instance
            //SpaceInvaders game = new SpaceInvaders();
            Azul.Game game = new mySpaceInvaders();
            Debug.Assert(game != null);

            // Start the game
            game.Run();
        }
    }
}

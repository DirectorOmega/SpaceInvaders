using System.Diagnostics;

namespace SpaceInvaders.GameState
{
    abstract class GameState
    {
        public abstract void Handle();

        public abstract void Init();

        public virtual void Reset()
        {
            Debug.Assert(false);
        }

        public virtual void ResumeCommands()
        {
            Debug.Assert(false);
        }

        public virtual void prepNextWave()
        {
            Debug.Assert(false);
        }

        public abstract void Update(float time);

        public abstract void Render();

        public virtual void Resume()
        {
            Debug.Assert(false);
        }

        public virtual float getWaveMult()
        {
            Debug.Assert(false);
            return 0.0f;
        }

        public virtual void incrementScore(int points)
        {
            Debug.Assert(false);
        }

        public virtual void loseLife()
        {
            Debug.Assert(false);
        }

        public virtual void addLife()
        {
            Debug.Assert(false);
        }

        public virtual void setLives(int numLives)
        {
            Debug.Assert(false);
        }

        public virtual int getLifeCount()
        {
            Debug.Assert(false);
            return 0;
        }
    }
}

using SpaceInvaders.Commands;
using SpaceInvaders.Manager;

namespace SpaceInvaders.Time
{
    internal class TimeEvent : TLink
    {
        private float deltaTime;
        private float triggerTime;
        private TimeEventID Name;
        Command CMD;

        public TimeEvent()
        {
            triggerTime = 0.0f;
            Name = TimeEventID.undef;
            CMD = NullCMD.getInstance();
        }

        public void process()
        {
            CMD.execute(deltaTime);
        }

        public TimeEventID getName()
        {
            return Name;
        }

        public void setName(TimeEventID name)
        {
            this.Name = name;
        }

        public void set(TimeEventID name, float TTime)
        {
            Name = name;
            triggerTime = TTime;
        }
        public void set(TimeEventID name,float DTime,float TTime)
        {
            deltaTime = DTime;
            triggerTime = TTime;
            Name = name;
        }

        public void set(TimeEventID name,float DTime, float TTime, Command cmd)
        {
            Name = name;
            deltaTime = DTime;
            triggerTime = TTime;
            CMD = cmd;
        }

        public void set(Command cmd)
        {
            CMD = cmd;
        }

        public float getTriggerTime()
        {
            return triggerTime;
        }
        //Bad smell Not sure how I like the greaterThan system, I think I want to do the pass up value for comparison.
        //I always want the earliest insertion so I am using greaterthan or equals to.
        public override bool greaterThan(DLink that)
        {
            TimeEvent TE = (TimeEvent) that;
            return this.triggerTime >= TE.triggerTime;
        }

        public override void dClean()
        {
         //   deltaTime = 0f;
            triggerTime = 0f;
            Name = TimeEventID.undef;
        }
    }
}
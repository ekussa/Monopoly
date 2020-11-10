using System;

namespace Monopoly
{
    public abstract class Square
    {
        public string Name { get; }
        public Player Owner { get; set; }

        public EventHandler<Player> OnStop { get; set; }
        public EventHandler<Player> OnPass { get; set; }
        
        protected Square(string name)
        {
            Name = name;
        }
    }
}
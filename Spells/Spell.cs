using System;

namespace OurCoolGame.Spells
{
    abstract class Spell : IMagic
    {
        public virtual int ManaCost { get; protected set; }
        public virtual bool Pronouncing { get; protected set; }
        public virtual bool Gesturing { get; protected set; }

        public virtual void MagicEffect(Wizard origin, Wizard target = null, int magicPower = 0)
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
            return;
        }

        public virtual void MagicEffect(Wizard origin, Wizard target)
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
            return;
        }

        public virtual void MagicEffect(Wizard origin, int magicPower)
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
            return;
        }
    }
}
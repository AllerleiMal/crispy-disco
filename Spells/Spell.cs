using System;

namespace OurCoolGame.Spells
{
    public abstract class Spell : IMagic
    {
        public virtual int ManaCost { get; protected set; }
        public virtual bool Pronouncing { get; protected set; }
        public virtual bool Gesturing { get; protected set; }

        public virtual void MagicEffect(Wizard origin, Wizard target, int magicPower)  //template for spell that has both target and magic power as parameters
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
        }

        public virtual void MagicEffect(Wizard origin, Wizard target) // template for spell that have only target as parameter
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
        }

        public virtual void MagicEffect(Wizard origin, int magicPower) //template for spell that have only magicPower as parameter
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
        }

        public virtual void MagicEffect(Wizard origin) //template for spell without any parameters
        {
            Console.WriteLine("Wrong method parameters (Spell MagicEffect)");
        }
    }
}
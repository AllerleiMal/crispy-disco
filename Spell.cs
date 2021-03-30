using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurCoolGame
{
    abstract class Spell : IMagic
    {
        public virtual int ManaCost { get; protected set; }
        public virtual bool Pronouncing { get; protected set; }
        public virtual bool Gesturing { get; protected set; }
        public virtual void MagicEffect(Character character, int magicPower)
        {
            throw new Exception("Wrong method parameters (Spell MagicEffect)");
        }
        public virtual void MagicEffect(Character character)
        {
            throw new Exception("Wrong method parameters (Spell MagicEffect)");
        }
        public virtual void MagicEffect(int magicPower)
        {
            throw new Exception("Wrong method parameters (Spell MagicEffect)");
        }
        public virtual void MagicEffect()
        {
            throw new Exception("Wrong method parameters (Spell MagicEffect)");
        }
    }
}

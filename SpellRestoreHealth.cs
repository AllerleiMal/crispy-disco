using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurCoolGame
{
    class SpellRestoreHealth : Spell
    {
        public override int ManaCost { get; protected set; } = 0;
        public override bool Gesturing { get; protected set; } = false;
        public override bool Pronouncing { get; protected set; } = true;
        override public void MagicEffect(Character character, int magicPower)
        {
            ManaCost = 2 * magicPower;
            if (character.CharacterState == State.Dead)
            {
                throw new Exception("Can't heal dead character (SpellRestoreHealth)");
                return;
            }
            if (character.CurrentHealthPoints + magicPower < character.MaxHealthPoints)
                character.CurrentHealthPoints += magicPower;
            else
                character.CurrentHealthPoints = character.MaxHealthPoints;
            return;    
        }
    }
}

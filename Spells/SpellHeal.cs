using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellHeal : Spell
    {
        public override int ManaCost { get; protected set; } = 2;
        public override bool Gesturing { get; protected set; } = false;
        public override bool Pronouncing { get; protected set; } = false;

        public override string ToString()
        {
            return "Heal";
        }
        public override void MagicEffect(Wizard origin, Character target, int magicPower)
        {
            if(origin.CurMana < ManaCost * magicPower)
            {
                Console.WriteLine("Not enough mana to cast heal spell");
                return;
            }
            if (target.CharacterState == State.Dead)
            {
                Console.WriteLine("Can't heal dead character (SpellRestoreHealth)");
                return;
            }
            origin.CurMana -= 2 * magicPower;
            if (target.CurrentHealthPoints + magicPower < target.MaxHealthPoints)
            {
                target.CurrentHealthPoints += magicPower;
            }
            else
            {
                target.CurrentHealthPoints = target.MaxHealthPoints;
            }
        }
    }
}
using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellArmor : Spell
    {
        public override int ManaCost { get; protected set; } = 50;
        public override bool Gesturing { get; protected set; } = true;
        public override bool Pronouncing { get; protected set; } = false;

        public override void MagicEffect(Wizard origin, Character target, int magicPower) // magicPower is equal to count of turns armor lasts
        {
            if (origin.CurMana < ManaCost * magicPower)
            {
                Console.WriteLine("Not enough mana to cast armor spell");
                return;
            }
            if (target.CharacterState != Enums.State.Dead)
            {
                Console.WriteLine("Can't use armor on dead character");
                return;
            }
            origin.CurMana -= ManaCost;
            /*
             * armor realization
             */
        }
    }
}

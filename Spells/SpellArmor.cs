using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellArmor : Spell
    {
        public override int ManaCost { get; protected set; } = 150;
        public override bool Gesturing { get; protected set; } = true;
        public override bool Pronouncing { get; protected set; } = false;

        public override string ToString()
        {
            return "Armor";
        }

        public override void MagicEffect(Wizard origin, Wizard target, int magicPower)
        {
            if (origin.CurrentMana < ManaCost * magicPower)
            {
                Console.WriteLine("Not enough mana to cast armor spell");
                return;
            }

            if (target.CharacterState != State.Dead)
            {
                Console.WriteLine("Can't use armor on dead character");
                return;
            }

            origin.CurrentMana -= ManaCost;
            target.CharacterState = State.Invulnerable;
        }
    }
}
using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellCure : Spell
    {
        public override int ManaCost { get; protected set; } = 20;
        public override bool Gesturing { get; protected set; } = true;
        public override bool Pronouncing { get; protected set; } = true;

        public override string ToString()
        {
            return "Cure";
        }

        public override void MagicEffect(Wizard origin, Wizard target)
        {
            if (origin.CurrentMana < ManaCost)
            {
                Console.WriteLine("You don't have enough mana to cure character");
            }

            if (target.CharacterState != State.Sick)
            {
                Console.WriteLine("Your character must be sick");
            }

            if (target.CharacterState == State.Dead)
            {
                Console.WriteLine("You can't apply it to the dead character");
            }

            target.CharacterState = target.CurrentHealthPoints <= target.MaxHealthPoints / 10
                ? State.Weakened
                : State.Healthy;

            origin.CurrentMana -= ManaCost;
        }
    }
}
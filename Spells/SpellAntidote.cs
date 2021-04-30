using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellAntidote : Spell
    {
        public override int ManaCost { get; protected set; } = 30;
        public override bool Gesturing { get; protected set; } = false;
        public override bool Pronouncing { get; protected set; } = false;

        public override string ToString()
        {
            return "Antidote";
        }

        public override void MagicEffect(Wizard origin, Wizard target)
        {
            if (origin.CurrentMana < ManaCost)
            {
                Console.WriteLine("You don't have enough mana to give this character antidote");
            }

            if (target.CharacterState != State.Poisoned)
            {
                Console.WriteLine("Character must be poisoned");
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
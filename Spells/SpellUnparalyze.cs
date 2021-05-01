using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellUnparalyze : Spell
    {
        public override int ManaCost { get; protected set; } = 85;
        public override bool Gesturing { get; protected set; } = false;
        public override bool Pronouncing { get; protected set; } = true;

        public override string ToString()
        {
            return "Unparalyze";
        }

        public override void MagicEffect(Wizard origin, Wizard target)
        {
            if (origin.CurrentMana < ManaCost)
            {
                Console.WriteLine("Not enough mana to cast unparalyze spell");
                return;
            }

            if (target.CharacterState != Enums.State.Paralyzed)
            {
                Console.WriteLine("Target character must be paralyzed");
                return;
            }

            origin.CurrentMana -= ManaCost;
            target.CharacterState = target.CurrentHealthPoints <= target.MaxHealthPoints / 10
                ? State.Weakened
                : State.Healthy;
        }
    }
}
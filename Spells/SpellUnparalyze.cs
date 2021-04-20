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
        public override void MagicEffect(Wizard origin, Character target)
        {
            if (origin.CurMana < ManaCost)
            {
                Console.WriteLine("Not enough mana to cast unparalyze spell");
                return;
            }
            if (target.CharacterState != Enums.State.Paralyzed)
            {
                Console.WriteLine("Target character must be paralyzed");
                return;
            }
            origin.CurMana -= ManaCost;
            target.CurrentHealthPoints = 1;
            target.CharacterState = State.Weakened;
        }
    }
}

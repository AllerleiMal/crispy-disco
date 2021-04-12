using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellRevival : Spell
    {
        public override int ManaCost { get; protected set; } = 150;
        public override bool Gesturing { get; protected set; } = true;
        public override bool Pronouncing { get; protected set; } = true;

        public override void MagicEffect(Wizard origin, Wizard target)
        {
            if (origin.CurMana < ManaCost)
            {
                Console.WriteLine("Not enough mana to cast revival spell");
                return;
            }
            if(target.CharacterState != Enums.State.Dead)
            {
                Console.WriteLine("Target character must be dead");
                return;
            }
            origin.CurMana -= ManaCost;
            target.CurrentHealthPoints = 1;
            target.CharacterState = State.Weakened;
        }
    }
}

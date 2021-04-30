using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Spells
{
    class SpellFireball : Spell
    {
        public override int ManaCost { get; protected set; } = 1;
        public override bool Gesturing { get; protected set; } = true;
        public override bool Pronouncing { get; protected set; } = true;
        public override string ToString()
        {
            return "Fireball";
        }
        public override void MagicEffect(Wizard origin, Character target, int magicPower)
        {
            if (origin.CurMana < ManaCost * magicPower)
            {
                Console.WriteLine("Not enough mana to cast fireball");
                return;
            }
            if (target.CharacterState == State.Dead)
            {
                Console.WriteLine("You dont need to cast that on dead character (SpellFireball)");
                return;
            }
            origin.CurMana -= ManaCost * magicPower;
            target.CurrentHealthPoints -= magicPower;
        }
    }
}

﻿using OurCoolGame.Enums;
using System.Collections.Generic;
using OurCoolGame.Spells;
using System;

namespace OurCoolGame
{
    public class Wizard : Character
    {
        public int CurMana { get; set; }
        public int MaxMana { get; private set; }
        public List<Spell> _learnedSpells;
        public Wizard(string name, Race characterRace, Gender characterGender, int age) : base(name, characterRace, characterGender, age)
        {
            switch (characterRace)
            {
                case Race.Elf:
                {
                    MaxMana = 400;
                    break;
                }
                case Race.Gnome:
                {
                    MaxMana = 150;
                    break;
                }
                case Race.Goblin:
                {
                    MaxMana = 200;
                    break;
                }
                case Race.Human:
                {
                    MaxMana = 300;
                    break;
                }
                case Race.Orc:
                {
                    MaxMana = 250;
                    break;
                }
            }
            CurMana = MaxMana;
            _learnedSpells = new List<Spell>();
        }
        public override string ToString()
        {
            var characterInfo = "";
            characterInfo += "ID: " + ID + ", name: " + Name + ", race: " + CharacterRace + ", age: " + Age +
                             ", state: " + CharacterState + ", HP: " + CurrentHealthPoints + ", maximum HP: " +
                             MaxHealthPoints + ", XP: " + ExperiencePoints + ", MP: " + CurMana + ", maximum MP: " + MaxMana;
            return characterInfo;
        }
        private bool SpellLearnedCheck(Spell spell)
        {
            bool learned = _learnedSpells.FindIndex(target => spell == target) != -1;
            if(!learned)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("The character doesn't know this spell");
                Console.ResetColor();
            }
            return learned;
        }

        public void LearnSpell(Spell spell)
        {
            if (_learnedSpells.FindIndex(target => spell == target) == -1)
            {
                _learnedSpells.Add(spell);
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("The character already knows this spell");
                Console.ResetColor();
            }
        }
        public void ForgetSpell(Spell spell)
        {
            if (SpellLearnedCheck(spell))
            {
                _learnedSpells.Remove(spell);
            }
        }
        public void CastSpell(Spell spell, Wizard target, int magicPower)
        {
            if(SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this, target, magicPower);
            }
        }
        public void CastSpell(Spell spell, Wizard target)
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this, target);
            }
        }
        public void CastSpell(Spell spell, int magicPower)
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this, magicPower);
            }
        }
        public void CastSpell(Spell spell)
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this);
            }
        }
    }
}

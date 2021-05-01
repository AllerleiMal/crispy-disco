using OurCoolGame.Enums;
using System.Collections.Generic;
using OurCoolGame.Spells;
using System;

namespace OurCoolGame
{
    public class Wizard : Character
    {
        private int _currentMana;

        public int CurrentMana
        {
            get => _currentMana;
            set
            {
                if (value < 0)
                {
                    _currentMana = 0;
                    return;
                }

                if (value > MaxMana)
                {
                    _currentMana = MaxMana;
                    return;
                }

                _currentMana = value;
            }
        }

        public int MaxMana { get; private set; }
        public List<Spell> _learnedSpells;

        public Wizard(string name, Race characterRace, Gender characterGender, int age) : base(name, characterRace,
            characterGender, age)
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

            CurrentMana = MaxMana;
            _learnedSpells = new List<Spell>();
        }

        public override string ToString()
        {
            var characterInfo = "";
            characterInfo += base.ToString() + ", MP: " + CurrentMana + ", maximum MP: " + MaxMana;
            return characterInfo;
        }

        private bool SpellLearnedCheck(Spell spell)
        {
            var learned = _learnedSpells.FindIndex(target => spell.ToString() == target.ToString()) != -1;
            if (!learned)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The character doesn't know this spell");
                Console.ResetColor();
            }

            return learned;
        }

        public void LearnSpell(Spell spell)
        {
            if (_learnedSpells.FindIndex(target => spell.ToString() == target.ToString()) == -1)
            {
                Console.WriteLine("{0} learned spell {1}", Name, spell);
                _learnedSpells.Add(spell);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
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

        public void CastSpell(Spell spell, Wizard target,
            int magicPower)
        {
            if (SpellLearnedCheck(spell))
            {
                if ((spell.Gesturing == CanMove || CanMove) && (spell.Pronouncing == CanTalk || CanTalk))
                {
                    spell.MagicEffect(this, target, magicPower);
                    Console.WriteLine("Spell {0} was used by {1} on {2}", spell, Name, target.Name);
                    ExperiencePoints += 150;
                }
            }
        }

        public void CastSpell(Spell spell, Wizard target)
        {
            if (SpellLearnedCheck(spell))
            {
                if ((spell.Gesturing == CanMove || CanMove) && (spell.Pronouncing == CanTalk || CanTalk))
                {
                    spell.MagicEffect(this, target);
                    Console.WriteLine("Spell {0} was used by {1} on {2}", spell, Name, target.Name);
                    ExperiencePoints += 150;
                }
            }
        }

        public void CastSpell(Spell spell, int magicPower)
        {
            if (SpellLearnedCheck(spell))
            {
                if ((spell.Gesturing == CanMove || CanMove) && (spell.Pronouncing == CanTalk || CanTalk))
                {
                    spell.MagicEffect(this, magicPower);
                    Console.WriteLine("Spell {0} was used by {1}", spell, Name);
                    ExperiencePoints += 150;
                }
            }
        }

        public void CastSpell(Spell spell)
        {
            if (SpellLearnedCheck(spell))
            {
                if ((spell.Gesturing == CanMove || CanMove) && (spell.Pronouncing == CanTalk || CanTalk))
                {
                    spell.MagicEffect(this);
                    Console.WriteLine("Spell {0} was used by {1}", spell, Name);
                    ExperiencePoints += 150;
                }
            }
        }

        public void ShowLearnedSpells()
        {
            for (var i = 0; i < _learnedSpells.Count; ++i)
            {
                Console.WriteLine("({0}) {1}", i + 1, _learnedSpells[i]);
            }
        }
    }
}
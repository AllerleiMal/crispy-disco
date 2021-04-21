using OurCoolGame.Enums;
using System.Collections.Generic;
using OurCoolGame.Spells;
using System;

namespace OurCoolGame
{
    public class Wizard : Character
    {
        private int _curMana;

        public int CurMana
        {
            get => _curMana;
            set
            {
                if (value < 0)
                {
                    _curMana = 0;
                    return;
                }

                if (value > MaxMana)
                {
                    _curMana = MaxMana;
                    return;
                }

                _curMana = value;
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

            CurMana = MaxMana;
            _learnedSpells = new List<Spell>();
        }

        public override string ToString()
        {
            var characterInfo = "";
            characterInfo += base.ToString() + ", MP: " + CurMana + ", maximum MP: " + MaxMana;
            return characterInfo;
        }

        private bool SpellLearnedCheck(Spell spell)
        {
            var learned = _learnedSpells.FindIndex(target => spell == target) != -1;
            if (!learned)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("The character doesn't know this spell");
                Console.ResetColor();
            }

            return learned;
        }

        public void LowManaOrHpMessage()
        {
            if ((double) CurrentHealthPoints / MaxHealthPoints * 100 - 10 < 0)
            {
                Console.WriteLine("Your HP is low {0}/{1}", CurrentHealthPoints, MaxHealthPoints);
            }

            if ((double) CurMana / MaxMana * 100 - 10 < 0)
            {
                Console.WriteLine("Your mana is low {0}/{1}", CurMana, MaxMana);
            }
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

        public void
            CastSpell(Spell spell, Character target,
                int magicPower) //why target only wizard? it also can be just character
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this, target, magicPower);
                ExperiencePoints += 150;
            }
        }

        public void CastSpell(Spell spell, Character target)
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this, target);
                ExperiencePoints += 150;
            }
        }

        public void CastSpell(Spell spell, int magicPower)
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this, magicPower);
                ExperiencePoints += 150;
            }
        }

        public void CastSpell(Spell spell)
        {
            if (SpellLearnedCheck(spell))
            {
                spell.MagicEffect(this);
                ExperiencePoints += 150;
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
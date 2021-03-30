using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace OurCoolGame
{
    public class Wizard : Character
    {
        public int curMana { get; set; }
        public int maxMana { get; set; }
        public Wizard(string name, Race characterRace, Gender characterGender, int age) : base(name, characterRace, characterGender, age)
        {
            switch (characterRace)
            {
                case Race.Elf:
                {
                    maxMana = 400;
                    break;
                }
                case Race.Gnome:
                {
                    maxMana = 150;
                    break;
                }
                case Race.Goblin:
                {
                    maxMana = 200;
                    break;
                }
                case Race.Human:
                {
                    maxMana = 300;
                    break;
                }
                case Race.Orc:
                {
                    maxMana = 250;
                    break;
                }
            }
            
        }

        private int ReadSpell(int CurMana)
        {
            CurMana = curMana;
            if (CurMana >= 0)
            {
                if (CurMana < ManaCost)
                {
                    throw new Exception("You need more mana to do this spell!");
                }
                else
                {
                    CurMana -= ManaCost;
                }
            }

            return CurMana;
        }
    }
}
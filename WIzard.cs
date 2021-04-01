using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace OurCoolGame
{
    public class Wizard : Character
    {
        public int CurMana { get; private set; }
        public int MaxMana { get; private set; }
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
    }
}

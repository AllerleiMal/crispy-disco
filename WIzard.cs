using System;
using OurCoolGame.Enums;
using OurCoolGame.Spells;

namespace OurCoolGame
{
    public class Wizard : Character
    {
        public int CurMana { get; set; }
        public int MaxMana { get; private set; }
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
        }
        
        // public void CastSpell<T>(Wizard origin, Wizard target, int magicPower)
        // {
        //     if (!typeof(T).IsSubclassOf(typeof(Spell)))
        //     {
        //         //here must be output message or exception throwing
        //         return;
        //     }
        //
        //     T spell = new T();
        // }
        
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
    }
}

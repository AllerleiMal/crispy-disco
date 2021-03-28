using System;

namespace OurCoolGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var myFirstCharacter = new Character("Snor", Race.Human, Gender.Undefined, 32);
            Console.WriteLine(myFirstCharacter.ToString());
            var mySecondCharacter = new Character("Amur", Race.Orc, Gender.Male, 12);
            Console.WriteLine(mySecondCharacter.ToString());
        }
    }
}

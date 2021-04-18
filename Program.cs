using System;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;
using OurCoolGame.Spells;

namespace OurCoolGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var myFirstCharacter = new Wizard("Snor", Race.Human, Gender.Undefined, 32);
            var mySecondCharacter = new Wizard("Amur", Race.Orc, Gender.Male, 12);
            /*LivingWater water = new LivingWater(BottleSize.Medium);  // ТУТ КАКОЙ-ТО ТЕСТ ИНВЕНТАРЯ
            myFirstCharacter.PickUpArtefact(water);
            Console.WriteLine(myFirstCharacter._inventory[0]);
            Console.WriteLine(typeof(LivingWater));
            DeadWater deadWater = new DeadWater(BottleSize.Big);
            DeadWater deadWater1 = new DeadWater(BottleSize.Small);
            myFirstCharacter.PickUpArtefact(deadWater1);
            myFirstCharacter.PickUpArtefact(deadWater);
            myFirstCharacter.ThrowAwayArtefact(deadWater);
            Console.WriteLine(myFirstCharacter._inventory.Find(artefact => artefact == deadWater1).ArtefactPower);
            Console.WriteLine(myFirstCharacter.CurrentHealthPoints);
            mySecondCharacter.CurrentHealthPoints = mySecondCharacter.CurrentHealthPoints - 100;
            myFirstCharacter.UseArtefact(water, mySecondCharacter);
            Console.WriteLine(myFirstCharacter.CurrentHealthPoints);*/
            //  ТУТ ТЕСТ СПЕЛЛОВ
            mySecondCharacter.CurrentHealthPoints = 100;
            Console.WriteLine(myFirstCharacter.ToString());
            Console.WriteLine(mySecondCharacter.ToString());

            myFirstCharacter.LearnSpell(new SpellHeal());
            
            myFirstCharacter.CastSpell(myFirstCharacter._learnedSpells[0], mySecondCharacter, 100);
            Console.WriteLine("\n" + myFirstCharacter.ToString());
            Console.WriteLine(mySecondCharacter.ToString());

            myFirstCharacter.CastSpell(myFirstCharacter._learnedSpells[0], mySecondCharacter, 100);
            myFirstCharacter.CastSpell(myFirstCharacter._learnedSpells[0], mySecondCharacter);
            myFirstCharacter.CastSpell(myFirstCharacter._learnedSpells[0], 100);
            SpellAntidote antidote = new SpellAntidote();
            myFirstCharacter.CastSpell(antidote, mySecondCharacter);

        }
    }
}
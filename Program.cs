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
            // var myFirstCharacter = new Wizard("Snor", Race.Human, Gender.Undefined, 32);
            // // Console.WriteLine(myFirstCharacter.ToString());
            // var mySecondCharacter = new Wizard("Amur", Race.Orc, Gender.Male, 12);
            // // Console.WriteLine(mySecondCharacter.ToString());
            // LivingWater water = new LivingWater(BottleSize.Medium);
            // myFirstCharacter.PickUpArtefact(water);
            // Console.WriteLine(myFirstCharacter._inventory[0]);
            // Console.WriteLine(typeof(LivingWater));
            // DeadWater deadWater = new DeadWater(BottleSize.Big);
            // DeadWater deadWater1 = new DeadWater(BottleSize.Small);
            // myFirstCharacter.PickUpArtefact(deadWater1);
            // myFirstCharacter.PickUpArtefact(deadWater);
            // myFirstCharacter.ThrowAwayArtefact(deadWater);
            // Console.WriteLine(myFirstCharacter._inventory.Find(artefact => artefact == deadWater1).ArtefactPower);
            // Console.WriteLine(myFirstCharacter.CurrentHealthPoints);
            // mySecondCharacter.CurrentHealthPoints = mySecondCharacter.CurrentHealthPoints - 100;
            // myFirstCharacter.UseArtefact(water, mySecondCharacter);
            // Console.WriteLine(myFirstCharacter.CurrentHealthPoints);
            var gameLogic = new GameLogic();
            gameLogic.GameStart();
            Wizard wizard = null;
            gameLogic.CreateCharacter(ref wizard);
            gameLogic.GenerateLevel();
            //add ++_difficultyLevel after ending of each level
            while (true)
            {
                if (GameLogic.MoveCounter == 0)
                {
                    gameLogic.GenerateLevel();
                }

                if (gameLogic.FinalLevelComplete())
                {
                    break;
                }
            }

            Console.WriteLine("Congratulations! You really did it! Thanks for playing. Hope you like it. Bis bald :)");
        }
    }
}
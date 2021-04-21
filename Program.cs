using System;
using System.Threading;
using OurCoolGame.Enums;

namespace OurCoolGame
{
    class Program
    {
        static void Main(string[] args)
        {
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
using System;
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
            gameLogic.CreateCharacter(wizard);
            gameLogic.GenerateLevel();
            while (true)
            {
                if (GameLogic.MoveCounter == 0 || wizard.CharacterState == State.Dead)
                {
                    gameLogic.GenerateLevel();
                }
                
                gameLogic.InputProcessing();
                
                if (gameLogic.FinalLevelComplete())
                {
                    break;
                }
            }

            Console.WriteLine("Congratulations! You really did it! Thanks for playing. Hope you like it. Bis bald :)");
        }
    }
}
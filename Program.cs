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
            wizard = gameLogic.CreateCharacter(wizard);
            gameLogic.GenerateLevel();
            while (true)
            {
                if (GameLogic.MoveCounter == 0 || wizard.CharacterState == State.Dead)
                {
                    gameLogic.GenerateLevel();
                }

                if (gameLogic.FinalLevelComplete())
                {
                    break;
                }

                int deadEnemies = 0;
                foreach (var enemy in gameLogic._enemy)
                {
                    if (enemy.CharacterState == State.Dead)
                    {
                        ++deadEnemies;
                    }
                }

                if (deadEnemies == gameLogic._enemy.Count)
                {
                    ++gameLogic._difficultyLevel;
                }
                gameLogic.InputProcessing();

            }

            Console.WriteLine("Congratulations! You really did it! Thanks for playing. Hope you like it. Bis bald :)");
        }
    }
}
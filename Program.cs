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
                    gameLogic._enemy.Clear();
                    gameLogic.GenerateLevel();
                }

                if (gameLogic.FinalLevelComplete())
                {
                    break;
                }
                
                gameLogic.InputProcessing();
                
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
                    GameLogic.MoveCounter = 0;
                    gameLogic._enemy.Clear();
                }
            }

            Console.WriteLine("Congratulations! You really did it! Thanks for playing. Hope you like it. Bis bald :)");
        }
    }
}
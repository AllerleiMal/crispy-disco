using System;
using OurCoolGame.Enums;

namespace OurCoolGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameLogic = new GameLogic();                //create GameLogic object
            gameLogic.GameStart();                          //start game
            Wizard wizard = null;                           //create main character object, initialize with null  
            wizard = gameLogic.CreateCharacter(wizard);     //initialize main character with gameLogic method (player will input all necessary data)
            gameLogic.GenerateLevel();                      //generate level. this will start with educational level
            while (true)                                    //game cycle
            {
                if (GameLogic.MoveCounter == 0 || wizard.CharacterState == State.Dead)  //generate level if move counter equal to 0(means that new level just starter) or main character died(then player will play same level again, without full game restart)
                {
                    gameLogic.GenerateLevel();              //this will print some messsages, let you select new artefacts and spells, generate new enemies
                }

                if (gameLogic.FinalLevelComplete())         //game cycle breaks when final level is complited
                {
                    break;
                }
                
                gameLogic.InputProcessing();                //interaction with player. this will also cause gameLogic.EnemyMove method
                
                int deadEnemies = 0;                        //dead enemies counter
                foreach (var enemy in gameLogic._enemy)     //go through every enemy
                {
                    if (enemy.CharacterState == State.Dead) //if enemy is dead
                    {
                        ++deadEnemies;                      //then increase dead enemies count
                    }
                }
                
                if (deadEnemies == gameLogic._enemy.Count)  //if dead enemies count equal to summary count of enemies in level
                {
                    ++gameLogic._difficultyLevel;           //then increase difficulti level(basically go to next level) 
                    GameLogic.MoveCounter = 0;              //set MoveCounter to 0 so new level will be generated in next cycle iteration
                    gameLogic._enemy.Clear();               //clear enemy list
                }
            }

            Console.WriteLine("Congratulations! You really did it! Thanks for playing. Hope you like it. Bis bald :)");
        }
    }
}
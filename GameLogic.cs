using System;
using System.Collections.Generic;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;

namespace OurCoolGame
{
    public class GameLogic
    {
        public static int MoveCounter { get; set; } = 0;
        private Wizard _mainPlayer;
        private int _difficultyLevel;
        // private List<Artefact> _allArtefacts; 

        public GameLogic()
        {
            _difficultyLevel = 0;
            _mainPlayer = null;
            _enemy = new List<Wizard>();
            _teammates = new List<Wizard>();
        }

        private List<Wizard> _enemy;
        private List<Wizard> _teammates;

        public void GameStart()
        {
            Console.WriteLine(
                "Welcome!\nIt is our mini version of console RPG game.\nAll the characters are fictional, and the coincidences are random.\nThe whole story will develop in a magical medieval fantasy world(do not be surprised by talking goblins and orcs).\nYour task is to pass all the tests and overcome the difficulties on the way to such a cherished goal - to learn magic from the great Merlin.\nP.S. follow the instructions that will be given later, otherwise you risk hearing a lot of bad words in your direction. You can enter \"!help\" to get info with all valid commands.\n(Press any key to continue)\n");
            Console.ReadKey();
        }

        public void CreateCharacter(ref Wizard wizard)
        {
            Console.WriteLine("Now is the time to create the character and choose the subclass");
            string name;
            while (true)
            {
                Console.WriteLine("Enter the name of your character(name couldn't be empty):");
                name = Console.ReadLine();
                if (name != "")
                {
                    break;
                }

                Console.WriteLine("As I already said name couldn't be empty you stupid piece of shit");
            }

            int age = 0;
            while (true)
            {
                Console.WriteLine("Enter the age of your character:");
                //here we try to parse string with age, and if it is not possible, ask for input one more time
                if (int.TryParse(Console.ReadLine(), out age))
                {
                    break;
                }

                Console.WriteLine(
                    "It is probably a miss click or you don't even know, that age contains only numbers you stupid piece of shit");
            }

            var race = Race.Elf;
            while (true)
            {
                Console.WriteLine("Which race would you choose:\nHuman(1)\nGnome(2)\nElf(3)\nOrc(4)\nGoblin(5)");
                //here we try to parse string with choice number, and if it is not possible or number is wrong, ask for input one more time                int choice; 
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice) && choice is >= 1 and <= 5)
                {
                    switch (choice)
                    {
                        case 1:
                        {
                            race = Race.Human;
                            break;
                        }
                        case 2:
                        {
                            race = Race.Gnome;
                            break;
                        }
                        case 3:
                        {
                            race = Race.Elf;
                            break;
                        }
                        case 4:
                        {
                            race = Race.Orc;
                            break;
                        }
                        case 5:
                        {
                            race = Race.Goblin;
                            break;
                        }
                    }

                    break;
                }

                Console.WriteLine("You need numbers from 1 to 5 you stupid piece of shit");
            }

            var gender = Gender.Undefined;
            while (true)
            {
                Console.WriteLine("Which gender would you choose:\nMale(1)\nFemale(2)\nUndefined(3)");
                //here we try to parse string with choice number, and if it is not possible or number is wrong, ask for input one more time
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice) && choice is >= 1 and <= 3)
                {
                    switch (choice)
                    {
                        case 1:
                        {
                            gender = Gender.Male;
                            break;
                        }
                        case 2:
                        {
                            gender = Gender.Female;
                            break;
                        }
                        case 3:
                        {
                            gender = Gender.Undefined;
                            break;
                        }
                    }

                    break;
                }

                Console.WriteLine("You need numbers from 1 to 3 you stupid piece of shit");
            }

            Console.WriteLine(
                "And now is the time to choose a subclass of you character:\nWizard(1)\nWarrior(2)\nBandit(3)");
            Console.ReadKey();
            Console.WriteLine(
                "Ooops... There is a problem. Whatever you choose, your subclass will be a wizard. The rest are not finalized yet, the game is on the prerelease beta gamma alpha test 0.0.0.0.1a.");

            wizard = new Wizard(name, race, gender, age);
            _mainPlayer = wizard;
        }

        //that is a method that would be called with !help, it shows information about basic game commands
        public void ShowInformationAboutCommands()
        {
            //add other commands
            Console.WriteLine(
                "!help - get info about commands\n!new_game - will start the game from the very beginning\nUSE SPELL [number of the spell in your spell list] TO [target character] WITH [spell power] - ");
        }

        public void GenerateLevel()
        {
            if (_difficultyLevel == 0)
            {
                
            }

            if (_difficultyLevel <= 3)
            {
                
            }

            if (_difficultyLevel is > 3 and <= 6)
            {
                
            }

            if (_difficultyLevel is > 6 and <= 10)
            {
                
                
                if 
            }
        }
    }
}
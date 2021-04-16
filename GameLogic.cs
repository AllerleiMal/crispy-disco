using System;
using System.Collections.Generic;
using System.Threading;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;

namespace OurCoolGame
{
    public class GameLogic
    {
        public static int MoveCounter { get; set; } = 0;
        private Wizard _mainPlayer;

        private int _difficultyLevel;
        private Random _random;

        public GameLogic()
        {
            _random = new Random();
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
            Console.ReadLine();
            Console.WriteLine(
                "Ooops... There is a problem. Whatever you choose, your subclass will be a wizard. The rest are not finalized yet, the game is on the prerelease beta gamma alpha test 0.0.0.0.1a.");

            wizard = new Wizard(name, race, gender, age);
            _mainPlayer = wizard;
        }

        //that is a method that would be called with !help, it shows information about basic game commands
        public void ShowInformationAboutCommands()
        {
            string temp;
            //maybe while true should be deleted
            while (true)
            {
                temp = Console.ReadLine();
                if (temp != "!help")
                {
                    continue;
                }

                //add other commands
                Console.WriteLine(
                    "!help - get info about commands\n!new_game - will start the game from the very beginning\nUSE SPELL [number of the spell in your spell list] TO [target character] WITH [spell power] - ...\n!inventory - to see your artefacts");
                break;
            }
        }

        public void ShowRules()
        {
            //there should be basic rules
        }

        public void GenerateLevel()
        {
            switch (_difficultyLevel)
            {
                case 0:
                {
                    RunTraining();
                    break;
                }
                case <= 3:
                {
                    //RunEasyLevel();
                    break;
                }
                case > 3 and <= 6:
                {
                    //RunMediumLevel();
                    break;
                }
                case > 6 and <= 10:
                {
                    // RunHardLevel();
                    if (_difficultyLevel == 10)
                    {
                        // RunFinalPlot();
                    }

                    break;
                }
            }
        }

        //this method is for creating a basic arena with 1 enemy with training messages
        private void RunTraining()
        {
            ++_difficultyLevel;
            _enemy.Add(new Wizard("dummy", Race.Human, Gender.Undefined, 10));
            Console.WriteLine(
                "Hello, exile! That is your first fight. Your enemy is {0}. Now we are going to train not to suck in the real fight.\nThere is something interesting in your bag, check it(enter \"!inventory\")",
                _enemy[0].Name);
            _mainPlayer.PickUpArtefact(new LightningStaff(150));
            while (true)
            {
                var temp = Console.ReadLine();
                if (temp == "!inventory")
                {
                    _mainPlayer.ShowInventory();
                    break;
                }
                Console.WriteLine("Something went wrong, try again and follow the right command's format");
            }
            Console.WriteLine(
                "Good job! Now you can see what is in your bag. Choose one of the artefact and use it on your enemy.\n Ah, ye... You don't know how. Enter \"!help\"");
            Thread.Sleep(2000);
            ShowInformationAboutCommands();
            Console.WriteLine("Ok, now you know more and can do something");
            Thread.Sleep(2000);
            Console.WriteLine("Try your artefact on the {0}", _enemy[0].Name);
            while (true)
            {
                var temp = Console.ReadLine();
                if (temp == "USE ARTEFACT 1 TO dummy")
                {
                    _mainPlayer.UseArtefact(_mainPlayer._inventory[0], _enemy[0]);
                    break;
                }
                Console.WriteLine("Something went wrong, try again and follow the right command's format");
            }
            Console.WriteLine("Good! Let's check, what happened with our dummy");
            Thread.Sleep(2000);
            Console.WriteLine("{0}/{1}", _enemy[0].CurrentHealthPoints, _enemy[0].MaxHealthPoints);
            Thread.Sleep(2000);
            Console.WriteLine("Now we would check how you can take damage");
            Thread.Sleep(2000);
            _enemy[0]._inventory.Add(new LightningStaff(1500));
            Thread.Sleep(2000);
            _enemy[0].UseArtefact(_enemy[0]._inventory[0], _mainPlayer);
            Thread.Sleep(2000);
            Console.WriteLine("Ouffff, your defence is really weak {0}/{1} HP", _mainPlayer.CurrentHealthPoints, _mainPlayer.MaxHealthPoints);
            Thread.Sleep(2000);
            _mainPlayer.PickUpArtefact(new LivingWater(BottleSize.Small));
            Thread.Sleep(2000);
            _mainPlayer.PickUpArtefact(new DeadWater(BottleSize.Small));
            Thread.Sleep(2000);
            Console.WriteLine("You have 2 special bottles. Living is for live regeneration and dead is for mana regeneration.\nThey disappoint after using, so think twice and don't use it when you are full. Now restore your HP");
            while (true)
            {
                var temp = Console.ReadLine();
                if (temp == "USE ARTEFACT 2 TO me")
                {
                    _mainPlayer.UseArtefact(_mainPlayer._inventory[1], _mainPlayer);
                    break;
                }
                Console.WriteLine("Something went wrong, try again and follow the right command's format");
            }
            Console.WriteLine("Now it is better, {0}/{1} HP", _mainPlayer.CurrentHealthPoints, _mainPlayer.MaxHealthPoints);

            /*
             * here should be same thing for spells, as it is for artefacts
             *
             *
             * after demonstration work of spells, use Dead Water as it was done with living water
             */
        }

        private void RunEasyLevel()
        {
            
        }

        private BottleSize RandomizeBottleSize()
        {
            var size = _random.Next(3);
            return size switch
            {
                0 => BottleSize.Small,
                1 => BottleSize.Medium,
                2 => BottleSize.Big,
                _ => BottleSize.Big
            };
        }

        private Artefact RandomizeArtefact()
        {
            
            return null;
        }
    }
}
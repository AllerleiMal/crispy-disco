using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;
using OurCoolGame.Spells;

namespace OurCoolGame
{
    public class GameLogic
    {
        public static int MoveCounter { get; set; } = 0;
        private Wizard _mainPlayer;

        private int _difficultyLevel;
        private readonly Random _random;

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
                if (int.TryParse(Console.ReadLine(), out age) && age >= 12)
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
            Thread.Sleep(2000);
            wizard = new Wizard(name, race, gender, age);
            _mainPlayer = wizard;
        }

        //that is a method that would be called with !help, it shows information about basic game commands
        public void InputProcessing()
        {
            string temp;
            //maybe while true should be deleted
            while (true)
            {
                temp = Console.ReadLine();
                if (temp == "!help")
                {
                    Console.WriteLine(
                        "!help - get info about commands\n!new_game - will start the game from the very beginning\n!use - get info about usage of spells and artefacts\n!inventory - to see your artefacts");
                    Thread.Sleep(2000);
                    Console.WriteLine("Ok, now you know more and can do something");
                    break;
                }

                if (temp == "!use")
                {
                    UseMenu();
                    break;
                }

                if (temp == "!inventory")
                {
                    _mainPlayer.ShowInventory();
                    break;
                }

                if (temp == "!new_game")
                {
                    GameStart(); /////////////////////////////
                    break;
                }

                //add other commands
                Console.WriteLine(
                    "omg... please, check what you're trying to enter. if you forget, i can remind: enter \"!help\"");
            }
        }

        private int SelectTarget()
        {
            Console.WriteLine("Now select your target:\n(0)YOU");
            for (int i = 1; i <= _enemy.Count; i++)
            {
                Console.WriteLine($"({i}) {_enemy[i - 1].Name} (ENEMY)");
            }

            int select;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out select) && select >= 0 && select <= _enemy.Count)
                {
                    return select;
                }

                Console.WriteLine("What are you trying to enter you stupid piece of shit. -20HP");
                _mainPlayer.CurrentHealthPoints -= 20;
            }
        }

        //a little menu for usage of spells and artefacts
        public void UseMenu()
        {
            Console.WriteLine("Write what do you want do use: \"spell\" or \"artefact\"");
            string temp;
            while (true)
            {
                temp = Console.ReadLine()?.ToUpper();
                if (temp == "A" || temp == "ARTEFACT")
                {
                    _mainPlayer.ShowInventory();
                    Thread.Sleep(2000);
                    Console.WriteLine("Pick a number of an artefact that you want to use");
                    int pickArtefact;
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out pickArtefact) &&
                            pickArtefact <= _mainPlayer._inventory.Count && pickArtefact > 0)
                        {
                            break;
                        }

                        Console.WriteLine("What are you trying to enter you stupid piece of shit. -20HP");
                        _mainPlayer.CurrentHealthPoints -= 10;
                    }

                    Thread.Sleep(2000);
                    int select = SelectTarget();
                    _mainPlayer.UseArtefact(_mainPlayer._inventory[pickArtefact - 1],
                        select == 0 ? _mainPlayer : _enemy[select - 1]);
                    Thread.Sleep(2000);
                    if (select == 0)
                        Console.WriteLine(
                            "YOU STUPID ASSHOLE WTF ARE YOU DOING K NOW YOU HAVE DOUBLE DAMAGE AHHAHA"); //JOKE
                    else
                        Console.WriteLine("OMG! Let's check, what happened");
                    Console.WriteLine("Your HP: {0}/{1}\nEnemy's HP:", _mainPlayer.CurrentHealthPoints,
                        _mainPlayer.MaxHealthPoints);
                    for (int i = 0; i < _enemy.Count; i++)
                    {
                        Console.WriteLine("{0}: {1}/{2}", _enemy[i].Name, _enemy[i].CurrentHealthPoints,
                            _enemy[i].MaxHealthPoints);
                    }

                    break;
                }

                if (temp == "SPELL" || temp == "S")
                {
                    bool isEmpty = !_mainPlayer._learnedSpells.Any();
                    if (isEmpty)
                    {
                        Console.WriteLine("Oopsie... You don't know it yet ;(");
                        UseMenu();
                    }
                    else
                    {
                        _mainPlayer.ShowLearnedSpells();
                        Thread.Sleep(2000);
                        Console.WriteLine("Pick a number of a spell that you want to use");
                        int pickSpell;
                        while (true)
                        {
                            if (int.TryParse(Console.ReadLine(), out pickSpell) &&
                                pickSpell <= _mainPlayer._learnedSpells.Count && pickSpell > 0)
                            {
                                break;
                            }

                            Console.WriteLine("What are you trying to enter you stupid piece of shit. -20HP");
                            _mainPlayer.CurrentHealthPoints -= 20;
                        }

                        int select = SelectTarget();
                        if (_mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellArmor().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellHeal().ToString())
                        {
                            Console.WriteLine("Enter magic power");
                            int magic;
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out magic) && magic > 0 &&
                                    magic * _mainPlayer._learnedSpells[pickSpell - 1].ManaCost <= _mainPlayer.CurMana)
                                {
                                    break;
                                }

                                Console.WriteLine("What are you trying to enter you stupid piece of shit. -20HP");
                                _mainPlayer.CurrentHealthPoints -= 20;
                            }

                            _mainPlayer.CastSpell(_mainPlayer._learnedSpells[pickSpell - 1],
                                select == 0 ? _mainPlayer : _enemy[select - 1], magic);
                            Thread.Sleep(2000);
                            Console.WriteLine("OMG! Let's check, what happened");
                            Console.WriteLine("Your HP: {0}/{1}\nEnemy's HP: {2}/{3}", _mainPlayer.CurrentHealthPoints,
                                _mainPlayer.MaxHealthPoints, _enemy[0].CurrentHealthPoints,
                                _enemy[0].MaxHealthPoints);
                            break;
                        }

                        if (_mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellAntidote().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellCure().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellRevival().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellUnparalyze().ToString())
                        {
                            _mainPlayer.CastSpell(_mainPlayer._learnedSpells[pickSpell - 1],
                                select == 0 ? _mainPlayer : _enemy[select - 1]);
                            Console.WriteLine("OMG! Let's check, what happened");
                            Console.WriteLine("Your HP: {0}/{1}\nEnemy's HP: {2}/{3}", _mainPlayer.CurrentHealthPoints,
                                _mainPlayer.MaxHealthPoints, _enemy[0].CurrentHealthPoints,
                                _enemy[0].MaxHealthPoints);
                        }
                    }

                    break;
                }

                Console.WriteLine(
                    "Something went wrong, try again and follow the right command's format. -20HP"); //JOKE
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
                case 1:
                {
                    RunEasyLevel();
                    break;
                }
                case 2:
                {
                    RunMediumLevel();
                    break;
                }
                case 3:
                {
                    RunHardLevel();
                    break;
                }
                case 4:
                {
                    RunFinalPlot();
                    break;
                }
            }
        }

        //this method is for creating a basic arena with 1 enemy with training messages
        private void RunTraining()
        {
            _enemy.Add(new Wizard("dummy", Race.Human, Gender.Undefined, 10));
            Console.WriteLine(
                "Hello, exile! That is your first fight. Your enemy is {0}. Now we are going to train not to suck in the real fight.\nThere is something interesting in your bag, check it(enter \"!inventory\")",
                _enemy[0].Name);
            _mainPlayer.PickUpArtefact(new LightningStaff());
            InputProcessing();

            Console.WriteLine(
                "Good job! Now you can see what is in your bag. Choose one of the artefact and use it on your enemy.\n Ah, ye... You don't know how. Enter \"!help\"");
            Thread.Sleep(2000);
            InputProcessing();
            UseMenu();
            Console.WriteLine("Now we would check how you can take damage");
            Thread.Sleep(2000);
            _enemy[0]._inventory.Add(new LightningStaff());
            Thread.Sleep(2000);
            _enemy[0].UseArtefact(_enemy[0]._inventory[0], _mainPlayer);
            Thread.Sleep(2000);
            Console.WriteLine("Ouffff, your defence is really weak {0}/{1} HP", _mainPlayer.CurrentHealthPoints,
                _mainPlayer.MaxHealthPoints);
            Thread.Sleep(2000);
            _mainPlayer.PickUpArtefact(new LivingWater(BottleSize.Small));
            Thread.Sleep(2000);
            _mainPlayer.PickUpArtefact(new DeadWater(BottleSize.Small));
            Thread.Sleep(2000);
            Console.WriteLine(
                "You have 2 special bottles. Living is for live regeneration and dead is for mana regeneration.\nThey disappoint after using, so think twice and don't use it when you are full. Now restore your HP");
            UseMenu();
            Console.WriteLine("Now let's check what you can do! Check your spells");
            _mainPlayer.LearnSpell(new SpellHeal());
            UseMenu();
            Console.WriteLine("Training is ended. Now you can begin you journey");
            Console.Write("Preparing all things down");
            Thread.Sleep(2000);
            Console.Write(".");
            Thread.Sleep(2000);
            Console.Write(".");
            Thread.Sleep(2000);
            Console.Write(".");
            Console.WriteLine("");
            Thread.Sleep(2000);
            _enemy.Clear();
            _teammates.Clear();
            ++_difficultyLevel;
        }

        //this method will generate easy fight situation 1v2 or 1v1
        private void RunEasyLevel()
        {
            LevelStartingMessages("-- EASY LEVEL __", ConsoleColor.Cyan);
            _enemy.Add(new Wizard("Tramp", Race.Human, Gender.Female, 42));


            _enemy.Clear();
            _teammates.Clear();
            ++_difficultyLevel;
        }

        private void GenerateNamesForEasyLevel()
        {
        }

        private void LevelStartingMessages(string message, ConsoleColor color)
        {
            MoveCounter = 0;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Thread.Sleep(2000);
            ChooseArtefactWhenLevelStarts();
            Thread.Sleep(2000);
        }

        private void ChooseArtefactWhenLevelStarts()
        {
            Console.WriteLine("It's time to choose the artefact: ");
            Thread.Sleep(1000);
            Console.WriteLine(
                "(1)Bottle of living water\n(2)Bottle of dead water\n(3)Basilisk eye\n(4)Frog legs decoct\n(5)Poisonous saliva");
            int switchIntInput;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out switchIntInput) || switchIntInput is < 1 or > 5)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                    _mainPlayer.CurrentHealthPoints -= 50;
                    continue;
                }
                break;
            }

            switch (switchIntInput)
            {
                case 1:
                {
                    _mainPlayer.PickUpArtefact(new LivingWater(RandomizeBottleSize()));
                    break;
                }
                case 2:
                {
                    _mainPlayer.PickUpArtefact(new DeadWater(RandomizeBottleSize()));
                    break;
                }
                case 3:
                {
                    _mainPlayer.PickUpArtefact(new BasiliskEye());
                    break;
                }
                case 4:
                {
                    _mainPlayer.PickUpArtefact(new FrogLegsDecoct());
                    break;
                }
                case 5:
                {
                    _mainPlayer.PickUpArtefact(new PoisonousSaliva());
                    break;
                }
            }
        }

        //this method will generate easy fight situation 2v2 or 2v3
        private void RunMediumLevel()
        {
            LevelStartingMessages("-- MEDIUM LEVEL --", ConsoleColor.DarkCyan);

            ++_difficultyLevel;
        }

        //this method will generate easy fight situation 2v3 or 2v4 ??????
        private void RunHardLevel()
        {
            LevelStartingMessages("-- HARD LEVEL --", ConsoleColor.DarkBlue);
            ++_difficultyLevel;
        }

        //final plot will be lineal as the training level i bet
        private void RunFinalPlot()
        {
            LevelStartingMessages("-- FINAL --", ConsoleColor.Red);
            ++_difficultyLevel;
        }

        //this is an additional method to create living/dead water bottles during artefact generation or after killing bots
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

        //this method will be used to generate artefacts for bots and at the level start
        private Artefact RandomizeArtefact()
        {
            var artefactNumber = _random.Next(1, 5);
            return artefactNumber switch
            {
                1 => new FrogLegsDecoct(),
                2 => new BasiliskEye(),
                3 => new DeadWater(RandomizeBottleSize()),
                4 => new LivingWater(RandomizeBottleSize()),
                5 => new PoisonousSaliva(),
                _ => new LivingWater(RandomizeBottleSize())
            };
        }

        public bool FinalLevelComplete()
        {
            return _difficultyLevel == 6;
        }

        //use it after all made a move
        public void UpdateMoveCounters()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveCounter -= 1;
            }

            foreach (var teammate in _teammates)
            {
                teammate.MoveCounter -= 1;
            }

            _mainPlayer.MoveCounter -= 1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;
using OurCoolGame.Spells;
using System.Text.RegularExpressions;

namespace OurCoolGame
{
    public class GameLogic
    {
        //common move counter is used to generate new level, when previous was ended
        public static int MoveCounter { get; set; } = 0;
        //main player is a user, that plays game
        private Wizard _mainPlayer;
        //enemy generator is a separate class that is used to create enemies in each level
        EnemyGenerator _enemyGenerator;
        
        //difficulty level depends on the quantity of levels that you have finished, also help to choose which level should be generated
        public int _difficultyLevel;
        //single field to reduce the possibility of same generation
        private readonly Random _random;
        //list of enemies that is active now
        public List<Wizard> _enemy;
        
        //constructor that initialize all fields
        public GameLogic()
        {
            _random = new Random();
            _difficultyLevel = 0;
            _mainPlayer = null;
            _enemy = new List<Wizard>();
            _enemyGenerator = new EnemyGenerator();
        }

        
        //output starting message
        public void GameStart()
        {
            Console.WriteLine(
                "Welcome!\nIt is our mini version of console RPG game.\nAll the characters are fictional, and the coincidences are random.\nThe whole story will develop in a magical medieval fantasy world(do not be surprised by talking goblins and orcs).\nYour task is to pass all the tests and overcome the difficulties on the way to such a cherished goal - to learn magic from the great Merlin.\nP.S. follow the instructions that will be given later, otherwise you risk hearing a lot of bad words in your direction. You can enter \"!help\" to get info with all valid commands.\n(Press any key to continue)\n");
            Console.ReadLine();
        }

        //works as a constructor, it creates character and returns it
        public Wizard CreateCharacter(Wizard wizard)
        {
            Console.WriteLine("Now is the time to create the character and choose the subclass");
            string name;
            while (true)
            {
                Console.WriteLine("Enter the name of your character(name couldn't be empty):");
                name = Console.ReadLine();
                //regexp to process and check the input data
                Regex reg = new Regex(@"^\s*$");
                if (!reg.IsMatch(name))
                {
                    while(name[0] == ' ')
                        name = name.Remove(0, 1);
                    while(name[^1] == ' ')
                        name = name.Remove(name.Length - 1, 1);
                    break;
                }

                Console.WriteLine("Wrong format");
            }

            int age;
            while (true)
            {
                Console.WriteLine("Enter the age of your character:");
                //here we try to parse string with age, and if it is not possible, ask for input one more time
                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age < 12)
                    {
                        Console.WriteLine("Age must be at least 12. Try again");
                        continue;
                    }

                    break;
                }

                Console.WriteLine(
                    "It is probably a miss click or you don't even know, that age contains only numbers");
            }

            var race = Race.Elf;
            while (true)
            {
                Console.WriteLine("Which race would you choose:\nHuman(1)\nGnome(2)\nElf(3)\nOrc(4)\nGoblin(5)");
                //here we try to parse string with choice number, and if it is not possible or number is wrong, ask for input one more time                 
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

                Console.WriteLine("You need numbers from 1 to 5");
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

                Console.WriteLine("You need numbers from 1 to 3");
            }

            Console.WriteLine(
                "And now is the time to choose a subclass of you character:\nWizard(1)\nWarrior(2)\nBandit(3)");
            Console.ReadLine();
            Console.WriteLine(
                "Ooops... There is a problem. Whatever you choose, your subclass will be a wizard. The rest are not finalized yet, the game is on the prerelease beta gamma alpha test 0.0.0.0.1a.");
            Thread.Sleep(2000);
            wizard = new Wizard(name, race, gender, age);
            _mainPlayer = wizard;
            return _mainPlayer;
        }

        //that is a method that would be called instantly to process user input and call appropriate methods
        public void InputProcessing(int playerMustChoose = 0)
        {
            Console.WriteLine("Enter the command");
            string temp;
            while (true)
            {
                temp = Console.ReadLine();
                //!help calls basic command info
                if (temp == "!help" && (playerMustChoose is 0 or 1))
                {
                    Console.WriteLine(
                        "!help - get info about commands\n!use - get info about usage of spells and artefacts\n!inventory - to see your artefacts\n!show_spells - to see list of learned spells");
                    Thread.Sleep(2000);
                    break;
                }

                //!use helps to use artefacts from inventory and spells from list of learned spells
                if (temp == "!use" && (playerMustChoose is 0 or 2))
                {
                    UseMenu();
                    //we need it to block enemy move in training
                    if (playerMustChoose == 0)
                    {
                        foreach (var enemy in _enemy)
                        {
                            EnemyMove(enemy);
                        }

                        UpdateMoveCounters();
                        Console.WriteLine("OMG let's check what happened");
                        ShowFightInfo();
                    }

                    ++MoveCounter;
                    break;
                }
                
                if (temp == "!rules" && playerMustChoose == 0 )
                {
                    ShowRules();
                    break;
                }
                
                //shows current inventory
                if (temp == "!inventory" && (playerMustChoose is 0 or 3))
                {
                    _mainPlayer.ShowInventory();
                    break;
                }
                
                //shows learned spells
                if (temp == "!learned_spells" && playerMustChoose == 0)
                {
                    _mainPlayer.ShowLearnedSpells();
                    break;
                }
                
                //special message that is used in training only
                if (playerMustChoose != 0)
                {
                    Console.WriteLine("woops, there's mistake in command or you are truing to break the tutorial");
                    continue;
                }

                //common message of wrong input
                Console.WriteLine(
                    "omg... please, check what you're trying to enter. if you forget, i can remind: enter \"!help\"");
            }
        }

        //method to choose on which character artefact or spells are used
        private int SelectTarget()
        {
            Console.WriteLine("Now select your target:\n(0)YOU");
            for (int i = 1; i <= _enemy.Count; i++)
            {
                Console.WriteLine(_enemy[i - 1].CharacterState == State.Dead
                    ? $"({i}) {_enemy[i - 1].Name} (ENEMY) (DEAD)"
                    : $"({i}) {_enemy[i - 1].Name} (ENEMY)");
            }

            int select;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out select) && select >= 0 && select <= _enemy.Count)
                {
                    return select;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                Console.ResetColor();
            }
        }

        //a little menu for usage of spells and artefacts
        private void UseMenu()
        {
            Console.WriteLine("Write what do you want do use: \"spell\" or \"artefact\"");
            string temp;
            while (true)
            {
                temp = Console.ReadLine()?.ToUpper();
                //artefact usage section
                if (temp is "A" or "ARTEFACT")
                {
                    _mainPlayer.ShowInventory();
                    Thread.Sleep(2000);
                    Console.WriteLine("Pick a number of an artefact that you want to use");
                    int pickArtefact;
                    while (true)
                    {
                        //checks for correct input of picked artefact
                        if (int.TryParse(Console.ReadLine(), out pickArtefact) &&
                            pickArtefact <= _mainPlayer._inventory.Count && pickArtefact > 0)
                        {
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                        Console.ResetColor();
                    }

                    Thread.Sleep(2000);
                    //call for target selection
                    int select = SelectTarget();
                    //use previously picked artefact on selected enemy
                    _mainPlayer.UseArtefact(_mainPlayer._inventory[pickArtefact - 1],
                        select == 0 ? _mainPlayer : _enemy[select - 1]);
                    Thread.Sleep(2000);
                    break;
                }

                //spell cast section
                if (temp is "SPELL" or "S")
                {
                    //check for empty list of learned spells
                    bool isEmpty = !_mainPlayer._learnedSpells.Any();
                    if (isEmpty)
                    {
                        Console.WriteLine("Oopsie... You don't know any spell yet ;(");
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
                            //check for correct input
                            if (int.TryParse(Console.ReadLine(), out pickSpell) &&
                                pickSpell <= _mainPlayer._learnedSpells.Count && pickSpell > 0)
                            {
                                break;
                            }

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                            Console.ResetColor();
                        }

                        //select target for spell cast
                        int select = SelectTarget();
                        
                        if (_mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellHeal().ToString() || 
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellFireball().ToString())
                        {
                            //cast spells with magic power parameter
                            Console.WriteLine("Enter magic power");
                            int magic;
                            while (true)
                            {
                                //check for correct input of spell choice
                                if (int.TryParse(Console.ReadLine(), out magic) && magic > 0 &&
                                    magic * _mainPlayer._learnedSpells[pickSpell - 1].ManaCost <=
                                    _mainPlayer.CurrentMana)
                                {
                                    break;
                                }

                                if (magic * _mainPlayer._learnedSpells[pickSpell - 1].ManaCost <=
                                    _mainPlayer.CurrentMana)
                                {
                                    Console.WriteLine("You don't have enough mana");
                                    UseMenu();
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                                Console.ResetColor();
                            }
                            //cast picked spell with magic power parameter on selected enemy
                            _mainPlayer.CastSpell(_mainPlayer._learnedSpells[pickSpell - 1],
                                select == 0 ? _mainPlayer : _enemy[select - 1], magic);
                            break;
                        }
                        //cast all the spells that do not need magic power parameter
                        if (_mainPlayer._learnedSpells[pickSpell - 1].ToString() != new SpellHeal().ToString() || 
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() != new SpellFireball().ToString())
                        {
                            _mainPlayer.CastSpell(_mainPlayer._learnedSpells[pickSpell - 1],
                                select == 0 ? _mainPlayer : _enemy[select - 1]);
                        }
                    }

                    break;
                }

                //common message for wrong input
                Console.WriteLine(
                    "Something went wrong, try again and follow the right command's format.");
            }
        }


        //method to output game rules
        private void ShowRules()
        {
            Console.WriteLine(
                "So, now there is the most important part - RULES! Jk, have fan and don't write anything gods don't want to see. It is quite dangerous...");
        }

        //creates level depending on difficulty level
        public void GenerateLevel()
        {
            MoveCounter = 0;
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
            //create enemy
            _enemy.Add(new Wizard("dummy", Race.Human, Gender.Undefined, 10));
            Console.WriteLine(
                "Hello, exile! That is your first fight. Your enemy is {0}. Now we are going to train not to suck in the real fight.\nThere is something interesting in your bag, check it(enter \"!inventory\")",
                _enemy[0].Name);
            _mainPlayer.PickUpArtefact(new LightningStaff());
            //ask for !inventory
            InputProcessing(3);

            Console.WriteLine(
                "Good job! Now you can see what is in your bag. Choose one of the artefact and use it on your enemy.\n Ah, ye... You don't know how. Enter \"!help\"");
            Thread.Sleep(2000);
            //ask for !help
            InputProcessing(1);
            Console.WriteLine(
                "ok, now let's try !use. attack enemy with you Lightning Staff");
            //ask for !use
            InputProcessing(2);
            Console.WriteLine("Look at this damage\n{0}: {1}/{2} HP", _enemy[0].Name, _enemy[0].CurrentHealthPoints, _enemy[0].MaxHealthPoints);
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
                "You have 2 special bottles. Living is for live regeneration and dead is for mana regeneration.\nThey disappoint after using, so think twice and don't use it when you are full. Now restore your HP(start with !use)");
            //ask fot !use
            InputProcessing(2);
            Console.WriteLine("Now let's try to use heal spell!(start with !use)");
            _mainPlayer.LearnSpell(new SpellHeal());
            //ask for !use
            InputProcessing(2);
            Console.WriteLine("Training is ended. Now you can begin you journey");
            //weapon randomizer to change main player's main renewable artefact
            var weapon = _random.Next(0, 2);
            switch (weapon)
            {
                case 0:
                    Console.WriteLine(
                        "Mhm, we decided that you are to young for the new weapon. Continue your fight with LightningStaff!");
                    break;

                case 1:
                    Console.WriteLine(
                        "Otaaay, Lets try something new!. Continue your fight with BloodMace!");
                    _mainPlayer._inventory[0] = new BloodMace();
                    break;

                case 2:
                    Console.WriteLine(
                        "Wow, you did a great job! So we decided to give you some really cool staff. Continue your fight with ShadowDagger!");
                    _mainPlayer._inventory[0] = new ShadowDagger();
                    break;
            }
            Console.Write("Preparing all things down");
            Thread.Sleep(2000);
            Console.Write(".");
            Thread.Sleep(2000);
            Console.Write(".");
            Thread.Sleep(2000);
            Console.Write(".");
            Console.WriteLine("");
            Thread.Sleep(2000);
            //clear all training changes, except main player inventory
            _enemy.Clear();
            ++_difficultyLevel;
            MoveCounter = 0;
            _mainPlayer.CurrentHealthPoints = _mainPlayer.MaxHealthPoints;
            _mainPlayer.CurrentMana = _mainPlayer.MaxMana;
        }

        //each level starts with common methods and messages, that is the combination of all these methods 
        private void LevelStartingMessages(string message, ConsoleColor color)
        {
            MoveCounter = 0;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Thread.Sleep(2000);
            LearnSpellWhenLevelStarts();
            ChooseArtefactWhenLevelStarts();
            Thread.Sleep(2000);
        }

        //main player choose which spell he want to learn on the level start
        private void LearnSpellWhenLevelStarts()
        {
            Console.WriteLine("New level starts so you can learn spell: ");
            Thread.Sleep(1000);
            List<Spell> unlearnedSpells = new List<Spell>(_enemyGenerator._allSpells);
            //delete already learned spells
            foreach (var spell in _mainPlayer._learnedSpells)
            {
                unlearnedSpells.Remove(unlearnedSpells.Find(match =>
                    match.ToString() == spell.ToString()));
            }
            //output unlearned spells
            for (var i = 0; i < unlearnedSpells.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {unlearnedSpells[i]}");
            }

            int switchIntInput;
            //input choice and check if it is correct 
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out switchIntInput) || switchIntInput < 1 ||
                    switchIntInput > unlearnedSpells.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            //learn picked spell
            _mainPlayer.LearnSpell(unlearnedSpells[switchIntInput - 1]);
        }

        //same method as it was for spells
       private void ChooseArtefactWhenLevelStarts()
        {
            Console.WriteLine("It's time to choose the artefact: ");
            Thread.Sleep(1000);
            Console.WriteLine(
                "(1)Bottle of living water\n(2)Bottle of dead water\n(3)Basilisk eye\n(4)Frog legs decoct\n(5)Poisonous saliva");
            int switchIntInput;
            //input choice and check if it is correct
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out switchIntInput) || switchIntInput is < 1 or > 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                    continue;
                }

                break;
            }

            //switch to pick artefact that was chosen
            switch (switchIntInput)
            {
                case 1:
                {
                    _mainPlayer.PickUpArtefact(new LivingWater(_enemyGenerator.RandomizeBottleSize()));
                    break;
                }
                case 2:
                {
                    _mainPlayer.PickUpArtefact(new DeadWater(_enemyGenerator.RandomizeBottleSize()));
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
       
       //generate easy level with 1 enemy and 500hp
        private void RunEasyLevel()
        {
            LevelStartingMessages("-- EASY LEVEL --", ConsoleColor.Cyan);
            //enemy generation depends on difficulty level, for easy level that is 1
            _enemy.Add(_enemyGenerator.Generate(1));
            Console.WriteLine("Say hi to your first enemy - {0}! He is {1}, his age: {2}, ", _enemy[0].Name,
                _enemy[0].CharacterRace, _enemy[0].Age);
            _enemy[0].MaxHealthPoints = 500;
            Console.WriteLine("By the way, his max health points is {0}", _enemy[0].MaxHealthPoints);
        }
        
        //generate medium level with 1 enemy and 1000hp
        private void RunMediumLevel()
        {
            LevelStartingMessages("-- MEDIUM LEVEL --", ConsoleColor.DarkCyan);
            _mainPlayer.CurrentHealthPoints = _mainPlayer.MaxHealthPoints;
            _mainPlayer.CurrentMana = _mainPlayer.MaxMana;
            //enemy generation depends on difficulty level, for medium level that is 2
            _enemy.Add(_enemyGenerator.Generate(2));
            Console.WriteLine("Your enemies are:");
            foreach (var enemy in _enemy)
            {
                Console.WriteLine("{0}. He is {1}, his age: {2}, ", enemy.Name,
                    enemy.CharacterRace, enemy.Age);
                _enemy[0].MaxHealthPoints = 1000;
                Console.WriteLine("By the way, his max health points is {0}", enemy.MaxHealthPoints);
            }
        }

        //generate hard level with 2 enemies and 750hp
        private void RunHardLevel()
        {
            LevelStartingMessages("-- HARD LEVEL --", ConsoleColor.DarkBlue);
            _mainPlayer.CurrentHealthPoints = _mainPlayer.MaxHealthPoints;
            _mainPlayer.CurrentMana = _mainPlayer.MaxMana;
            //enemy generation depends on difficulty level, for hard level that is 3
            _enemy.Add(_enemyGenerator.Generate(3));
            _enemy.Add(_enemyGenerator.Generate(3));
            Console.WriteLine("Your enemies are:");
            foreach (var t in _enemy)
            {
                Console.WriteLine("{0}. He is {1}, his age: {2}, ", t.Name,
                    t.CharacterRace, t.Age);
                t.MaxHealthPoints = 750;
                Console.WriteLine("By the way, his max health points is {0}", t.MaxHealthPoints);
            }
        }
        
        private void RunFinalPlot()
        {
            LevelStartingMessages("-- FINAL --", ConsoleColor.Red);
            Console.WriteLine("Final level will suddenly appear if our team will get 10 as a game project mark :)");
            ++_difficultyLevel;
        }

        //this method used to end the game when final level is completed
        public bool FinalLevelComplete()
        {
            return _difficultyLevel == 5;
        }

        //update personal move counter of each character
        private void UpdateMoveCounters()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveCounter -= 1;
            }

            _mainPlayer.MoveCounter -= 1;
        }

        //method to make enemy move logic
        private void EnemyMove(Wizard enemy)
        {
            //dead enemy can't make a move
            if (enemy.CharacterState == State.Dead)
            {
                return;
            }

            //enemy that need revival, heal or unparalyze
            Wizard inDangerTarget = enemy;
            //enemy that need mana regeneration
            Wizard lowerManaTarget = enemy;

            //if enemy quantity more than 1 we want to choose the most weakened character in list of enemies
            if (_enemy.Count > 1)
            {
                foreach (var wizard in _enemy)
                {
                    if (wizard.CurrentHealthPoints < inDangerTarget.CurrentHealthPoints ||
                        wizard.CharacterState is State.Dead or State.Paralyzed)
                    {
                        if (wizard.CharacterState == State.Paralyzed &&
                            inDangerTarget.CharacterState == State.Paralyzed)
                        {
                            //if both characters are paralyzed it chooses one with least hp 
                            switch (wizard.CurrentHealthPoints < inDangerTarget.CurrentHealthPoints)
                            {
                                case true:
                                {
                                    inDangerTarget = wizard;
                                    break;
                                }
                                case false:
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            inDangerTarget = wizard;
                        }
                    }
                    //find character with the least mana
                    if (wizard.CurrentMana < lowerManaTarget.CurrentMana)
                    {
                        lowerManaTarget = wizard;
                    }
                }
            }
            //switch to revive or unparalyze character or his teammate
            switch (inDangerTarget.CharacterState)
            {
                case State.Dead:
                {
                    if (TryCastSpell(new SpellRevival(), enemy, inDangerTarget))
                    {
                        return;
                    }

                    break;
                }
                case State.Paralyzed:
                {
                    if (TryCastSpell(new SpellUnparalyze(), enemy, inDangerTarget))
                    {
                        return;
                    }

                    break;
                }
            }
            
            //cast heal spell when character is lowHP and mana is not low
            if (inDangerTarget.CurrentHealthPoints < inDangerTarget.MaxHealthPoints / 2 && enemy.CurrentMana >= enemy.MaxMana / 2)
            {
                if (TryCastSpell(new SpellHeal(), enemy, inDangerTarget))
                {
                    return;
                }
            }
            
            Artefact outFromFunc;
            //use living water when it is in inventory and inDangerTarget needs hp regeneration
            if (inDangerTarget.CurrentHealthPoints < inDangerTarget.MaxHealthPoints / 2 &&
                enemy.HasWaterBottle(true, out outFromFunc))
            {
                enemy.UseArtefact(outFromFunc, inDangerTarget);
                return;
            }

            //use dead water when it is in inventory and lowerManaTarget needs mana regeneration
            if (lowerManaTarget.CurrentMana < lowerManaTarget.MaxMana / 4 &&
                enemy.HasWaterBottle(false, out outFromFunc))
            {
                enemy.UseArtefact(outFromFunc, lowerManaTarget);
                return;
            }
            
            //use armor spell when mana is almost full(it is quite expensive)
            if (enemy.CurrentMana > enemy.MaxMana * 8 / 10)
            {
                if (TryCastSpell(new SpellArmor(), enemy, enemy))
                {
                    return;
                }
            }

            //switch to undo the negative state
            switch (enemy.CharacterState)
            {
                case State.Poisoned:
                {
                    if (TryUseArtefact(new FrogLegsDecoct(), enemy, enemy))
                    {
                        return;
                    }

                    break;
                }
                case State.Sick:
                {
                    if (TryCastSpell(new SpellAntidote(), enemy, enemy))
                    {
                        return;
                    }

                    break;
                }
            }
            
            //use artefact with negative status on main player
            if (_mainPlayer.CharacterState != State.Dead &&
                (_mainPlayer.CharacterState is State.Healthy or State.Weakened) &&
                enemy.HasStatusArtefact(out outFromFunc))
            {
                enemy.UseArtefact(outFromFunc, _mainPlayer);
                return;
            }
            
            //use spell FireBall on main player when there is enough mana and spell is learned
            if (enemy._learnedSpells.FindIndex(spell => spell.ToString() == new SpellFireball().ToString()) != -1 &&
                enemy.CurrentMana > enemy.MaxMana / 4)
            {
                enemy.CastSpell(new SpellFireball(), _mainPlayer, enemy.CurrentMana / 4);
            }

            enemy.UseArtefact(enemy._inventory[0], _mainPlayer);
        }

        //method that casts spell and returns true, when spell is learned, otherwise doesn't cast and return false
        private bool TryCastSpell(Spell spell, Wizard origin, Wizard target)
        {
            var usedItemIndex = origin._learnedSpells.FindIndex(targetSpell => targetSpell.ToString() == spell.ToString());
            if (usedItemIndex != -1)
            {
                if (spell == new SpellHeal())
                {
                    origin.CastSpell(origin._learnedSpells[usedItemIndex], target, origin.CurrentMana / 4);
                }
                else
                {
                    origin.CastSpell(origin._learnedSpells[usedItemIndex], target);
                }
                return true;
            }

            return false;
        }

        //method that uses artefact and returns true, when artefact is in inventory, otherwise doesn't use and return false
        private bool TryUseArtefact(Artefact artefact, Wizard origin, Wizard target)
        {
            var usedItemIndex = origin._inventory.FindIndex(targetArtefact => targetArtefact.ToString() == artefact.ToString());
            if (usedItemIndex != -1)
            {
                origin.UseArtefact(origin._inventory[usedItemIndex], target);
                return true;
            }

            return false;
        }

        //output basic info about main player and enemies that we use after each move
        private void ShowFightInfo()
        {
            Console.WriteLine("Your HP: {0}/{1}\nYour MP: {2}/{3}\nYour state: {4}\nEnemy's HP:", _mainPlayer.CurrentHealthPoints,
                _mainPlayer.MaxHealthPoints, _mainPlayer.CurrentMana, _mainPlayer.MaxMana, _mainPlayer.CharacterState);
            for (int i = 0; i < _enemy.Count; i++)
            {
                Console.WriteLine("{0} HP: {1}/{2} state:{3}", _enemy[i].Name, _enemy[i].CurrentHealthPoints,
                    _enemy[i].MaxHealthPoints, _enemy[i].CharacterState);
            }
        }
    }
}
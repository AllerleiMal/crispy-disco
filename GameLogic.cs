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
        public static int MoveCounter { get; set; } = 0;
        private Wizard _mainPlayer;
        EnemyGenerator _enemyGenerator;

        public int _difficultyLevel;
        private readonly Random _random;
        
        public GameLogic()
        {
            _random = new Random();
            _difficultyLevel = 0;
            _mainPlayer = null;
            _enemy = new List<Wizard>();
            _enemyGenerator = new EnemyGenerator();
        }

        public List<Wizard> _enemy;
        
        public void GameStart()
        {
            Console.WriteLine(
                "Welcome!\nIt is our mini version of console RPG game.\nAll the characters are fictional, and the coincidences are random.\nThe whole story will develop in a magical medieval fantasy world(do not be surprised by talking goblins and orcs).\nYour task is to pass all the tests and overcome the difficulties on the way to such a cherished goal - to learn magic from the great Merlin.\nP.S. follow the instructions that will be given later, otherwise you risk hearing a lot of bad words in your direction. You can enter \"!help\" to get info with all valid commands.\n(Press any key to continue)\n");
            Console.ReadLine();
        }

        public Wizard CreateCharacter(Wizard wizard)
        {
            Console.WriteLine("Now is the time to create the character and choose the subclass");
            string name;
            while (true)
            {
                Console.WriteLine("Enter the name of your character(name couldn't be empty):");
                name = Console.ReadLine();
                Regex reg = new Regex(@"^\s*$");
                if (!reg.IsMatch(name))
                {
                    break;
                }

                Console.WriteLine("As I already said name couldn't be empty");
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

        //that is a method that would be called with !help, it shows information about basic game commands
        public void InputProcessing(int playerMustChoose = 0)
        {
            string temp;
            while (true)
            {
                temp = Console.ReadLine();
                if (temp == "!help" && (playerMustChoose == 0 || playerMustChoose == 1))
                {
                    Console.WriteLine(
                        "!help - get info about commands\n!use - get info about usage of spells and artefacts\n!inventory - to see your artefacts\n!show_spells - to see list of learned spells");
                    Thread.Sleep(2000);
                    break;
                }

                if (temp == "!use" && (playerMustChoose == 0 || playerMustChoose == 2))
                {
                    UseMenu();
                    if (playerMustChoose == 0)
                    {
                        foreach (var enemy in _enemy)
                        {
                            EnemyMove(enemy);
                        }

                        UpdateMoveCounters();
                        Console.WriteLine("OMG let's check what happned");
                        ShowFightInfo();
                    }

                    ++MoveCounter;
                    break;
                }

                if (temp == "!inventory" && (playerMustChoose == 0 || playerMustChoose == 3))
                {
                    _mainPlayer.ShowInventory();
                    break;
                }

                if (temp == "!learned_spells" && playerMustChoose == 0)
                {
                    _mainPlayer.ShowLearnedSpells();
                    break;
                }

                if (playerMustChoose != 0)
                {
                    Console.WriteLine("woops, there's mistake in command or you are truing to break the tutorial");
                    continue;
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

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                _mainPlayer.CurrentHealthPoints -= 50;
                Console.ResetColor();
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

                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                        _mainPlayer.CurrentHealthPoints -= 50;
                        Console.ResetColor();
                    }

                    Thread.Sleep(2000);
                    int select = SelectTarget();
                    _mainPlayer.UseArtefact(_mainPlayer._inventory[pickArtefact - 1],
                        select == 0 ? _mainPlayer : _enemy[select - 1]);
                    Thread.Sleep(2000);
                    break;
                }

                if (temp == "SPELL" || temp == "S")
                {
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
                            if (int.TryParse(Console.ReadLine(), out pickSpell) &&
                                pickSpell <= _mainPlayer._learnedSpells.Count && pickSpell > 0)
                            {
                                break;
                            }

                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                            _mainPlayer.CurrentHealthPoints -= 50;
                            Console.ResetColor();
                        }

                        int select = SelectTarget();
                        if (_mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellArmor().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellHeal().ToString() || 
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellFireball().ToString())
                        {
                            Console.WriteLine("Enter magic power");
                            int magic;
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out magic) && magic > 0 &&
                                    magic * _mainPlayer._learnedSpells[pickSpell - 1].ManaCost <=
                                    _mainPlayer.CurrentMana)
                                {
                                    break;
                                }

                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                                _mainPlayer.CurrentHealthPoints -= 50;
                                Console.ResetColor();
                            }

                            _mainPlayer.CastSpell(_mainPlayer._learnedSpells[pickSpell - 1],
                                select == 0 ? _mainPlayer : _enemy[select - 1], magic);
                            break;
                        }

                        if (_mainPlayer._learnedSpells[pickSpell - 1].ToString() != new SpellArmor().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() != new SpellHeal().ToString() ||
                            _mainPlayer._learnedSpells[pickSpell - 1].ToString() == new SpellFireball().ToString())
                        {
                            _mainPlayer.CastSpell(_mainPlayer._learnedSpells[pickSpell - 1],
                                select == 0 ? _mainPlayer : _enemy[select - 1]);
                        }
                    }

                    break;
                }

                Console.WriteLine(
                    "Something went wrong, try again and follow the right command's format.");
            }
        }


        public void ShowRules()
        {
            Console.WriteLine(
                "So, now there is the most important part - RULES! Jk, have fan and don't write anything gods don't want to see. It is quite dangerous...");
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
            InputProcessing(3);

            Console.WriteLine(
                "Good job! Now you can see what is in your bag. Choose one of the artefact and use it on your enemy.\n Ah, ye... You don't know how. Enter \"!help\"");
            Thread.Sleep(2000);
            InputProcessing(1);
            Console.WriteLine(
                "ok, now let's try !use. attack enemy with you Lightning Staff(start with !use)");
            InputProcessing(2);
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
            InputProcessing(2);
            Console.WriteLine("Now let's check what you can do! Check your spells(start with !use)");
            _mainPlayer.LearnSpell(new SpellHeal());
            InputProcessing(2);
            Console.WriteLine("Training is ended. Now you can begin you journey");
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
            _enemy.Clear();
            ++_difficultyLevel;
            MoveCounter = 0;
            _mainPlayer.CurrentHealthPoints = _mainPlayer.MaxHealthPoints;
            _mainPlayer.CurrentMana = _mainPlayer.MaxMana;
        }

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

        private void LearnSpellWhenLevelStarts()
        {
            Console.WriteLine("New level starts so you can learn spell: ");
            Thread.Sleep(1000);
            List<Spell> unlearnedSpells = new List<Spell>(_enemyGenerator._allSpells);
            for (int i = 0; i < _mainPlayer._learnedSpells.Count; i++)
                unlearnedSpells.Remove(unlearnedSpells.Find(match =>
                    match.ToString() == _mainPlayer._learnedSpells[i].ToString()));
            for (int i = 0; i < unlearnedSpells.Count; i++)
                Console.WriteLine($"({i + 1}) {unlearnedSpells[i]}");
            int switchIntInput;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out switchIntInput) || switchIntInput < 1 ||
                    switchIntInput > unlearnedSpells.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Gods hate ridicule, you played with fire and lose");
                    _mainPlayer.CurrentHealthPoints -= 50;
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            _mainPlayer.LearnSpell(unlearnedSpells[switchIntInput - 1]);
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
                    Console.BackgroundColor = ConsoleColor.Red;
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

        //this method will generate easy fight situation 1v2 or 1v1
        private void RunEasyLevel()
        {
            LevelStartingMessages("-- EASY LEVEL --", ConsoleColor.Cyan);
            _enemy.Add(_enemyGenerator.Generate(1));
            Console.WriteLine("Say hi to your first enemy - {0}! He is {1}, his age: {2}, ", _enemy[0].Name,
                _enemy[0].CharacterRace, _enemy[0].Age);
            _enemy[0].MaxHealthPoints = 1000;
            Console.WriteLine("By the way, his max health points is {0}", _enemy[0].MaxHealthPoints);
        }
        
        private void RunMediumLevel()
        {
            LevelStartingMessages("-- MEDIUM LEVEL --", ConsoleColor.DarkCyan);
            _enemy.Add(_enemyGenerator.Generate(2));
            Console.WriteLine("Your enemies are:");
            foreach (var t in _enemy)
            {
                Console.WriteLine("{0}. He is {1}, his age: {2}, ", t.Name,
                    t.CharacterRace, t.Age);
                Console.WriteLine("By the way, his max health points is {0}", t.MaxHealthPoints);
            }
        }

        
        private void RunHardLevel()
        {
            LevelStartingMessages("-- HARD LEVEL --", ConsoleColor.DarkBlue);
            _enemy.Add(_enemyGenerator.Generate(3));
            _enemy.Add(_enemyGenerator.Generate(3));
            Console.WriteLine("Your enemies are:");
            foreach (var t in _enemy)
            {
                Console.WriteLine("{0}. He is {1}, his age: {2}, ", t.Name,
                    t.CharacterRace, t.Age);
                _enemy[0].MaxHealthPoints = 1000;
                Console.WriteLine("By the way, his max health points is {0}", t.MaxHealthPoints);
            }
        }

        //final plot will be lineal as the training level i bet
        private void RunFinalPlot()
        {
            LevelStartingMessages("-- FINAL --", ConsoleColor.Red);
            Console.WriteLine("Final level will suddenly appear if our team will get 10 as a game project mark :)");
            ++_difficultyLevel;
        }

        //this method will be used to generate artefacts for bots and at the level start
        public bool FinalLevelComplete()
        {
            return _difficultyLevel == 5;
        }

        //use it after all made a move
        public void UpdateMoveCounters()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveCounter -= 1;
            }

            _mainPlayer.MoveCounter -= 1;
        }

        public void EnemyMove(Wizard enemy)
        {
            if (enemy.CharacterState == State.Dead)
            {
                return;
            }

            Wizard inDangerTarget = enemy;
            Wizard lowerManaTarget = enemy;

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

                    if (wizard.CurrentMana < lowerManaTarget.CurrentMana)
                    {
                        lowerManaTarget = wizard;
                    }
                }
            }

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

            if (inDangerTarget.CurrentHealthPoints < inDangerTarget.MaxHealthPoints / 4 && enemy.CurrentMana > enemy.MaxMana / 3)
            {
                TryCastSpell(new SpellHeal(), enemy, inDangerTarget);
                return;
            }
            
            Artefact outFromFunc;
            if (inDangerTarget.CurrentHealthPoints < inDangerTarget.MaxHealthPoints / 4 &&
                enemy.HasWaterBottle(true, out outFromFunc))
            {
                enemy.UseArtefact(outFromFunc, inDangerTarget);
                return;
            }

            if (lowerManaTarget.CurrentMana < lowerManaTarget.MaxMana / 4 &&
                enemy.HasWaterBottle(false, out outFromFunc))
            {
                enemy.UseArtefact(outFromFunc, lowerManaTarget);
                return;
            }

            if (enemy.CurrentMana > enemy.MaxMana * 8 / 10)
            {
                if (TryCastSpell(new SpellArmor(), enemy, enemy))
                {
                    return;
                }
            }

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

            if (_mainPlayer.CharacterState != State.Dead &&
                (_mainPlayer.CharacterState is State.Healthy or State.Weakened) &&
                enemy.HasStatusArtefact(out outFromFunc))
            {
                enemy.UseArtefact(outFromFunc, _mainPlayer);
                return;
            }

            if (enemy._learnedSpells.FindIndex(spell => spell == new SpellFireball()) != -1 &&
                enemy.CurrentMana > enemy.MaxMana / 4)
            {
                enemy.CastSpell(new SpellFireball(), _mainPlayer, enemy.CurrentMana / 4);
            }

            enemy.UseArtefact(enemy._inventory[0], _mainPlayer);
        }

        private bool TryCastSpell(Spell spell, Wizard origin, Wizard target)
        {
            var usedItemIndex = origin._learnedSpells.FindIndex(targetSpell => targetSpell == spell);
            if (usedItemIndex != -1)
            {
                if (spell == new SpellHeal())
                {
                    origin.CastSpell(origin._learnedSpells[usedItemIndex], target, origin.CurrentMana / 3);
                }
                else
                {
                    origin.CastSpell(origin._learnedSpells[usedItemIndex], target);
                }
                return true;
            }

            return false;
        }

        private bool TryUseArtefact(Artefact artefact, Wizard origin, Wizard target)
        {
            var usedItemIndex = origin._inventory.FindIndex(targetArtefact => targetArtefact == artefact);
            if (usedItemIndex != -1)
            {
                origin.UseArtefact(origin._inventory[usedItemIndex], target);
                return true;
            }

            return false;
        }

        private void ShowFightInfo()
        {
            Console.WriteLine("Your HP: {0}/{1}\nYour MP: {2}/{3}\nEnemy's HP:", _mainPlayer.CurrentHealthPoints,
                               _mainPlayer.MaxHealthPoints, _mainPlayer.CurrentMana, _mainPlayer.MaxMana);
            for (int i = 0; i < _enemy.Count; i++)
            {
                Console.WriteLine("{0}: {1}/{2}", _enemy[i].Name, _enemy[i].CurrentHealthPoints,
                    _enemy[i].MaxHealthPoints);
            }
        }
    }
}
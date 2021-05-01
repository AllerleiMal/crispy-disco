using System;
using System.Collections.Generic;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;

namespace OurCoolGame
{
    public class Character : IComparable
    {
        private static int _objectId;
        public int ID { get; private set; }
        public string Name { get; private set; }
        private State _characterState;

        public State CharacterState // getter/setter for character's state
        {
            get => _characterState;
            set
            {
                if (CharacterState == State.Invulnerable)
                {
                    if (MoveCounter != 0)
                    {
                        return;
                    }
                }
                
                switch (value)
                {
                    case State.Dead or State.Healthy or State.Weakened:
                    {
                        _characterState = value;
                        return;
                    }
                    case State.Paralyzed:
                    {
                        CanMove = false;
                        CanTalk = false;
                        break;
                    }
                    case State.Poisoned:
                    {
                        CanTalk = false;
                        break;
                    }
                    case State.Sick:
                    {
                        CanMove = false;
                        break;
                    }
                }
                
                _characterState = value;
                MoveCounter = 3;
            }
        }

        public bool CanTalk { get; set; } = true;
        public bool CanMove { get; set; } = true;
        public Race CharacterRace { get; set; }
        public Gender CharacterGender { get; private set; }
        public int Age { get; set; }
        private int _currentHealthPoints;

        public int CurrentHealthPoints // getter/setter for character's current health points
        {
            get => _currentHealthPoints;
            set
            {
                if (CharacterState == State.Invulnerable && _currentHealthPoints > value)
                {
                    return;
                }

                _currentHealthPoints = value;
                if (_currentHealthPoints > MaxHealthPoints)
                {
                    _currentHealthPoints = MaxHealthPoints;
                }

                if (_currentHealthPoints > 0)
                {
                    return;
                }

                _currentHealthPoints = 0;
                CharacterState = State.Dead;
            }
        }

        private int _maxHealthPoints;

        public int MaxHealthPoints  // getter/setter for character's maximum health points
        {
            get => _maxHealthPoints;
            set
            {
                _maxHealthPoints = value;
                if (CurrentHealthPoints > value)
                {
                    CurrentHealthPoints = value;
                }
            }
        }

        public int ExperiencePoints { get; set; } = 0;
        private int _moveCounter;

        public int MoveCounter // getter/setter for move counter. Is used for state updates and interactions
        {
            get => _moveCounter;
            set
            {
                _moveCounter = value;

                if (CharacterState is State.Poisoned or State.Sick)
                {
                    CurrentHealthPoints -= 20;
                }

                if (_moveCounter == 0)
                {
                    StateUpdate();
                }
                
                if (_moveCounter < 0)
                {
                    _moveCounter = 0;
                }
            }
        }

        public List<Artefact> _inventory;

        public Character(string name, Race characterRace, Gender characterGender, int age) //character constructor
        {
            _moveCounter = 0;
            ID = _objectId;
            ++_objectId;
            Name = name;
            CharacterRace = characterRace;
            CharacterGender = characterGender;
            Age = age;
            switch (characterRace)  //sets max HP according to character's race
            {
                case Race.Elf:
                {
                    MaxHealthPoints = 1500;
                    break;
                }
                case Race.Gnome:
                {
                    MaxHealthPoints = 2000;
                    break;
                }
                case Race.Goblin:
                {
                    MaxHealthPoints = 1000;
                    break;
                }
                case Race.Human:
                {
                    MaxHealthPoints = 2100;
                    break;
                }
                case Race.Orc:
                {
                    MaxHealthPoints = 3000;
                    break;
                }
                default:
                {
                    Console.WriteLine("You need to add new race to constructor(heath points switch-case)");
                    break;
                }
            }

            CurrentHealthPoints = MaxHealthPoints;
            _inventory = new List<Artefact>();
        }

        public bool IsBetter(Character lhs) 
        {
            return ExperiencePoints > lhs.ExperiencePoints;
        }

        private void StateUpdate()  //updates state according to current HP
        {
            var healthPercentage = (double) CurrentHealthPoints * 100 / MaxHealthPoints;
            if (healthPercentage - 10 > double.Epsilon)
            {
                CharacterState = State.Healthy;
                CanMove = true;
                CanTalk = true;
            }

            if (healthPercentage - 10 < double.Epsilon)
            {
                CanTalk = true;
                CanMove = false;
                CharacterState = State.Weakened;
            }

            if (healthPercentage < double.Epsilon)
            {
                CanMove = false;
                CanTalk = false;
                CharacterState = State.Dead;
            }

            if (CharacterState == State.Dead)
            {
                CurrentHealthPoints = 0;
            }
        }

        public override string ToString()
        {
            var characterInfo = "";
            characterInfo += "ID: " + ID + ", name: " + Name + ", race: " + CharacterRace + ", age: " + Age +
                             ", state: " + CharacterState + ", HP: " + CurrentHealthPoints + ", maximum HP: " +
                             MaxHealthPoints + ", XP: " + ExperiencePoints;
            return characterInfo;
        }

        public void Cure()  //changes Sick state to Healthy/Weakened
        {
            if (CharacterState != State.Sick)
            {
                return;
            }

            StateUpdate();
        }

        public void PickUpArtefact(Artefact artefact) //add artefact to characters inventory
        {
            _inventory.Add(artefact);
            Console.WriteLine("Artefact {0} was added to the inventory of {1}", artefact, Name);
        }

        public void ThrowAwayArtefact(Artefact artefact) //remove artefact from characters inventory
        {
            if (_inventory.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("You can't throw away anything, while your inventory is empty");
                Console.ResetColor();
                return;
            }

            _inventory.Remove(artefact);
        }

        public void GiveArtefact(Character target, Artefact artefact) //remove artefact from inventory and add it to targets inventory
        {
            ThrowAwayArtefact(artefact);
            target.PickUpArtefact(artefact);
        }

        public void UseArtefact(Artefact artefact, Wizard target) //obviously runs Artefact.UseArtefact method and throws away artefact if it's not reusable
        {
            if (!artefact.Renewability)
            {
                ThrowAwayArtefact(artefact);
            }
            
            Console.WriteLine("Artefact {0} was used by {1} on {2}", artefact, Name, target.Name);
            artefact.UseArtefact(target);
            ExperiencePoints += 100;
        }

        public void ShowInventory()  //write list of character's artefacts
        {
            for (var i = 0; i < _inventory.Count; ++i)
            {
                Console.WriteLine("({0}) {1}", i + 1, _inventory[i]);
            }
        }

        public bool HasWaterBottle(bool isLiving, out Artefact outputArtefact) //checks if character has a bottle of living/dead water. used in GameLogic.EnemyMove method
        {
            Tuple<Artefact, Artefact, Artefact> variants = isLiving switch
            {
                true => new Tuple<Artefact, Artefact, Artefact>(new LivingWater(BottleSize.Small),
                    new LivingWater(BottleSize.Medium), new LivingWater(BottleSize.Big)),
                false => new Tuple<Artefact, Artefact, Artefact>(new DeadWater(BottleSize.Small),
                    new DeadWater(BottleSize.Medium), new DeadWater(BottleSize.Big))
            };

            int foundIndex = _inventory.FindIndex(artefact =>
                artefact.ToString() == variants.Item1.ToString() || artefact.ToString() == variants.Item2.ToString() || artefact.ToString() == variants.Item3.ToString());
            outputArtefact = foundIndex != -1 ? _inventory[foundIndex] : null;
            return foundIndex != -1;
        }

        public bool HasStatusArtefact(out Artefact outputArtefact) //checks if character has an artifact that changes opponent's state. used in GameLogic.EnemyMove method
        {
            var artefactIndex = _inventory.FindIndex(artefact => artefact.ToString() == new BasiliskEye().ToString());
            if (artefactIndex != -1)
            {
                outputArtefact = _inventory[artefactIndex];
                return true;
            }

            artefactIndex = _inventory.FindIndex(artefact => artefact.ToString() == new PoisonousSaliva().ToString());
            if (artefactIndex != -1)
            {
                outputArtefact = _inventory[artefactIndex];
                return true;
            }

            outputArtefact = null;
            return false;
        }
    }
}
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
        public State CharacterState { get; set; } = State.Healthy;
        public bool CanTalk { get; set; } = true;
        public bool CanMove { get; set; } = true;
        public Race CharacterRace { get; set; }
        public Gender CharacterGender { get; private set; }
        public int Age { get; set; }
        public int CurrentHealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public int ExperiencePoints { get; set; } = 0;
        public List<Artefact> _inventory;

        public Character(string name, Race characterRace, Gender characterGender, int age)
        {
            ID = _objectId;
            ++_objectId;
            Name = name;
            CharacterRace = characterRace;
            CharacterGender = characterGender;
            Age = age;
            switch (characterRace)
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
            //there we can create new enum to show situation when 2 characters are equal by xp
            return ExperiencePoints > lhs.ExperiencePoints;
        }

        //this method must be changed when such states as poisoned and paralyzed
        private void StateUpdate()
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
                CharacterState = State.Weakened;
            }
            
            if (healthPercentage < double.Epsilon)
            {
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

        public void Cure()
        {
            if (CharacterState != State.Sick)
            {
                return;
            }
            StateUpdate();
        }

        public void PickUpArtefact(Artefact artefact)
        {
            _inventory.Add(artefact);
        }

        public void ThrowAwayArtefact(Artefact artefact)
        {
            if (_inventory.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("You can't throw away anything, while your inventory is empty");
                Console.ResetColor();
            }
            // var index = _inventory.FindIndex(0, target => artefact == target);
            // check how it works with different fields of one artefact
            _inventory.Remove(artefact);
        }

        public void GiveArtefact(Character target, Artefact artefact)
        {
            _inventory.Remove(artefact);
            target.PickUpArtefact(artefact);
        }

        public void UseArtefact(Artefact artefact, Character target)
        {
            if (!artefact.Renewability)
            {
                ThrowAwayArtefact(artefact);
            }
            // if (!typeof(T).IsSubclassOf(typeof(Artefact)))
            // {
            //     //exception or message
            //     return;
            // }
            artefact.UseArtefact((Wizard)this, (Wizard)target);
        }
    }
}
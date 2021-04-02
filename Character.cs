using System;
using System.Data;
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
                    throw new Exception("You need to add new race to constructor(heath points switch-case)");
                }
            }

            CurrentHealthPoints = MaxHealthPoints;
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
    }
}
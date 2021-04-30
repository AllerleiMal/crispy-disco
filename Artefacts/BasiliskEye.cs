using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class BasiliskEye : Artefact
    {
        public BasiliskEye() : base(250)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your enemy is already dead, this artefact didn't do anything, but spoiled artefact");
                Console.ResetColor();
            }

            target.CharacterState = State.Paralyzed;
        }

        public override string ToString()
        {
            return "basilisk eye";
        }
    }
}
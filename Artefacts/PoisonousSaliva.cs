using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class PoisonousSaliva : Artefact
    {
        public PoisonousSaliva() : base(200)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are already dead, this artefact didn't do anything. You have a strange acid taste in your mouth.");
                Console.ResetColor();
                return;
            }
            target.CharacterState = State.Poisoned;
            target.CurrentHealthPoints -= ArtefactPower;
        }
        
        public override string ToString()
        {
            return "poisonous salvina";
        }
    }
}
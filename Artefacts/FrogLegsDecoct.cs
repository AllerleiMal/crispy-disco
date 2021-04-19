using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class FrogLegsDecoct : Artefact
    {
        public FrogLegsDecoct() : base(200)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This character is already dead, this artefact didn't do anything. Local frogs look at you very suspiciously!");
                Console.ResetColor();
                return;
            }
            target.Cure();
            target.CurrentHealthPoints += ArtefactPower;
        }
        public override string ToString()
        {
            return "FrogLegsDecoct";
        }
    }
}
using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class DeadWater : Artefact
    {
        public DeadWater(BottleSize size) : base((int)size)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are already dead, this artefact didn't do anything, but a puddle of odd-smelling liquid");
                Console.ResetColor();
                return;
            }
            target.CurMana += ArtefactPower;
        }
        public override string ToString()
        {
            return "bottle of dead water";
        }
    }
}
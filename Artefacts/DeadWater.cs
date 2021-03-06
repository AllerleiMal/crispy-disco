using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class DeadWater : Artefact
    {
        public DeadWater(BottleSize size) : base((int) size)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard target) //add ArtefactPower equal amount of mana to target (ArtefactPower depends on BottleSize)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Your enemy is already dead, this artefact didn't do anything, but a puddle of odd-smelling liquid");
                Console.ResetColor();
                return;
            }

            target.CurrentMana += ArtefactPower;
        }

        public override string ToString()
        {
            return ArtefactPower switch
            {
                (int) BottleSize.Big => "big bottle of dead water",
                (int) BottleSize.Medium => "medium bottle of dead water",
                (int) BottleSize.Small => "small bottle of dead water",
                _ => "small bottle of dead water"
            };
        }
    }
}
using OurCoolGame.Enums;
using System;

namespace OurCoolGame.Artefacts
{
    public class LivingWater : Artefact
    {
        public LivingWater(BottleSize size) : base((int) size)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard target)//add ArtefactPower equal amount of HP to target (ArtefactPower depends on BottleSize)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "This character is already dead, this artefact didn't do anything. But flowers are growing around you now.");
                Console.ResetColor();
                return;
            }

            target.CurrentHealthPoints += ArtefactPower;
        }

        public override string ToString()
        {
            return ArtefactPower switch
            {
                (int) BottleSize.Big => "big bottle of living water",
                (int) BottleSize.Medium => "medium bottle of living water",
                (int) BottleSize.Small => "small bottle of living water",
                _ => "small bottle of living water"
            };
        }
    }
}
using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class ShadowDagger : Artefact
    {
        public ShadowDagger() : base(120)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Your enemy is already dead, this artefact didn't do anything. Unless enemy has one rib less");
                Console.ResetColor();
                return;
            }

            target.CurrentHealthPoints -= ArtefactPower;
            ArtefactPower = _random.Next(100, 150);
        }

        public override string ToString()
        {
            return "ShadowDagger";
        }
    }
}
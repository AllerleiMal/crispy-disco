using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class LightningStaff : Artefact
    {
        public LightningStaff() : base(225)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard target) //deal 150-300 damage to target
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Your enemy is already dead, this artefact didn't do anything. But a very strange thundercloud appeared");
                Console.ResetColor();
                return;
            }

            target.CurrentHealthPoints -= ArtefactPower;
            ArtefactPower = _random.Next(150, 300);
        }

        public override string ToString()
        {
            return "lightning staff";
        }
    }
}
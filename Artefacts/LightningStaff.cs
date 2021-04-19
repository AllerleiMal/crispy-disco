using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class LightningStaff : Artefact
    {
        public LightningStaff() : base(175)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your enemy is already dead, this artefact didn't do anything. But a very strange thundercloud appeared");
                Console.ResetColor();
                return;
            }
            target.CurrentHealthPoints -= ArtefactPower;
            ArtefactPower = _random.Next(150, 200);
            target.CharacterState = State.Sick;
        }
        
        public override string ToString()
        {
            return "lightning staff";
        }
    }
}
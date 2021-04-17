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
                Console.WriteLine("You are already dead, this artefact didn't do anything. But a very strange thundercloud appeared");
                Console.ResetColor();
                return;
            }
            target.CurrentHealthPoints -= ArtefactPower;
            
            //delete if we decide so
            ArtefactPower = _random.Next(150, 200);
        }
        
        public override string ToString()
        {
            return "lightning staff";
        }
    }
}
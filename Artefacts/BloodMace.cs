﻿using System;
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class BloodMace : Artefact
    {
        public BloodMace() : base(200)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard target)
        {
            if (target.CharacterState == State.Dead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are already dead, this artefact didn't do anything, but more blood");
                Console.ResetColor();
            }
            target.CurrentHealthPoints -= ArtefactPower;
            ArtefactPower = _random.Next(180, 250);
        }

        public override string ToString()
        {
            return "bloodMace";
        }
    }
}
using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class BasiliskEye : Artefact
    {
        public BasiliskEye(int artefactPower) : base(artefactPower)
        {
            Renewability = false;
        }

        protected override void UseArtefact(Wizard origin, Wizard target)
        {
            if (target.CharacterState != State.Dead)
            {
                target.CharacterState = State.Paralyzed;
            }
        }
    }
}
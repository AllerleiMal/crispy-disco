using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class PoisonousSaliva : Artefact
    {
        public PoisonousSaliva(int artefactPower) : base(artefactPower)
        {
            Renewability = true;
        }

        protected override void UseArtefact(Wizard origin, Wizard target)
        {
            target.CharacterState = State.Poisoned;
            target.CurrentHealthPoints -= ArtefactPower;
        }
    }
}
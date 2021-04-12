using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class PoisonousSaliva : Artefact
    {
        public PoisonousSaliva(int artefactPower) : base(artefactPower)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard origin, Wizard target)
        {
            target.CharacterState = State.Poisoned;
            target.CurrentHealthPoints -= ArtefactPower;
        }
        
        public override string ToString()
        {
            return "poisonous salvina";
        }
    }
}
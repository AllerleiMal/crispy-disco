namespace OurCoolGame.Artefacts
{
    public class FrogLegsDecoct : Artefact
    {
        public FrogLegsDecoct(int artefactPower) : base(artefactPower)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard origin, Wizard target = null)
        {
            if (target == null)
            {
                origin.Cure();
            }
            else
            {
                target.Cure();
            }
        }
        public override string ToString()
        {
            return "frog legs decoct";
        }
    }
}
namespace OurCoolGame.Artefacts
{
    public class FrogLegsDecoct : Artefact
    {
        public FrogLegsDecoct(int artefactPower) : base(artefactPower)
        {
            Renewability = false;
        }

        protected override void UseArtefact(Wizard origin, Wizard target = null)
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
    }
}
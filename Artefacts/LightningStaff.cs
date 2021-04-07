namespace OurCoolGame.Artefacts
{
    public class LightningStaff : Artefact
    {
        public LightningStaff(int artefactPower) : base(artefactPower)
        {
            Renewability = true;
        }

        protected override void UseArtefact(Wizard origin, Wizard target)
        {
            target.CurrentHealthPoints -= ArtefactPower;
        }
    }
}
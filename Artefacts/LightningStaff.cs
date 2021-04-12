namespace OurCoolGame.Artefacts
{
    public class LightningStaff : Artefact
    {
        public LightningStaff(int artefactPower) : base(artefactPower)
        {
            Renewability = true;
        }

        public override void UseArtefact(Wizard origin, Wizard target)
        {
            target.CurrentHealthPoints -= ArtefactPower;
        }
        
        public override string ToString()
        {
            return "lightning staff";
        }
    }
}
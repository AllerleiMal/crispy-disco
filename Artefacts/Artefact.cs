namespace OurCoolGame.Artefacts
{
    public abstract class Artefact
    {
        public bool Renewability { get; set; }
        public int ArtefactPower { get; set; }

        protected Artefact(int artefactPower)
        {
            ArtefactPower = artefactPower;
        }

        protected abstract void UseArtefact(Wizard origin, Wizard target = null);
    }
}
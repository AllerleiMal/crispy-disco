namespace OurCoolGame.Artefacts
{
    public interface IArtefactUsage
    {
        public void UseArtefact(Wizard origin, Wizard target = null);
    }
}
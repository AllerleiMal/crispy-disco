using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class LivingWater : Artefact
    {
        public LivingWater(BottleSize size) : base((int)size)
        {
            Renewability = false;
        }

        protected override void UseArtefact(Wizard origin, Wizard target = null)
        {
            origin.CurrentHealthPoints += ArtefactPower;
        }
    }
}
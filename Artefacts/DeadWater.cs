using OurCoolGame.Enums;

namespace OurCoolGame.Artefacts
{
    public class DeadWater : Artefact
    {
        public DeadWater(BottleSize size) : base((int)size)
        {
            Renewability = false;
        }

        public override void UseArtefact(Wizard origin, Wizard target = null)
        {
            origin.CurMana += ArtefactPower;
        }
        public override string ToString()
        {
            return "bottle of dead water";
        }
    }
}
namespace OurCoolGame.Spells
{
    interface IMagic
    {
        public void MagicEffect(Wizard origin, Wizard target = null, int magicPower = 0);
        public void MagicEffect(Wizard origin, Wizard target);
        public void MagicEffect(Wizard origin, int magicPower);
    }
}

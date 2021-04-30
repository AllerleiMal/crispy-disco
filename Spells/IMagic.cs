namespace OurCoolGame.Spells
{
    interface IMagic
    {
        public void MagicEffect(Wizard origin, Wizard target, int magicPower);
        public void MagicEffect(Wizard origin, Wizard target);
        public void MagicEffect(Wizard origin, int magicPower);
        public void MagicEffect(Wizard origin);
    }
}
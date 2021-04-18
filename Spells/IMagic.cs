namespace OurCoolGame.Spells
{
    interface IMagic
    {
        public void MagicEffect(Wizard origin, Character target, int magicPower);
        public void MagicEffect(Wizard origin, Character target);
        public void MagicEffect(Wizard origin, int magicPower);
        public void MagicEffect(Wizard origin);
    }
}

using System;

namespace OurCoolGame
{
    interface IMagic
    {
        public void MagicEffect(Character character, int magicPower);
        public void MagicEffect(Character character);
        public void MagicEffect(int magicPower);
        public void MagicEffect();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Text;

namespace Our_cool_game
{
    class Character
    {
        //public uint ID  //должно быть уникальным
        public string Name { get; private set; }
        //public enum state { get; set; }
        public bool CanTalk { get; set; }
        public bool CanMove { get; set; }
        //public enum race { get; private set; }
        //public enum gender { get; private set; }
        public uint Age { get; set; }
        public uint CurrentHP { get; set; }
        public uint MaxHP { get; set; }
        public uint XP { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace OurCoolGame.Artefacts
{
    public abstract class Artefact : IArtefactUsage
    {
        //we need renewability to know delete artefact after using or not
        public bool Renewability { get; set; }
        //power is equivalent of how much damage will be deal or how much hp it will restore
        protected int ArtefactPower { get; set; }
        //this field we use to randomize artefact power for some non-renewable artefacts
        protected readonly Random _random;

        protected Artefact(int artefactPower)
        {
            ArtefactPower = artefactPower;
            _random = new Random();
        }
        //override ToString is used to output information about artefact usage
        public override string ToString()
        {
            return "artefact";
        }

        //UseArtefact - method that is called when any character use some artefact. Its implementation is in inheritor classes
        public abstract void UseArtefact(Wizard target);

        //this is a comparer, which gives us opportunity to compare artefacts in inventory
        private sealed class RenewabilityArtefactPowerEqualityComparer : IEqualityComparer<Artefact>
        {
            public bool Equals(Artefact x, Artefact y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Renewability == y.Renewability && x.ArtefactPower == y.ArtefactPower;
            }

            public int GetHashCode(Artefact obj)
            {
                return HashCode.Combine(obj.Renewability, obj.ArtefactPower);
            }
        }

        public static IEqualityComparer<Artefact> RenewabilityArtefactPowerComparer { get; } =
            new RenewabilityArtefactPowerEqualityComparer();
    }
}
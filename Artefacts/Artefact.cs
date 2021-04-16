using System;
using System.Collections.Generic;

namespace OurCoolGame.Artefacts
{
    public abstract class Artefact : IArtefactUsage
    {
        public bool Renewability { get; set; }
        public int ArtefactPower { get; set; }

        protected Artefact(int artefactPower)
        {
            ArtefactPower = artefactPower;
        }

        public override string ToString()
        {
            return "artefact";
        }

        public virtual void UseArtefact(Wizard origin, Wizard target = null)
        {
            throw new NotImplementedException();
        }

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

       public static IEqualityComparer<Artefact> RenewabilityArtefactPowerComparer { get; } = new RenewabilityArtefactPowerEqualityComparer();
    }
}
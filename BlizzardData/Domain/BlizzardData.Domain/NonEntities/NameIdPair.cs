using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BlizzardData.Domain.NonEntities
{
    public abstract class NameIdPair : IEqualityComparer<NameIdPair>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals([AllowNull] NameIdPair x, [AllowNull] NameIdPair y)
        {
            if ((object)x == null && (object)y == null)
            {
                return true;
            }
            if ((object)x == null || (object)y == null)
            {
                return false;
            }
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] NameIdPair obj)
        {
            return (obj.Id.ToString() + obj.Name).GetHashCode();
        }
    }
}

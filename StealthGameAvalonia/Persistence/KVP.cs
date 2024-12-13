using System.Collections.Generic;

namespace StealthGameAvalonia.Persistence
{
    public class KVP<T, TT>(T key, TT value)
    {
        public T Key { get; set; } = key;
        public TT Value { get; set; } = value;

        public override bool Equals(object? obj)
        {
            return obj is KVP<T, TT> kVP &&
                   EqualityComparer<T>.Default.Equals(Key, kVP.Key);
        }

        public override int GetHashCode()
        {
            return object.Equals(Key, default(T)) ? -1 : Key!.GetHashCode();
        }
    }
}

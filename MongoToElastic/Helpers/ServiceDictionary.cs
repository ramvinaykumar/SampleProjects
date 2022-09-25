namespace MongoToElastic.Helpers
{
    public class ServiceDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var value))
                {
                    return value;
                }

                return default(TValue);
            }
            set
            {
                base[key] = value;
            }
        }
    }
}

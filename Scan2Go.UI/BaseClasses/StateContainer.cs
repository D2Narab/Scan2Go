namespace Scan2Go.UI.BaseClasses
{
    public class StateContainer
    {
        public readonly Dictionary<int, object> ObjectTunnel = new();
    }

    public static class StateContainerExtensions
    {
        public static int AddRoutingObjectParameter(this StateContainer stateContainer, object value)
        {
            stateContainer.ObjectTunnel[value.GetHashCode()] = value;
            return value.GetHashCode();
        }

        public static T GetRoutingObjectParameter<T>(this StateContainer stateContainer, int hashCode)
        {
            return (T)stateContainer.ObjectTunnel.PopValue(hashCode);
        }
    }

    public static class DictionaryExtensions
    {
        public static TValue PopValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey keyName) where TKey : notnull
        {
            var value = dictionary[keyName];
            dictionary.Remove(keyName);
            return value;
        }
    }
}

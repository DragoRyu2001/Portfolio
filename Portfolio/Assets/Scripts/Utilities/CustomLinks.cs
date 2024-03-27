using UnityEngine.Serialization;

namespace Utilities
{
    [System.Serializable]
    public struct CustomLinks
    {
        [FormerlySerializedAs("title")] public string Title;
        [FormerlySerializedAs("url")] public string URL;

    }
}

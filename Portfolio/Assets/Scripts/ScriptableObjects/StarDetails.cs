using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="StarDetail", menuName ="ScriptableObjects/Star")]
    public class StarDetails : ScriptableObject
    {
        [FormerlySerializedAs("starColor")] public Color StarColor;
        [FormerlySerializedAs("title")] public string Title;
        [FormerlySerializedAs("date")] public CustomCalendar Date;
        [FormerlySerializedAs("titleImage")] public Sprite TitleImage;
        [FormerlySerializedAs("context")] [TextArea]
        public string Context;
        [FormerlySerializedAs("links")] public List<CustomLinks> Links;
        [FormerlySerializedAs("subCategories")] public List<StarDetails> SubCategories;
    }
}

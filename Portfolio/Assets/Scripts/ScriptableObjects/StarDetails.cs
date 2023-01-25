using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="StarDetail", menuName ="ScriptableObjects/Star")]
public class StarDetails : ScriptableObject
{
    public Color starColor;
    public string title;
    public CustomCalendar date;
    public Sprite titleImage;
    [TextArea]
    public string context;
    public List<CustomLinks> links;
    public List<StarDetails> subCategories;
}

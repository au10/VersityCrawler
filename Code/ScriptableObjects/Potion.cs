using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion")]
public class Potion : Item
{
    public PotionType potionType;

    [Serializable]
    public enum PotionType
    {
        Health,
        Speed,
        Strength,
        Invisibility
    }
}

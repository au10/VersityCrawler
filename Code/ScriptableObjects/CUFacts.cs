using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CUFact", menuName = "CU Facts")]
public class CUFacts : ScriptableObject
{
    public short factNum;
    [TextArea]
    public string factInfo;
    public Sprite factImage;
}

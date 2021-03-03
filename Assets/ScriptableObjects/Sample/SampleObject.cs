using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Sample")]
public class SampleObject : ScriptableObject
{
    public string sampleName;
    public float bps = 120f;

    public CTInstrument instrument;

    public Koreography koreography;

    //TODO: auto gen unique ID
    [Header("Never set id to 0! IDS must be unique")]
    public int id;
}

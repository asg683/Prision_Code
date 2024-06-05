using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation Data", menuName = "Scriptable Objects/ Animation Data", order = 1)]

public class AnimationData : ScriptableObject
{
    public static float targetFrameTime = 0.0167f;
    public Sprite[] sprites;
    public int framesOfGap;
    public bool loop;
}

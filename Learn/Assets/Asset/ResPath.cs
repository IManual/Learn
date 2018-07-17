using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResPath
{
    public static string[] GetAnimResPath(string clipName)
    {
        return new string[] { "res/anim", clipName };
    } 
}

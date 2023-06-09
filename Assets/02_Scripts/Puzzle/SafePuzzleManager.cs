using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafePuzzleManager : MonoBehaviour
{
    public SafePuzzle[] safePuzzles;
    public InputField[] inputFields;
    public void CheckSafePuzzle(Collider _collider)
    {
        foreach(SafePuzzle one in safePuzzles)
        {
            if (one.name == _collider.name)
            {
                one.OnSafePuzzle();
            }
        }
    }
}

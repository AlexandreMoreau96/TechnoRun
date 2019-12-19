using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public int seed { get; set; }
}

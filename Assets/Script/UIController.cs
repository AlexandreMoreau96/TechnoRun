using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private InputField inputFieldSeed;
    [SerializeField] private Seed seed;

    public void Play()
    {
        seed.seed = int.Parse(inputFieldSeed.text);
        SceneManager.LoadScene(1);
    }
}

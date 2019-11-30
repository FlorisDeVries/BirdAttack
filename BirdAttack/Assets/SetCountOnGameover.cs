using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCountOnGameover : MonoBehaviour
{
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        GameManager.Instance.OnGameOver.AddListener(() => _text.text = WaveSpawner.Instance.WaveCounter.ToString());
    }
}

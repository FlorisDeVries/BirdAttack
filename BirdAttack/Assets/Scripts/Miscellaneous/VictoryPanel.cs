using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField]
    private Text _waveCounter;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnVictory.AddListener(OnGameOver);
        this.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(InitiazeValues());
    }

    IEnumerator InitiazeValues(){
        yield return new WaitForFixedUpdate();
        _waveCounter.text = WaveSpawner.Instance.WaveCounter.ToString();
    }
}
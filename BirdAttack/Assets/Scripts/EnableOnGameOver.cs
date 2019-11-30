using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameOver.AddListener(() => this.gameObject.SetActive(true));
        this.gameObject.SetActive(false);
    }
}

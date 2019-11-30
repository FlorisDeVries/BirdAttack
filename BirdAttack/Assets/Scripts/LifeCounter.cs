using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    private Text _text;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        GameManager.Instance.OnHit.AddListener(UpdateText);
        _text.text = GameManager.Instance.Lives.ToString();
    }

    private void UpdateText(float damage)
    {
        _text.text = GameManager.Instance.Lives.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMoney : MonoBehaviour
{
    private Text _text;
    private Shadow _shadow;
    private float _shadowAlpha;

    private float _id = 0;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _shadow = GetComponent<Shadow>();
        ResetColors();
        UpdateMoney();
        CurrencyManager.Instance.OnMoney.AddListener(UpdateMoney);
        BuildManager.Instance.OnErrorBuilding.AddListener((x) => StartCoroutine(BlinkRed(x)));
    }

    private void UpdateMoney()
    {
        _text.text = CurrencyManager.Instance.Bank.ToString();
    }

    private IEnumerator BlinkRed(float duration)
    {
        _id++;
        float value = _id;
        _text.color = ColorManager.Instance.Red;
        _shadow.effectColor = ColorManager.Instance.DarkRed;
        yield return new WaitForSeconds(duration);
        if(_id == value){
            _id = 0;
            ResetColors();
        }
    }

    private void ResetColors()
    {
        _shadowAlpha = _shadow.effectColor.a;
        _text.color = ColorManager.Instance.Green;
        Color newColor = ColorManager.Instance.DarkGreen;
        newColor.a = _shadowAlpha;
        _shadow.effectColor = newColor;
    }
}
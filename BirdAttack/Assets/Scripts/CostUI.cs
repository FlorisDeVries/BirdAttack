using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    [SerializeField]
    private Text _title;

    private Text _text;
    private Shadow _shadow;
    private float _shadowAlpha;

    private Color _shadowColor, _textColor;

    private float _id = 0;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _shadow = GetComponent<Shadow>();
        _shadowColor = _shadow.effectColor;
        _textColor = _text.color;
        UpdateCost();
        BuildManager.Instance.OnSelect.AddListener(UpdateCost);
        BuildManager.Instance.OnErrorBuilding.AddListener((x) => StartCoroutine(BlinkRed(x)));
    }

    private void UpdateCost()
    {
        if (BuildManager.Instance.ToBuild)
        {
            _title.enabled = true;
            _text.enabled = true;
            _text.text = BuildManager.Instance.ToBuild.GetComponent<Turret>().Cost.ToString();
        }
        else
        {
            _title.enabled = false;
            _text.enabled = false;
        }
    }

    private IEnumerator BlinkRed(float duration)
    {
        _id++;
        float value = _id;
        _text.color = ColorManager.Instance.Red;
        _shadow.effectColor = ColorManager.Instance.DarkRed;
        yield return new WaitForSeconds(duration);
        if (_id == value)
        {
            _text.color = _textColor;
            _shadow.effectColor = _shadowColor;
            _id = 0;
        }
    }
}
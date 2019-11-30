using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public void OnHit(float damage)
    {
        GameManager.Instance.OnHit.Invoke(damage);
    }
}
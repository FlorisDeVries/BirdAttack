using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMan : Enemy
{
    protected override void Die()
    {
        base.Die();
        GameManager.Instance.OnVictory.Invoke();
    }
}
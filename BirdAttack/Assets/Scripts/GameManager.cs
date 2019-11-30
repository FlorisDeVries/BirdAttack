using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float>
{ }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    [HideInInspector]
    public FloatEvent OnHit = new FloatEvent();
    [HideInInspector]
    public UnityEvent OnGameOver = new UnityEvent();
    [SerializeField]
    private float lives = 10f;
    public float Lives
    {
        get { return lives; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        OnHit.AddListener(Hit);
    }

    public void Hit(float livesLost)
    {
        lives -= livesLost;
        Debug.Log(lives);
        CheckGameOver();
    }

    private bool CheckGameOver()
    {
        if (lives <= 0)
        {
            // Gameover
            OnGameOver.Invoke();
            Debug.Log("You died!!");
            return true;
        }
        else
        {
            return false;
        }
    }
}
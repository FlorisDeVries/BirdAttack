using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Life
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
    }

    [SerializeField]
    private Image _HPBar;
    private float _HPBar_width;
    public float MaxHP = 20;
    private float _healthPoints;

    // Particles
    [SerializeField]
    private ParticleSystem _deathParticles;

    // Movement
    [SerializeField]
    private float _speed = 2f;
    public float Speed
    {
        set { _speed = value; }
    }
    private int _waypointIndex = 0;
    private Transform _currentWaypoint;
    private Quaternion _targetRotation;
    private Vector3 _scale;
    private float _distanceTravelled = 0;
    public float DistanceTravelled
    {
        get { return _distanceTravelled; }
    }

    // Animations
    private Animator _animator;
    [SerializeField]
    private Transform _modelTrans;

    // Damage
    [SerializeField]
    private float _damage = 1;
    public float Reward = 10;
    private Goal _goal;

    private void Start()
    {
        _healthPoints = MaxHP;
        _HPBar_width = _HPBar.rectTransform.rect.width;
        GetWaypoint();
        _scale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);

        _animator = GetComponentInChildren<Animator>();

        Setupanimation();
    }

    private void Setupanimation()
    {
        _animator.GetBehaviour<EndAttack>().Enemy = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive && _currentWaypoint != null)
        {
            // Spawn scale
            if (transform.localScale != _scale)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _scale, 2f * Time.deltaTime);
                if (Vector3.Distance(transform.localScale, _scale) < 0.001f)
                    transform.localScale = _scale;
            }

            // Moving
            Vector3 dir = _currentWaypoint.position - transform.position;
            Debug.DrawLine(_currentWaypoint.position, transform.position, Color.red);
            transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
            _distanceTravelled += _speed * Time.deltaTime;

            // Smooth look at target
            _modelTrans.rotation = Quaternion.Lerp(_modelTrans.rotation, _targetRotation, 5f * Time.deltaTime);

            // When at target, goto next
            if (Vector3.Distance(transform.position, _currentWaypoint.position) <= 0.2f)
            { // Iterate over waypoints list
                if (_waypointIndex < Waypoints.WaypointList.Count - 1)
                {
                    NextWaypoint();
                }
            }
        }
    }

    private void NextWaypoint()
    {
        _waypointIndex++;
        GetWaypoint();
    }

    private void GetWaypoint()
    {
        // Get current waypoint
        _currentWaypoint = Waypoints.WaypointList[_waypointIndex];
        _targetRotation = Quaternion.LookRotation(_currentWaypoint.position - _modelTrans.position);
    }

    public void Hit(float damage)
    {
        // Hit by something that deals damage
        _healthPoints -= damage;
        float value = _healthPoints / MaxHP * _HPBar_width;
        _HPBar.rectTransform.sizeDelta = new Vector2(value, _HPBar.rectTransform.sizeDelta.y);
        if (_healthPoints <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Damn I died
        _isAlive = false;
        GetComponent<CapsuleCollider>().enabled = false;
        _animator.SetTrigger("Die");
        CurrencyManager.Instance.AddToBank(Reward);
        StartCoroutine(Despawn());

        GetComponentInChildren<Canvas>().enabled = false;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2f);
        _scale = Vector3.zero;
        while (Vector3.Distance(_scale, transform.localScale) > 1f)
        {
            Vector3 pos = transform.position;
            pos.y = 0.25f;
            transform.localScale = Vector3.Lerp(transform.localScale, _scale, 2f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, pos, 2f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Instantiate(_deathParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Goal goal = other.GetComponent<Goal>();
        if (goal)
        {
            _currentWaypoint = null;
            _goal = goal;
            _animator.SetTrigger("Attack");
        }
    }

    public void Explode()
    {
        Instantiate(_deathParticles, transform.position + new Vector3(0, .25f, 0), transform.rotation);
        Instantiate(_deathParticles, transform.position - new Vector3(0, .25f, 0), transform.rotation);
        if (_goal)
            _goal.OnHit(_damage);
        Destroy(this.gameObject);
    }

    public void SetHP(float hp)
    {
        MaxHP = hp;
        _healthPoints = hp;
    }
}
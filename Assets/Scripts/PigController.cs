using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public enum PigState
    {
        idle,
        run,
        dead
    }

    #region UI Controlller
    [SerializeField] private ControllerButton _leftButton;
    [SerializeField] private ControllerButton _rightButton;
    #endregion

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public event Action<PigState> OnStateChange;
    public event Action OnScore;

    private float _speed = 2.5f;
    private Rigidbody2D _rb2D;
    private Animator _animator;
    private PigState _state;

    private PigState State { 
        get => _state; 
        set 
        { 
            _state = value;
            OnStateChange?.Invoke(value);
            PlayAnimation(value);
        } 
    }

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        AddEventListener();
    }

    public void Init()
    {
        transform.position = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Collected Object")) return;

        Destroy(collision.gameObject);
        OnScore?.Invoke();
    }

    private void MoveLeft()
    {
        State = PigState.run;
        _spriteRenderer.flipX = false;
        _rb2D.velocity = Vector2.left * _speed;
    }

    private void MoveRight()
    {
        State = PigState.run;
        _spriteRenderer.flipX = true;
        _rb2D.velocity = Vector2.right * _speed;
    }

    private void PlayAnimation(PigState state)
    {
        switch(state)
        {
            case PigState.idle:
                _animator.Play("Pig_Idle");
                break;
            case PigState.run:
                _animator.Play("Pig_Run");
                break;
        }
    }

    private void AddEventListener()
    {
        _leftButton.OnButtonDown += MoveLeft;
        _rightButton.OnButtonDown += MoveRight;

        _leftButton.OnButtonUp += () => State = PigState.idle;
        _rightButton.OnButtonUp += () => State = PigState.idle;
    }
}

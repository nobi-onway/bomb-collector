using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Dead Zone")) return;

        StartCoroutine(ExploseCoroutine());
        GameController.Instance.State = GameController.GameState.over;
    }

    private IEnumerator ExploseCoroutine()
    {
        _animator.Play("Bomb_Explose");

        float secondsForEndOfAnimation = _animator.GetCurrentAnimatorStateInfo(0).length;
        
        yield return new WaitForSeconds(secondsForEndOfAnimation);

        Destroy(gameObject);
    }
}

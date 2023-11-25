using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        _animator.SetTrigger("Bounce");
    }
}

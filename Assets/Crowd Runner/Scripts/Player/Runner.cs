using System;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isTarget;

    public void SetTarget() => isTarget = true;

    public bool IsTarget() => isTarget;

    public Animator GetAnimator() => animator;

    public void SetAnimator(Animator animator) => this.animator = animator;
}

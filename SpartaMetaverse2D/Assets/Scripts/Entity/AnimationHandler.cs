using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");    

    protected Animator animator;
    public KeyCode jumpKey = KeyCode.Space;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            animator.SetTrigger("IsJump");
        }
    }
}

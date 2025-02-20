using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Camera m_Camera;

    SceneChanger sceneChanger;
    
    protected override void Start()
    {
        base.Start();
        m_Camera = Camera.main;
        sceneChanger = SceneChanger.Instance;
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;

        lookDirection = movementDirection.normalized;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneChanger.GoGameLoading();
    }
}

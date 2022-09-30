using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputAction m_controls;

    [SerializeField]
    private float m_speed;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("I'm alive!");
    }

    // Update is called once per frame
    void Update() {
        var movement = m_controls.ReadValue<Vector2>();
        transform.position += movement.V3() * (m_speed * Time.deltaTime);
    }

    private void OnEnable() {
        m_controls.Enable();
    }

    private void OnDisable() {
        m_controls.Disable();
    }
}

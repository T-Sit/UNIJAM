using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        DoMovement();
        DoLevelRotation();
    }

    private void DoLevelRotation()
    {
        throw new NotImplementedException();
    }

    private void DoMovement()
    {
        transform.Translate(CurrentMovement());
    }
    private Vector3 CurrentMovement()
    {
        return Time.deltaTime * DesignSettings.Instance.MoveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    }
}

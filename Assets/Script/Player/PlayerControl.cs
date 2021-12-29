
using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    [SerializeReference]private CharacterController controller;
    [SerializeReference]private float moveSpeed;
    [SerializeReference]private Transform VREyeRig;
    [SerializeReference]private Vector3 rotationOffset;
    [SerializeReference]private Transform EyeConstraint;
    [SerializeReference] private float gravityScale = 1;
    private Transform _eye;
    private const float _gravity = -9.8f;
    private float _gravityVelocity = 0f;
    private void Start()
    {
        if(!isLocalPlayer) return;
        OVRCameraRig rig = FindObjectOfType<OVRCameraRig>();
        _eye = rig.centerEyeAnchor;
        VREyeRig = rig.transform;
        if(isServer&&!NetworkManager.singleton.networkAddress.Equals("localhost")) RequestTable.GetInstance().KeepGamingState();
    }

    private void FixedUpdate()
    {
        if(!isLocalPlayer) return;
        Move();
        Rotate();
    }

    private void Move()
    {
        var controllerT = controller.transform;
        float inputH = Input.GetAxis("Horizontal");    
        float inputV = Input.GetAxis("Vertical");
        
        Vector3 velY = controllerT.forward*inputV;
        Vector3 velX = controllerT.right*inputH;
        Vector3 vel = (velX+velY).normalized;  

        if (!controller.isGrounded)
        {
            _gravityVelocity += gravityScale * _gravity * Time.fixedDeltaTime;
            vel += new Vector3(0, _gravity, 0);
        }
        
        controller.Move(vel*moveSpeed*Time.fixedDeltaTime);
    }

    /// <summary>
    /// 将玩家的移动主方向调整地与视角一致
    /// </summary>
    private void Rotate()
    {
        Vector3 euler = _eye.rotation.eulerAngles+new Vector3(0,0,0);
        euler.x = 0;
        euler.z = 0;
        transform.rotation = Quaternion.Euler(euler)*Quaternion.Euler(rotationOffset);
        VREyeRig.position = EyeConstraint.position;
    }

    private void OnDisable() 
    {
        if (isServer && !NetworkManager.singleton.networkAddress.Equals("localhost")) RequestTable.GetInstance().EndGamingState();
    }

}

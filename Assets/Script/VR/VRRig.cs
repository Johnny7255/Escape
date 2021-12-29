using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
/// <summary>
/// 用于将VR设备与操作的部位进行映射
/// </summary>
public class VRMap
{
    public enum RotationIgnore: int
    {
        None = 0, X = 1, Y = 2, Z = 3, All = 4
    }
    public Transform VRTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public RotationIgnore mapRotation = RotationIgnore.None;
    public bool mapPosition = true;
    public void Map()
    {
       if(mapPosition) rigTarget.position = VRTarget.TransformPoint(trackingPositionOffset);

       int ignoreType = (int) mapRotation; 
       Vector3 euler = VRTarget.transform.rotation.eulerAngles+new Vector3(0,0,0);
       switch(ignoreType)
       {
           case 0: break;
           case 1: 
           {
               euler.x = 0;
               break;
           }
           case 2: 
           {
               euler.y = 0;
               break;
           }
           case 3: 
           {
               euler.z = 0;
               break;
           }
           case 4: return;
       }
       rigTarget.rotation = Quaternion.Euler(euler)*Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap leftArm;
    public VRMap rightArm;
    public VRMap head;

    // Update is called once per frame
    void Update()
    {  
        leftArm.Map();
        rightArm.Map();
        head.Map();
    }
}

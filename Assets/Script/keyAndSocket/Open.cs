using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{   
    public GameObject key;
    protected insert ifInsert;
    protected Transform keyTransform;
    private Vector3 rotatePoint;

    // Start is called before the first frame update
    void Start()
    {
        rotatePoint=new Vector3(-13.59f,0,13.7151f);
        if(key!=null){
            ifInsert=key.GetComponent<insert>();
            keyTransform = key.GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }
    public void rotate(){
        if(ifInsert!=null){
            if(ifInsert.isInsert&&this.transform.localEulerAngles.y<90){
                transform.RotateAround(rotatePoint,Vector3.up,25*Time.deltaTime);
                keyTransform.RotateAround(rotatePoint, Vector3.up, 25 * Time.deltaTime);
            }
        }
    }
}

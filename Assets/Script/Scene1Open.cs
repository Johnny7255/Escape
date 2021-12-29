using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Open : MonoBehaviour
{   
    

    public float delay;
    private float m_timer = 0;

    // Start is called before the first frame update
    void Start()
    {
   
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= delay && this.transform.localEulerAngles.y < 90)
        {

            transform.Rotate(0, 0, 20 * Time.deltaTime);

        }
    }
    public void rotate(){
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayAppear : MonoBehaviour
{

    public GameObject Object;
    IEnumerator ie;
    public float delay;

    // Use this for initialization
    void Start()
    {
        ie = waitFourSeconds();
        StartCoroutine(ie);

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitFourSeconds()
    {
        yield return new WaitForSeconds(delay);
        Object.SetActive(true);
        Destroy(this.gameObject, 2.5f);
    }
}

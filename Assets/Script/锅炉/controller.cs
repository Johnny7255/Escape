using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    private List<string> ingredients;
    // Start is called before the first frame update
    public GameObject effect1;
    public GameObject effect2;
    //public GameObject button;
    //private Animator animator;
    private bool pressed;
    void Start()
    {
        //animator = button.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        changeEffect();
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);




        if (other.CompareTag("red") || other.CompareTag("green") || other.CompareTag("blue")) // 2
        {
            if (ingredients.Count < 3)
            {
                ingredients.Add(other.tag);
            }


        }


    }

    public void setPressed(){
        pressed=true;
    }
    private void changeEffect()
    {
        if (pressed)
        {
            effect1.SetActive(false);
            effect2.SetActive(true);
        }
    }
}
//&&ingredients.Count==3

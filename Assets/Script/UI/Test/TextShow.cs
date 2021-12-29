using UnityEngine;
using TMPro;

public class TextShow : MonoBehaviour
{
    public TMP_Text text;

    private void Start()
    {
        text.color = new Vector4(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        text.color = new Vector4(0, 0, 0, Mathf.Lerp(text.color.a, 1, Time.deltaTime));
    }
}

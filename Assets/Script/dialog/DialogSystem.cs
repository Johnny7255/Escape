using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text textLabel;
    // public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    // [Header("头像")]
    // public Sprite face01, face02;

    bool textFinished;//是否完成打字
    bool cancelTyping;//取消打字

    List <string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
    }
    private void OnEnable()
    {
        // textLabel.text = textList[index];
        // index++;
        textFinished = true;
        StartCoroutine(SetTextUI());

    }

    // Update is called once per frame
    void Update()
    {
        if (index == textList.Count){
        // if (Input.GetKeyDown(KeyCode.R) && index == textList.Count){
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if(textFinished){
        // if(Input.GetKeyDown(KeyCode.R) && textFinished){
            textLabel.text = textList[index];
            index++;
            StartCoroutine(SetTextUI());
        }

        // if (Input.GetKeyDown(KeyCode.R)){
        //     if (textFinished && !cancelTyping){
        //         StartCoroutine(SetTextUI());
        //     }else if (!textFinished && !cancelTyping){
        //         cancelTyping = true;
        //     }
        // }
    }

    void GetTextFromFile(TextAsset file){
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData){
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI(){
        textFinished = false;
        textLabel.text = "";

        switch (textList[index]){
            case "A":
            // faceImage.sprite = face01;
                index++;
                break;

            case "B":
            // faceImage.sprite = face02;
                index++;
                break;

        }

        // for (int i = 0; i < textList[index].Length; i++){
        //     textLabel.text += textList[index][i];

        //     yield return new WaitForSeconds(textSpeed);
        // }

        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1){
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);

        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;

        index ++;
    }

    
}

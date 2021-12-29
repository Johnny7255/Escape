using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class InputStudy : MonoBehaviour
{
    public Text devices;
    public Text input;
    private StringBuilder inputs = new StringBuilder();
    List<InputDevice> allDevices;
    
    // Start is called before the first frame update
    void Start()
    {
        allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        StringBuilder devicesInfo = new StringBuilder();
        foreach (var item in allDevices)
        {
            devicesInfo.AppendLine(item.name);
        }
        devices.text = devicesInfo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        inputs.Clear();
        foreach (var item in allDevices)
        {
            LogInput(item);
        }
        input.text = inputs.ToString();
    }

    private void LogInput(InputDevice device)
    {
        inputs.AppendLine(device.name);
        
        device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtoValue);
            if(primaryButtoValue) inputs.AppendLine("PrimaryButton");

        device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondarButtoValue);
            if(secondarButtoValue) inputs.AppendLine("secondaryButton");

        device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            if(triggerValue>0f) inputs.AppendLine("trigger values "+ triggerValue);

        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axisValue);
            inputs.AppendLine("primary2DAxis values "+ axisValue);

        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool axisClick);
            if(axisClick) inputs.AppendLine("primary2DAxis clicked ");
        
        device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue);
            if(gripValue) inputs.AppendLine("griped");
    }

    public void Quite()
    {
        Application.Quit();
    }
}

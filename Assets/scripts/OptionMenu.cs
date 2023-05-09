using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{

    public Toggle fullScreenTog;
    public Toggle VsyncTog;
    public List<ResItem> resolution = new List<ResItem>();
    private int selectedResolution;

    public TMP_Text resolutionLabel;
    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            VsyncTog.isOn = false;
        }
        else
        {
            VsyncTog.isOn = true;
        }

        bool foundRes = false;

        for(int i = 0; i < resolution.Count; i++)
        {
            if(Screen.width == resolution[i].horizontal && Screen.height == resolution[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                updateResolution();
            }
        }

        if(!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolution.Add(newRes);
            selectedResolution = resolution.Count - 1;

            updateResolution();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()  
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        updateResolution();
    }

    public void ResRight()
    {
        selectedResolution++;
        if(selectedResolution > resolution.Count-1)
        {
            selectedResolution = resolution.Count-1;
        }
        updateResolution();
    }
    
    public void updateResolution()
    {
        resolutionLabel.text = resolution[selectedResolution].horizontal.ToString() + " X " + resolution[selectedResolution].vertical.ToString();
    }

    public void ApplySetting()
    {
        // Screen.fullScreen = fullScreenTog.isOn;

        if(VsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolution[selectedResolution].horizontal, resolution[selectedResolution].vertical, fullScreenTog.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal;
    public int vertical;

};
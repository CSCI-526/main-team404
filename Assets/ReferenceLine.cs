using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class ReferenceLine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<Light2D> lights;
    private bool isActive = false;
    public float lightChangeTime = 1f;
    public float finalIntensity = 1f;
    public float finalVolumetricIntensity = 1f;
    void Start()
    {
        // Get all Light2D components in the children of this GameObject
        lights = new List<Light2D>(GetComponentsInChildren<Light2D>());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Show();
        //}
        
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Hide();
        //}
        // if the object is active, change the light volumetric intensity to 1 using lightChangeSpeed
        // else change the light volumetric intensity to 0 using lightChangeSpeed
        foreach (var light in lights)
        {
            if (isActive)
            {
                light.intensity = Mathf.Lerp(light.intensity, finalIntensity, Time.deltaTime / lightChangeTime);
                light.volumeIntensity = Mathf.Lerp(light.volumeIntensity, finalVolumetricIntensity, Time.deltaTime / lightChangeTime);
            }
            else
            {
                light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime / lightChangeTime);
                light.volumeIntensity = Mathf.Lerp(light.volumeIntensity, 0, Time.deltaTime / lightChangeTime);
            }
        }
    }

    public void Show()
    {
        isActive = true;
    }
    public void Hide()
    {
        isActive = false;
    }
}

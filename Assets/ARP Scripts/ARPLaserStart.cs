using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class ARPLaserStart : MonoBehaviour
{
    public Material mat1, mat2;
    ARPLaser beam;
    public bool laserActive;
    public Vector3 direction;
    public AudioSource laserSound;

    void Start()
    {
        UnityEngine.Debug.Log(gameObject.transform.position);
    }


    void Update()
    {
        if (laserActive)
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
            beam = new ARPLaser(gameObject.transform.position, direction, mat1, mat2, gameObject.name, laserSound); 
        }
        else
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
        }
    }

    public void SetLaserActive(bool input)
    {
        laserActive = input;
    }

    public void ActivateLaserStart(string targetobject)
    {
        switch (targetobject)
        {
            case "portal1":
                GameObject.Find("Portal1").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "portal2":
                GameObject.Find("Portal2").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp1":
                GameObject.Find("Amp1").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp2":
                GameObject.Find("Amp2").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
        }


    }

    public void DeactivateLaserStart(string targetobject)
    {
        switch (targetobject)
        {
            case "portal1":
                GameObject.Find("Portal1").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "portal2":
                GameObject.Find("Portal2").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp1":
                GameObject.Find("Amp1").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp2":
                GameObject.Find("Amp2").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
        }
    }
}
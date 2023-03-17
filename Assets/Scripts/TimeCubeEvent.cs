using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class TimeCubeEvent : MonoBehaviour
{
    public AudioSource soundPlayerPast;
    public AudioSource soundPlayerPresent;
    public GameObject star;
    public GameObject warpEffect;
    public GameObject[] wheels;
    public manager managerScript;
    public Material enableMaterial;
    public Material disableMaterial;
    public Camera m_Camera;

    private bool enabled = false;
    private bool enabledOnce = false;
    private bool glowUp = true;
    private string goalYear = "1998";

    /*
     * Turns warp effect off and sets year to the starting numbers of the wheels
     */
    void Start()
    {
        warpEffect.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        //m_Camera = Camera.main;
    }

    /*
     * Checks if the input corresponds to the desired year to travel to
     */
    public void checkYear()
    {
        string curYear = "";
        for (int i = 0; i < wheels.Length; i++)
        {
            int nr = Int32.Parse(wheels[i].GetComponent<TimeCubeNumber>().currentNumberChar);
            if (nr < 10)
            {
                curYear += wheels[i].GetComponent<TimeCubeNumber>().currentNumberChar;
            } else
            {
                curYear += "0";
            }
        }
        Debug.Log("Goal: " + goalYear);
        Debug.Log("Current: " + curYear);

        if (string.Equals(curYear, goalYear))
        {
            starEnable(true);
        }
        else
        {
            starEnable(false);
        }
    }

    /*
     * Enables the star -> turns yellow, glows and allows time travel
     */
    public void starEnable(bool enable)
    {
        Debug.Log("here: " + enable);
        if (enable)
        {
            enabled = enable;
            MeshRenderer my_renderer = star.GetComponent<MeshRenderer>();
            if (my_renderer != null)
            {
                my_renderer.material = enableMaterial;

            }
        }
        else {
            enabled = enable;
            MeshRenderer my_renderer = star.GetComponent<MeshRenderer>();
            if (my_renderer != null)
            {
                my_renderer.material = disableMaterial;
            }
        }

    }

    /*
     * Changes emission glow of star
     */
    public void SetCustomMaterialEmissionIntensity(float intensity)
    {
        // get the material at this path
        Material mat = enableMaterial;
        Color color = mat.GetColor("_Color");

        // for some reason, the desired intensity value (set in the UI slider) needs to be modified slightly for proper internal consumption
        float adjustedIntensity = intensity - (0.4169F);

        // redefine the color with intensity factored in - this should result in the UI slider matching the desired value
        color *= Mathf.Pow(2.0F, adjustedIntensity);
        mat.SetColor("_EmissionColor", color);
    }


    /*
     * Initiates the warp, if the correct year was selected
     */
    public void initiateWarp()
    {
        if (enabled)
        {
            enabledOnce = true;
            StartCoroutine(initiateWarpReal());
        }
    }

    /*
     * Initiates the warp -> warp effect, sound
     */
    public IEnumerator initiateWarpReal()
    {
        if (enabledOnce)
        {
            enabledOnce = false;
            managerScript.setTime(Int32.Parse(goalYear));
            if (string.Equals(goalYear, "2023"))
            {
                soundPlayerPresent.Play();
                goalYear = "1991";
            }
            else
            {
                soundPlayerPast.Play();
                goalYear = "2023";
            }

            warpEffect.transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);

            warpEffect.GetComponent<ParticleSystem>().Play(true);
            warpEffect.GetComponent<WarpSpeed>().Engage();
            yield return new WaitForSeconds(5);
            warpEffect.GetComponent<WarpSpeed>().Disengage();
            warpEffect.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            starEnable(false);
            
        }
    }
}

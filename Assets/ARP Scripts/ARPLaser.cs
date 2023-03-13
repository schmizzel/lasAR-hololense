using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ARPLaser
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();
    string pointerName;
    Material mat1, mat2;


    public ARPLaser(Vector3 pos, Vector3 dir, Material red, Material green, string name)
    {
        this.laser = new LineRenderer();
        this.pointerName = name;
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam-" + name;
        this.pos = pos;
        this.dir = dir;
        this.mat1 = red;
        this.mat2 = green;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.02f;
        this.laser.endWidth = 0.02f;
        this.laser.material = red;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 30))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void checkLaserCollison()
    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {


        switch (hitInfo.collider.gameObject.tag)
        {
            case "ColliderTest":
                laser.SetPosition(1, hitInfo.point);
                break;
            case "AmpTest":
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SetLaserActive(true);
                break;

        }

    }
}

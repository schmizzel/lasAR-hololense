using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LaserNew {
    public GameObject laserTarget;
    Material mat1, mat2;
    Material currentMaterial;
    string ignoreTag = "";
    bool hitBox = false;

    public LaserNew(Vector3 pos, Vector3 dir, Material red, Material green) {
        this.laserTarget = new GameObject();
        this.laserTarget.name = "Laser Beam";
        this.mat1 = red;
        this.mat2 = green;
        this.currentMaterial = this.mat1;
        this.laserTarget.tag = "Laser";
        addLaserSegment(pos, dir, this.mat1, 0);

        if (!hitBox) {
            enableBox();
        }
    }

    private void addLaserSegment(Vector3 origin, Vector3 direction, Material mat, int depth) {
        if (depth > 20) {
            return;
        }
    
        LineRenderer nextSegment = makeLineRenderer(origin, mat);
        Ray ray = new Ray(origin, direction);
        RaycastHit hit; 

        if (closestHit(Physics.RaycastAll(ray), out hit)) {
            ignoreTag = hit.collider.gameObject.tag;
            nextSegment.SetPosition(1, hit.point);
            checkHit(hit, depth + 1);
        } else {
            nextSegment.SetPosition(1, direction * 20 + origin);
        }
    }

    private void checkHit(RaycastHit hitInfo, int depth) {
        Vector3 dir = hitInfo.normal * -1;
        Vector3 origin;

        // TODO: Add a max recursion depth
        switch (hitInfo.collider.gameObject.tag) {
            case "AmpTest":
                this.currentMaterial = mat2;
                addLaserSegment(hitInfo.point, dir, this.currentMaterial, depth);
                break;
            case "Portal1":
                origin = GameObject.FindWithTag("Portal2").transform.position;
                addLaserSegment(origin, dir, this.currentMaterial, depth);
                break;
            case "Portal2":
                origin = GameObject.FindWithTag("Portal1").transform.position;
                addLaserSegment(origin, dir, this.currentMaterial, depth);
                break;
            case "Mirror":
                this.currentMaterial = mat1;
                addLaserSegment(hitInfo.point, hitInfo.normal, this.currentMaterial, depth);
                break;
            case "Goal":
                if (this.currentMaterial == mat2) {
                    hitBox = true;
                    openRemoteBox();
                }
                break;
        }
    }

    private LineRenderer makeLineRenderer(Vector3 origin, Material mat) {
        GameObject child = new GameObject();
        child.transform.parent = this.laserTarget.transform;

        LineRenderer segment = child.AddComponent(typeof(LineRenderer)) as LineRenderer;    
        segment.startWidth = 0.02f;
        segment.endWidth = 0.02f;
        segment.material = mat;
        segment.SetPosition(0, origin);
        return segment;
    }

    private bool closestHit(RaycastHit[] hits, out RaycastHit closest) {
        float min = 9999999;
        bool ok = false;
        closest = new RaycastHit();
        foreach (var hit in hits) {
            if (hit.collider.gameObject.tag == ignoreTag) {
                continue;
            }

            if (hit.distance < min) {
                ok = true;
                min = hit.distance;
                closest = hit;
            }            
        }

        return ok;
    }

    private void openRemoteBox() {
        Debug.Log("opening box");
        GameObject.Find("ScriptController").GetComponent<OpenBox>().openRemoteBox();
        //StartCoroutine(getRequest("http://192.168.1.1:3000/api/open"));
    }

    private void enableBox() {
       GameObject.Find("ScriptController").GetComponent<OpenBox>().enableBox(); 
    }

    private IEnumerator getRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for result
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}

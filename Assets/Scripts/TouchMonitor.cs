using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchMonitor : MonoBehaviour
{
    private ARPlane portalPlane;
    public GameObject portalPrefab;
    public ARRaycastManager arRaycastManager;
    public ARPlaneManager arPlaneManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isInitiated;
    public Text text;
    private void Start()
    {
        portalPlane = FindObjectOfType<ARPlane>();
        //text.text = "Started";
        isInitiated = false;
       
    }
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    //text.text = "Plane Detected";
                    Pose hitPose = hits[0].pose;
                    
                    if (!isInitiated)
                    {
                        Instantiate(portalPrefab, hitPose.position, hitPose.rotation);
                        int planeCount = arPlaneManager.trackables.count;
                        if (planeCount >= 1)
                        {
                            // Only run this sequence if the AR Plane Manager exists
                            if (arPlaneManager != null)
                            {
                                arPlaneManager.SetTrackablesActive(false);
                                arPlaneManager.enabled = false;
                            }
                        }
                        isInitiated = true;
                    }
                    
                    
                    
                }
                else
                {
                    //text.text = "Not Detected";
                }
            }
        }
    }
}

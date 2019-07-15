using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaneMode : Mode
{
    public GameObject sceneObject;

    private Anchor currentSceneAnchor;
    private Vector3 _previousAnchorPosition;

    [Header("References")]
    public Button acceptButton;

    public bool canRaycast = false;
    private bool _firstTime = true;

    public override void OnModeEnabled()
    {
        base.OnModeEnabled();
        PlaneManager.instance.SetDetectedPlaneVisualizerActive(true);
        canRaycast = true;
    }

    public override void OnModeDisabled()
    {
        base.OnModeDisabled();
        PlaneManager.instance.SetDetectedPlaneVisualizerActive(false);
        canRaycast = false;
    }

    public void Start()
    {
        acceptButton.interactable = false;
        sceneObject.SetActive(false);
    }

    public void PlaneIsSelected()
    {
        acceptButton.interactable = true;
    }

    public void OnAccept()
    {
        canRaycast = false;
        UIManager.instance.SetMode(ModeState.EditMode);
    }

    public void UpdatePointOfInterest()
    {
        if(currentSceneAnchor != null)
        {
            if(_previousAnchorPosition != currentSceneAnchor.transform.position)
            {
                _previousAnchorPosition = currentSceneAnchor.transform.position;
                ScaleManager.instance.pointOfInterest = _previousAnchorPosition;
            }
        }
    }

    private void Update()
    {
        UpdatePointOfInterest();

        if (!canRaycast || Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        TrackableHit hit;
        TrackableHitFlags filter = TrackableHitFlags.PlaneWithinPolygon;

        if(Frame.Raycast(touch.position.x, touch.position.y, filter, out hit) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(Camera.main.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
                return;

            if((hit.Trackable is DetectedPlane) && ((DetectedPlane)hit.Trackable).PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
            {

                Transform root = ScaleManager.instance.rootTransform;

                Vector3 hitPosition = hit.Pose.position;

                hitPosition.Scale(root.transform.localScale);
                root.localPosition = hitPosition * -1;

                sceneObject.transform.position = hit.Pose.position;

                ScaleManager.instance.pointOfInterest = sceneObject.transform.position;

                if (_firstTime)
                {
                    _firstTime = false;
                    ScaleManager.instance.InitializeStartScale();
                }

                ScaleManager.instance.AlignWithPointOfInterest(hit.Pose.position);

                if (sceneObject.activeSelf == false)
                    sceneObject.SetActive(true);

                if(touch.phase == TouchPhase.Ended)
                {
                    PlaneIsSelected();
                    Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    currentSceneAnchor = anchor;
                    sceneObject.transform.SetParent(anchor.transform);
                }
            }
        }
    }
}

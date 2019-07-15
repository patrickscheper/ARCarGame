using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditMode : Mode
{
    private float _currentScale;

    private float _previousSliderValue;

    public Slider scaleSlider;

    public TextMeshProUGUI measurementText;

    public AnimationCurve animationCurve;

    public Transform SceneObject;

    [Header("Ramp Properties")]
    public GameObject currentRamp;

    public LayerMask rampLayerMask;
    public LayerMask planeLayerMask;

    public TMP_Dropdown rampDropdown;

    public bool isPlacingRamp;

    [Header("Buttons")]
    public Button planeButton;
    public Button acceptButton;
    public GameObject rampPlaceButtons;
    public GameObject rampEditButtons;

    //    private void Start()
    //    {
    //        rampDropdown.ClearOptions();
    //        rampDropdown.AddOptions(RampManager.instance.GetRampNames());
    //        SetEditButtons(false);
    //        SetPlaceButtons(false);
    //    }

    //    public void OnAccept()
    //    {
    //        UIManager.instance.SetMode(ModeState.PlayMode);
    //    }

    //    public void BackToPlaneMode()
    //    {
    //        UIManager.instance.SetMode(ModeState.PlaneMode);
    //    }

    //    public void ChangeScale(bool value)
    //    {
    //        if (!value)
    //        {
    //            CheckScale(scaleSlider.value);
    //            ScaleManager.instance.SetScale(_currentScale);
    //        }
    //    }

    //    public void CheckScale(float scale)
    //    {
    //        scaleSlider.interactable = false;

    //        if (scale != _previousSliderValue)
    //        {
    //            _previousSliderValue = scale;
    //            Invoke("ActivateSlider", ScaleManager.instance.animationTime);
    //        }
    //        else
    //            ActivateSlider();

    //        switch (scale)
    //        {
    //            case 5:
    //                _currentScale = 1f;
    //                break;
    //            case 4:
    //                _currentScale = 5f / 1f;
    //                break;
    //            case 3:
    //                _currentScale = 5f / 0.5f;
    //                break;
    //            case 2:
    //                _currentScale = 5f / 0.25f;
    //                break;
    //            case 1:
    //                _currentScale = 5f / 0.10f;
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    public void SetMeasurementText(float value)
    //    {
    //        switch (value)
    //        {
    //            case 5:
    //                measurementText.text = "5 M";
    //                break;
    //            case 4:
    //                measurementText.text = "1 M";
    //                break;
    //            case 3:
    //                measurementText.text = "50 CM";
    //                break;
    //            case 2:
    //                measurementText.text = "25 CM";
    //                break;
    //            case 1:
    //                measurementText.text = "10 CM";
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    public void ActivateSlider()
    //    {
    //        scaleSlider.interactable = true;
    //    }

    //    public void OnStartPlacingRamp(int index)
    //    {
    //        currentRamp = RampManager.instance.GetAndActivateRamp(index);
    //        OnEditRamp();
    //    }

    //    public void OnEditRamp()
    //    {
    //        isPlacingRamp = true;
    //        SetEditButtons(false);
    //        SetPlaceButtons(true);
    //    }

    //    public void OnPlaceRamp()
    //    {
    //        isPlacingRamp = false;
    //        currentRamp = null;
    //        SetPlaceButtons(false);
    //    }

    //    public void OnRemoveRamp()
    //    {
    //        isPlacingRamp = false;
    //        StartCoroutine(ScaleAndRemoveRamp(currentRamp, 0.75f));
    //        SetEditButtons(false);
    //        SetPlaceButtons(false);
    //        currentRamp = null;
    //    }

    //    public void SetEditButtons(bool value)
    //    {
    //        rampEditButtons.SetActive(value);
    //        planeButton.interactable = !value;
    //        acceptButton.interactable = !value;
    //    }

    //    public void SetPlaceButtons(bool value)
    //    {
    //        rampPlaceButtons.SetActive(value);
    //        planeButton.interactable = !value;
    //        acceptButton.interactable = !value;
    //    }

    //    public void Update()
    //    {
    //        if (Input.touchCount < 1)
    //        {
    //            return;
    //        }

    //        Touch touch;

    //        DetectTouchMovement.Calculate();

    //        touch = Input.GetTouch(0);

    //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
    //        RaycastHit hit;

    //        if (!isPlacingRamp && Physics.Raycast(ray, out hit, float.MaxValue, rampLayerMask) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
    //        {
    //            currentRamp = hit.transform.gameObject;
    //            SetEditButtons(true);
    //        }
    //        else if (currentRamp != null && !isPlacingRamp && Physics.Raycast(ray, out hit, float.MaxValue, ~rampLayerMask) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
    //        {
    //            currentRamp = null;
    //            SetEditButtons(false);
    //        }


    //        if (!isPlacingRamp)
    //            return;

    //        if (Physics.Raycast(ray, out hit, float.MaxValue, planeLayerMask) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
    //        {
    //            currentRamp.transform.position = hit.point;

    //            if(Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
    //            {
    //                Vector3 rotationDeg = Vector3.zero;
    //                rotationDeg.y = -DetectTouchMovement.turnAngleDelta * 2;
    //                currentRamp.transform.rotation *= Quaternion.Euler(rotationDeg);
    //            }
    //        }
    //    }

    //    public IEnumerator ScaleAndRemoveRamp(GameObject ramp, float time)
    //    {
    //        float elaspedTime = 0;
    //        Vector3 startScale = ramp.transform.localScale;
    //        Vector3 targetScale = Vector3.zero;

    //        while(elaspedTime < time)
    //        {
    //            ramp.transform.localScale = Vector3.Lerp(startScale, targetScale, animationCurve.Evaluate(elaspedTime / time));
    //            elaspedTime += Time.deltaTime;
    //            yield return new WaitForEndOfFrame();
    //        }

    //        RampManager.instance.DeactiveRamp(ramp);
    //    }
    //}

    private void Start()
    {
        rampDropdown.ClearOptions();
        rampDropdown.AddOptions(RampManager.instance.GetRampNames());
        SetEditButtons(false);
        SetPlaceButton(false);
    }

    public void OnAccept()
    {
        UIManager.instance.SetMode(ModeState.PlayMode);
    }

    public void BackToPlaneMode()
    {
        UIManager.instance.SetMode(ModeState.PlaneMode);
    }

    public void OnStartPlacingRamp(int index)
    {
        currentRamp = RampManager.instance.GetAndActivateRamp(index);
        OnEditRamp();
    }

    public void Edit(bool down)
    {
        if (!down)
        {
            CheckScale(scaleSlider.value);
            ScaleManager.instance.SetScale(_currentScale);
        }
    }

    public void SetMeasurementText(float value)
    {
        switch (value)
        {
            case 5:
                measurementText.text = "5 M";
                break;
            case 4:
                measurementText.text = "1 M";
                break;
            case 3:
                measurementText.text = "50 CM";
                break;
            case 2:
                measurementText.text = "25 CM";
                break;
            case 1:
                measurementText.text = "10 CM";
                break;
        }
    }

    public void ActivateSlider()
    {
        scaleSlider.interactable = true;
    }

    public void CheckScale(float scale)
    {
        scaleSlider.interactable = false;

        if (scale != _previousSliderValue)
        {
            _previousSliderValue = scale;
            Invoke("ActivateSlider", 0.5f);
        }
        else
            ActivateSlider();

        scale = Mathf.RoundToInt(scale);
        switch (scale)
        {
            case 5:
                _currentScale = 1f;
                break;
            case 4:
                _currentScale = 5 / 1f;
                break;
            case 3:
                _currentScale = 5 / 0.5f;
                break;
            case 2:
                _currentScale = 5 / 0.25f;
                break;
            case 1:
                _currentScale = 5 / 0.10f;
                break;
            default:
                break;
        }
    }



    public void SetEditButtons(bool active)
    {
        rampEditButtons.SetActive(active);
        planeButton.interactable = !active;
        acceptButton.interactable = !active;
    }

    public void SetPlaceButton(bool active)
    {
        rampPlaceButtons.SetActive(active);
        planeButton.interactable = !active;
        acceptButton.interactable = !active;
    }
    public void OnPlaceRamp()
    {
        isPlacingRamp = false;
        currentRamp = null;
        SetPlaceButton(false);
    }

    public void OnEditRamp()
    {
        isPlacingRamp = true;
        SetEditButtons(false);
        SetPlaceButton(true);
    }

    public void OnRemoveRamp()
    {
        isPlacingRamp = false;
        StartCoroutine(ScaleAndRemoveRamp(currentRamp, 1f));
        SetEditButtons(false);
        SetPlaceButton(false);
        currentRamp = null;
    }

    void Update()
    {
        if (Input.touchCount < 1)
        {
            return;
        }

        Touch touch;

        DetectTouchMovement.Calculate();

        touch = Input.GetTouch(0);

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (!isPlacingRamp && Physics.Raycast(ray, out hit, float.MaxValue, rampLayerMask) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            currentRamp = hit.transform.gameObject;
            SetEditButtons(true);
        }
        else if (currentRamp != null && !isPlacingRamp && Physics.Raycast(ray, out hit, float.MaxValue, ~rampLayerMask) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            currentRamp = null;
            SetEditButtons(false);
        }


        if (!isPlacingRamp)
            return;

        if (Physics.Raycast(ray, out hit, float.MaxValue, planeLayerMask) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            //Vector3 scaledHitPosition = hit.point;

            //scaledHitPosition.Scale(ScaleManager.instance.rootTransform.localScale);

            //scaledHitPosition = scaledHitPosition + (ScaleManager.instance.rootTransform.position - Sceeobjcet.transform.position);

            currentRamp.transform.position = hit.point;// new Vector3(scaledHitPosition.x, hit.point.y, scaledHitPosition.z);

            if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
            {
                Vector3 rotationDeg = Vector3.zero;
                rotationDeg.y = -DetectTouchMovement.turnAngleDelta * 2;
                currentRamp.transform.rotation *= Quaternion.Euler(rotationDeg);
            }
        }
    }

    public IEnumerator ScaleAndRemoveRamp(GameObject ramp, float time)
    {
        float elapsedTime = 0;
        Vector3 startScale = ramp.transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (elapsedTime < time)
        {
            ramp.transform.localScale = Vector3.Lerp(startScale, targetScale, animationCurve.Evaluate(elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        RampManager.instance.DeactiveRamp(ramp);
    }
}

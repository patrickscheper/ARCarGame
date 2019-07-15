using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampManager : MonoBehaviour
{
    public static RampManager instance;

    public Transform sceneObject;

    public GameObject[] ramps;
    public List<GameObject> rampPool = new List<GameObject>();

    public float startAmount = 4;

    private Vector3 instantiatePosition = new Vector3(999, 999, 999);

    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    void Start()
    {
        InstantiateRamps(startAmount);
    }

    public void InstantiateRamps(float amount)
    {
        foreach (GameObject ramp in ramps)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject tempRamp = Instantiate(ramp, sceneObject);
                rampPool.Add(tempRamp);

                tempRamp.name = ramp.name;
                tempRamp.transform.localPosition = instantiatePosition;
                tempRamp.SetActive(false);
            }
        }
    }

    public List<string> GetRampNames()
    {
        List<string> rampNames = new List<string>();

        foreach (GameObject ramp in ramps)
        {
            rampNames.Add(ramp.name);
        }
        return rampNames;
    }

    public GameObject InstantiateNewRamp(int index)
    {
        GameObject tempRamp = Instantiate(ramps[index], sceneObject);
        rampPool.Add(tempRamp);
        tempRamp.name = ramps[index].name;
        tempRamp.transform.localPosition = instantiatePosition;
        tempRamp.SetActive(false);
        return tempRamp;
    }

    public GameObject GetAndActivateRamp(int index)
    {
        for (int i = 0; i < rampPool.Count; i++)
        {
            if(!rampPool[i].activeSelf && rampPool[i].name == ramps[index].name)
            {
                rampPool[i].SetActive(true);
                return rampPool[i];
            }
        }

        GameObject newRamp = InstantiateNewRamp(index);
        newRamp.SetActive(true);
        return newRamp;
    }

    public void DeactiveRamp(GameObject ramp)
    {
        for (int i = 0; i < rampPool.Count; i++)
        {
            if(rampPool[i] == ramp)
            {
                ramp.transform.localScale = Vector3.one;
                ramp.transform.localPosition = instantiatePosition;
                ramp.SetActive(false);
            }

        }
    }

}

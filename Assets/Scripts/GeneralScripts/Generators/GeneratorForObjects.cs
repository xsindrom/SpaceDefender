﻿using UnityEngine;
using System.Collections;

public class GeneratorForObjects : MonoBehaviour
{
    #region FIELDS
    [Header(StringHeadersInfo.OBJECTSTOGENERATE_Header)]
    public GameObject[] objectsToGenerate;
    [Header(StringHeadersInfo.TIMEPERIOD_Header)]
    public float timeToGenerate;
    private Timer timerForActions;
    #endregion
    #region STANDART_EVENTS
    void Start()
    {
        timerForActions = new Timer(timeToGenerate);
        timerForActions.AddAction(GenerateObject);
    }
    void Update()
    {
        timerForActions.CompleteAction();
    }
    #endregion
    #region LOGIC
    private void GenerateObject()
    {
        Instantiate(objectsToGenerate[Random.Range(0, objectsToGenerate.Length - 1)]);
    }
    #endregion
}

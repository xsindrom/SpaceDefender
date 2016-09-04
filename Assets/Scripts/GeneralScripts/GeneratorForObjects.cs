using UnityEngine;
using System.Collections;

public class GeneratorForObjects : MonoBehaviour
{
    [Header(StringHeadersInfo.OBJECTSTOGENERATE_Header)]
    public GameObject[] objectsToGenerate;
    [Header(StringHeadersInfo.TIMEPERIOD_Header)]
    public float timeToGenerate;
    private Timer timerForActions;
    void Start()
    {
        timerForActions = new Timer(timeToGenerate);
        timerForActions.AddAction(GenerateObject);
    }
    private void GenerateObject()
    {
        Instantiate(objectsToGenerate[Random.Range(0, objectsToGenerate.Length - 1)]);
    }
    void Update()
    {
        timerForActions.CompleteAction();
    }
}

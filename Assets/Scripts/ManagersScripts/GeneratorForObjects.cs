using UnityEngine;
using System.Collections;

public class GeneratorForObjects : MonoBehaviour
{
    public GameObject[] objectsToGenerate;
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

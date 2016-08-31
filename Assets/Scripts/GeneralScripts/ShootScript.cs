using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour
{
    public GameObject toInstantiate;
    public Vector3 offset;
    public float attackRate;
    private Timer timerForActions;
    void Start()
    {
        timerForActions = new Timer();
        timerForActions.TimeToComplete = attackRate;
        timerForActions.AddAction(CreateObject);
    }
    private void CreateObject()
    {
        if (toInstantiate == null)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(toInstantiate, transform.position + offset, new Quaternion());
        }
    }

    void Update()
    {
        timerForActions.CompleteAction();
    }
}

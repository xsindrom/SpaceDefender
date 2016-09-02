using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIViewManager : MonoBehaviour
{
    private Text scoreText;

    void Start()
    {
        scoreText = transform.GetComponent<Text>();
    }
    void Update()
    {
        scoreText.text = StringAdditionalInfo.SCORE_Caption + ScoreManager.Instance.Score.ToString();
    }
}
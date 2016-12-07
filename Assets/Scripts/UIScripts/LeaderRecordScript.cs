using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderRecordScript : MonoBehaviour
{

    private Text[] childText;
    public class Leader
    {
        public int id;
        public string leaderName;
        public int score;
        public int level;
        public Leader() { }
        public Leader(int id, string leaderName,int score,int level)
        {
            this.id = id;
            this.leaderName = leaderName;
            this.score = score;
            this.level = level;
        }
    }

    public Leader leader;
    void Start()
    {
        childText = gameObject.GetComponentsInChildren<Text>();
    }
    public void Fill(Leader leader)
    {
        childText[0].text = leader.id.ToString();
        childText[1].text = leader.leaderName;
        childText[2].text = leader.score.ToString();
        childText[3].text = leader.level.ToString();
    }
}

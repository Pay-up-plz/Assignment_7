using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiUpdater : MonoBehaviour {

    public Text score;
    public Text lives;

		// Update is called once per frame
	void Update () {
        score.text = StatsManager.yarn.ToString();
        lives.text = StatsManager.lives.ToString();
	
	}
}

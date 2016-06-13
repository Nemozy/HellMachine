using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour
{
	void Update () 
    {
        this.gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Score: " + GameObject.Find("Player").GetComponent<PlayerController>().GetScore();
	}
}

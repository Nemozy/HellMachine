using UnityEngine;
using System.Collections;

public class Alien : EnemyController
{
    public void Init(float Speed) 
    {
        MovSpeed = Speed;
        Initiate = true;
	}

    protected override void Move()
    {
        if (GameObject.FindGameObjectWithTag("PlayerShip"))
            this.transform.LookAt(GameObject.FindGameObjectWithTag("PlayerShip").transform);
        this.transform.position += new Vector3(this.transform.forward.x, this.transform.forward.y, this.transform.forward.z) * MovSpeed;
        if (GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text.Equals("2D"))
            this.transform.Rotate(90, 0, 0);
    }
}

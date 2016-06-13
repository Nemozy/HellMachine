using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private float Speed = 15f;
    const float DeltaWidth = 200f;
    const float DeltaHeight = 100f;

	void Update ()
    {
        GameObject gmobj = GameObject.FindGameObjectWithTag("PlayerShip");
        if (gmobj && this.GetComponent<Camera>().WorldToScreenPoint(gmobj.transform.position).x < DeltaWidth)
        {
            this.transform.position -= new Vector3(Time.deltaTime * Speed, 0, 0);
        }
        if (gmobj && this.GetComponent<Camera>().WorldToScreenPoint(gmobj.transform.position).x > Screen.width - DeltaWidth)
        {
            this.transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
        }
        if (gmobj && this.GetComponent<Camera>().WorldToScreenPoint(gmobj.transform.position).y < DeltaHeight)
        {
            this.transform.position -= new Vector3(0, 0, Time.deltaTime * Speed);
        }
        if (gmobj && this.GetComponent<Camera>().WorldToScreenPoint(gmobj.transform.position).y > Screen.height - DeltaHeight)
        {
            this.transform.position += new Vector3(0, 0, Time.deltaTime * Speed);
        }
	}
}

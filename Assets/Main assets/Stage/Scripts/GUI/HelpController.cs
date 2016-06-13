using UnityEngine;
using System.Collections;

public class HelpController : MonoBehaviour 
{
    public void InverseActive()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}

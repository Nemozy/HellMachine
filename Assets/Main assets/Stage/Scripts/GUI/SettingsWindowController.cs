using UnityEngine;
using System.Collections;

public class SettingsWindowController : MonoBehaviour
{
    public void SwitchGraphMode()
    {
        GameObject.Find("Terrain").GetComponent<StageEnvironment>().SendMessage("VisualSet_" + this.transform.Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text);
    }
}

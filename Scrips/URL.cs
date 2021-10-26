using UnityEngine;

public class URL : MonoBehaviour
{
    public string websiteAddress;

    public void OpenURLOnClick()
    {
        Application.OpenURL(websiteAddress);
    }
}
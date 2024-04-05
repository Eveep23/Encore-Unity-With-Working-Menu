using UnityEngine;
using System.Diagnostics;

public class OpenSongsFolder : MonoBehaviour
{
    public void OpenTheSongsFolder()
    {
        // Get the path to the "Songs" folder
        string songsFolderPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Songs");

        // Open the file explorer at the songs folder
        Application.OpenURL("file://" + songsFolderPath);
    }
}
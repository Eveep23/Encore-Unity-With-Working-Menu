using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SongLoader : MonoBehaviour
{
    public string songsFolderPath; // Path to the folder containing the songs in Unity

    public void OpenFileExplorer()
    {
        string selectedFolder = SelectFolder();

        if (!string.IsNullOrEmpty(selectedFolder))
        {
            ReplaceSongs(selectedFolder);
            RestartGame();
        }
    }

    private string SelectFolder()
    {
        // Open file picker dialog
        string selectedFolder = "";
        string dialogTitle = "Select Folder Containing New Song";

#if UNITY_EDITOR
            selectedFolder = UnityEditor.EditorUtility.OpenFolderPanel(dialogTitle, "", "");
#else
        selectedFolder = Application.dataPath; // Open in the default directory in the built game
        Application.OpenURL("file://" + selectedFolder);
#endif

        return selectedFolder;
    }

    private void ReplaceSongs(string selectedFolder)
    {
        // Delete previous song files
        DirectoryInfo directory = new DirectoryInfo(songsFolderPath);
        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        // Copy or move new song files from selected folder to Unity folder
        DirectoryInfo newSongsDirectory = new DirectoryInfo(selectedFolder);
        foreach (FileInfo file in newSongsDirectory.GetFiles())
        {
            string filePath = Path.Combine(songsFolderPath, file.Name);
            File.Copy(file.FullName, filePath); // Or File.Move() if you prefer
        }

        // Optionally, notify the player that the songs have been updated
        Debug.Log("Songs updated successfully!");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class LoadConfigurationUI : MonoBehaviour
{
    public GameManager gameManager;
    public Dropdown difficultyDropdown;
    public Dropdown instrumentDropdown;
    public Dropdown songDropdown; // Add reference to the new dropdown

    private Button playButton;

    private void Start()
    {
        playButton = GetComponent<Button>();
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayButtonClicked);
        }
        else
        {
            Debug.LogWarning("LoadConfigurationUI script requires a Button component on the GameObject.");
        }

        // Populate the song dropdown when the script starts
        PopulateSongDropdown();
    }

    private void PopulateSongDropdown()
    {
        // Get the path to the "Songs" folder
        string songsFolderPath = Path.Combine(Application.streamingAssetsPath, "Songs");

        // Get the list of directories (songs) in the "Songs" folder
        string[] songDirectories = Directory.GetDirectories(songsFolderPath);

        // Clear existing options in the dropdown
        songDropdown.ClearOptions();

        // Create a list to store the options
        List<string> songOptions = new List<string>();

        // Add each directory name (song name) to the options list
        foreach (string songDirectory in songDirectories)
        {
            string songName = Path.GetFileName(songDirectory);
            songOptions.Add(songName);
        }

        // Set the options for the dropdown
        songDropdown.AddOptions(songOptions);
    }

    private void PlayButtonClicked()
    {
        if (gameManager != null)
        {
            // Get the selected difficulty, instrument, and song from dropdowns
            SongDifficulty selectedDifficulty = (SongDifficulty)difficultyDropdown.value;
            InstrumentType selectedInstrument = (InstrumentType)instrumentDropdown.value;
            string selectedSong = songDropdown.options[songDropdown.value].text;

            // Construct the chart resource filename based on the selected song
            string chartResourceFilename = $"Songs/{selectedSong}/info.json";

            // Load configuration with the specified difficulty, instrument, and song
            gameManager.LoadConfiguration(chartResourceFilename, selectedInstrument, selectedDifficulty);
            gameManager.StartPlay();
        }
        else
        {
            Debug.LogWarning("GameManager reference is not set. Please assign GameManager in the inspector.");
        }
    }
}
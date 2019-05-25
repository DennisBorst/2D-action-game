using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PausedMenu : MonoBehaviour
{

    public GameObject ui;
    private Player player;
    private Abilities abilities;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        abilities = FindObjectOfType<Abilities>();
    }

    void Update()
    {
        if (ui != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))
            {
                Toggle();
            }
        }
    }

    public void Toggle()
    {
        if(ui != null)
        {
            ui.SetActive(!ui.activeSelf);

            if (ui.activeSelf)
            {
                Time.timeScale = 0f;
                player.enabled = false;
                abilities.enabled = false;
            }
            else
            {
                Time.timeScale = 1f;
                player.enabled = true;
                abilities.enabled = true;
            }
        }
    }

    public void MainMenu(string levelName)
    {
        Toggle();
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void OnClickButton(string ActionType)
    {
        switch (ActionType)
        {
            case "StartGame":
                int currentIndex;
                if (PlayerPrefs.HasKey("CurrentLevel"))
                {
                    currentIndex = PlayerPrefs.GetInt("CurrentLevel");
                }
                else
                {
                    currentIndex = 1;
                }
                SceneManager.LoadScene(currentIndex);              
                break;       
            case "Quit":
                Application.Quit();
                break;
        }
    }

}

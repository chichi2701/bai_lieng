using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Cards : MonoBehaviour 
{
	Random_ButThat randomButThat;

	public int cardsPerPlayer = 3;

	public Button button_playersNumberSubmit;
    public Button button_SpawnCards;
    

    public Text selectUsersText;
    public Text warningText;

    public Transform playersParent;

    public int allAvailableCards_Number;
    public int checkedNumberOfCards = 0;

    public GameObject winnerPanel;
    public GameObject exitPanel;


    void Start()
	{
		randomButThat = FindObjectOfType<Random_ButThat> ();

        button_playersNumberSubmit.onClick.AddListener (OnSubmit);

        button_playersNumberSubmit.transform.parent.gameObject.SetActive (true);
        button_SpawnCards.gameObject.SetActive(false);

	}

    void Update()
    {
        if(allAvailableCards_Number != 0)
        {
            //do something
            if (checkedNumberOfCards == allAvailableCards_Number && randomButThat.giveWinningCards == 0)
            {
                winnerPanel.gameObject.SetActive(true);
                winnerPanel.GetComponentInChildren<Text>().text = "No player wins !";
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<ExitGameHandler>().TogglePanel(exitPanel);
        }
    }

    public void ResetGame()
	{
//		Thread cardInitThread = new Thread (new ThreadStart (randomButThat.InitCardsArray));
//		cardInitThread.Start ();

        Time.timeScale = 1;
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	void OnSubmit()
	{
		string inputText = button_playersNumberSubmit.transform.parent.GetComponentInChildren<InputField> ().text;

        if (int.Parse(inputText) <= 6 && int.Parse(inputText) >= 1)
        {
            int playerInput = int.Parse(inputText);

            int arraySize = playerInput * cardsPerPlayer;
            randomButThat.CreateCardsArray(arraySize);

            for (int i = 0; i < playerInput; i++)
            {
                playersParent.GetChild(i).gameObject.SetActive(true);
            }


            Destroy(button_playersNumberSubmit.transform.parent.gameObject);

            button_SpawnCards.gameObject.SetActive(true);
            selectUsersText.gameObject.SetActive(true);
        }
        else
        {
            ShowWarning("Players must be between 1 to 6");
        }
	}

    void ShowWarning(string warningMessage)
    {
        warningText.gameObject.SetActive(true);
        warningText.text = warningMessage;

        Invoke("DisableWarning", 2);
    }

    void DisableWarning()
    {
        warningText.gameObject.SetActive(false);
    }
}

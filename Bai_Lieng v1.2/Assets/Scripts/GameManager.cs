using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchText;

    private bool _init = false;
    private int _matches = 13;

	
	// Update is called once per frame
	void Update () {
        if (!_init)
            initalizedCards();
        if (Input.GetMouseButtonUp(0))
            checkCards();
	}

    void initalizedCards()
    {
        for (int id = 0; id < 2; id++)
        {
            for (int i = 1; i < 14; i++)
            {
                bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<Cards>().initialized);
                }
                cards[choice].GetComponent<Cards>().cardValue = i;
                cards[choice].GetComponent<Cards>().initialized = true;
            }
        }
        foreach (GameObject c in cards)
            c.GetComponent<Cards>().setupGraphics();

        if (!_init)
            _init = true;
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }
    public Sprite getCardFace(int i)
    {
        return cardFace [i-1];
    }
    void checkCards()
    {
        List<int> c = new List<int>();

        for (int i = 0; i<cards.Length; i++)
        {
            if (cards[i].GetComponent<Cards>().state == 1)
                c.Add(i);
        }

        if (c.Count == 2)
            cardComparison(c);
    }
    void cardComparison(List<int> c)
    {
        Cards.DO_NOT = true;

        int x = 0;
       
        if(cards [c[0]].GetComponent<Cards>().cardValue == cards[c[1]].GetComponent<Cards>().cardValue)
        {
            x = 2;
            _matches--;
            matchText.text = "Number of Matches : " + _matches;
            if (_matches == 0)
                SceneManager.LoadScene("Menu");
        }
        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<Cards>().state = x;
            cards[c[i]].GetComponent<Cards>().falseCheck();
        }
    }
}

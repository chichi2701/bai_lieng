using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Random_ButThat : MonoBehaviour
{
    public int[] cardIds;
    int[] randomPLayer = { 0, 3, 6, 9, 12, 15 };

    public int giveWinningCards = 0;

    int random13;

    void InitCardsArray()
    {
        System.Random randWin = new System.Random();
        giveWinningCards = randWin.Next(0, 2); // give 0 or 1, work as a bool

        if (giveWinningCards == 1)
        {
            random13 = GiveAPlayerWinningCards();
            print("random13 : " + random13);
        }
            
        else
            print("Giving winning cards to nobody");


        System.Random cardId = new System.Random();

        for (int i = 0; i < cardIds.Length; i++)
        {
            int tempId = cardId.Next(1, 53); // random b/w 1 and 52
            for (int j = 0; j < i; j++)
            {
                while (cardIds[j] == tempId || tempId == random13 || tempId == random13 + 13 || tempId == random13 + 13 * 2)
                {
                    tempId = cardId.Next(1, 53); // random b/w 1 and 52
                }

            }
            if (cardIds[i] == 0)
            {
                cardIds[i] = tempId;
            }

            //			Thread.Sleep (50);
        }
        //		if (giveWinningCards == 1)
        //			GiveAPlayerWinningCards ();
        //		else
        //			print ("Giving winning cards to nobody");
    }

    public void CreateCardsArray(int size)
    {
        cardIds = new int[size];

        Thread cardInitThread = new Thread(new ThreadStart(InitCardsArray));
        cardInitThread.Start();
    }

    int GiveAPlayerWinningCards()
    {
        //for 0, 3, 6, 9, 12, 15 -> have cards with difference 13

        // 1. select a random number between 0 and number of playersNum that is divisble by 3
        // 2. for i = random; i < random+3; i++,  select a random number between 1 and 13 that is not in the array
        // 2a. assign it in arr[i] = rand13 + 13*i

        int randNum3 = 0; //random number divisble by 3

        
        // select a card that is divisiblle by 3
        System.Random divisibleBy3 = new System.Random();
        do { randNum3 = divisibleBy3.Next(0, cardIds.Length-3); } while (randNum3 % 3 != 0);

        print("Giving winning cards from " + randNum3);

        // select a card from 1 to 13
        System.Random from1To13 = new System.Random();
        int randNum13 = from1To13.Next(1, 14);

        int multiuplier = 0;

        for (int i = randNum3; i < randNum3 + 3; i++)
        {
            cardIds[i] = randNum13 + 13*multiuplier;
            multiuplier++;
        }

        
        return randNum13;
    }
}

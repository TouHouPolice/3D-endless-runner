using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    public static int pageNumber = 0;
    public GameObject[] tutorialPage;

    public GameObject tutorialCanvas;
    public GameObject openningCanvas;

    public GameObject nextButton;
    public GameObject previousButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextPage()
    {
        if (pageNumber < 4)
        {
            pageNumber += 1;
        }
        SwitchPage();
    }
    public void LastPage()
    {
        if (pageNumber > 0)
        {
            pageNumber -= 1;
        }
        SwitchPage();
    }

    public void SwitchPage()
    {
        for(int i = 0; i < 5; i++)
        {
            if (i == pageNumber)
            {
                tutorialPage[pageNumber].SetActive(true);
            }
            else
            {
                tutorialPage[i].SetActive(false);
            }
        }
        previousButton.SetActive(true);
        nextButton.SetActive(true);

        if (pageNumber == 0)
        {
            previousButton.SetActive(false);
        }
        else if (pageNumber == 4)
        {
            nextButton.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        
        tutorialCanvas.SetActive(false);
        openningCanvas.SetActive(true);
    }

    public void EnterTutorial()
    {
        pageNumber = 0;
        tutorialCanvas.SetActive(true);
        openningCanvas.SetActive(false);
        SwitchPage();
    }
}

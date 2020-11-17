using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] mClickables;
    public GameObject mNextItem;
    public int mItemsClicked;
    public LevelLoader mLvlLoader;
    public GameObject mDialogPanel, mFinishPanel
        ;
    public AudioSource mClickSounder;
    public AudioClip mGoodClick;
    public AudioClip mBadClick;



    // Start is called before the first frame update
    void Start()
    {
        mClickSounder = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<SetText>().addName();

        mItemsClicked = 0;

        for(int i = 0; i < mClickables.Length; i++)
        {
            mClickables[i].GetComponent<Clickable>().mItemNumber = i;
            mClickables[i].SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (mClickables[0].GetComponent<Clickable>().mIsClicked)
        {
            mDialogPanel.SetActive(false);
        }


        //if the user is picking numbers
      
        
        if (mItemsClicked < mClickables.Length)
        {
            if (mClickables[mItemsClicked].GetComponent<Clickable>().mIsClicked)
            {
                StartCoroutine(mClickables[mItemsClicked].GetComponent<Clickable>().FadeOut());
                mClickSounder.clip = mGoodClick;
                mClickSounder.Play();
                mItemsClicked++;
            }
        }
        else
        {
            mFinishPanel.SetActive(true);
        }

        //Debug.Log(mItemsClicked);
        


    }



    public void ActivateClickables()
    {
        for (int i = 0; i < mClickables.Length; i++)
        {
            
            mClickables[i].SetActive(true);
        }
    }

 
    public void ResetClickables()
    {
        mClickSounder.clip = mBadClick;
        mClickSounder.Play();

        for(int i = 0; i < mClickables.Length; i++)
        {
            mClickables[i].SetActive(true);
            mClickables[i].GetComponent<Clickable>().SetIsClicked(false);
            mClickables[i].GetComponent<Clickable>().ResetColor();
        }
        mItemsClicked = 0;
    }



}

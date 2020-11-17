using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public bool mIsClicked;
    public int mItemNumber;
    public float mFadeOutSpeed;
    public Vector3 mNormalSize, mEnlargedSize;
    GameManager mGameManager;
    public Color mColorOne, mColorTwo;
    SpriteRenderer mSR;
    
    


    // Start is called before the first frame update
    void Start()
    {
        mSR = gameObject.GetComponent<SpriteRenderer>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mIsClicked = false;
        mNormalSize = gameObject.transform.localScale;
        mEnlargedSize = mNormalSize * 1.5f;
        
    }

    private void OnMouseDown()
    {
        if (mItemNumber == mGameManager.mItemsClicked)
        {
           
            mIsClicked = true;
         
        }   
        else
        {
           
            mGameManager.ResetClickables();
        }
          
            
    }

    public void SetIsClicked(bool isClicked)
    {
        mIsClicked = isClicked;
    }


    public void OnMouseOver()
    {
        //mSR.color = mColorTwo;
        gameObject.transform.localScale = mEnlargedSize;
    }


    public void OnMouseExit()
    {
        mSR.color = mColorOne;
        gameObject.transform.localScale = mNormalSize;
    }

    public void ResetColor()
    {
        mSR.color = mColorOne;
    }


    public IEnumerator FadeOut()
    {
        
        for (int i = 0; i < 100; i++)
        {
            float alpha = (100.0f - i) / 100.0f;
            Color temp = new Color(1, 1, 1, alpha);
           
            gameObject.transform.GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(mFadeOutSpeed);
        }

        gameObject.SetActive(false);
        yield return null;
    }

}

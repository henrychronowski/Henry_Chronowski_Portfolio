using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Player player;
    int mCurrentScene;

    public Sprite cursor;

    // Start is called before the first frame update
    void Start()
    {
        //Texture2D cursor = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        //Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
        //                                 (int)sprite.textureRect.y,
        //                                 (int)sprite.textureRect.width,
        //                                 (int)sprite.textureRect.height);

        //cursor.SetPixel(pixels);
        //cursor.Apply();
        //Cursor.SetCursor(sprite.texture, new Vector2(0, 0), CursorMode.Auto);
        player.SetName("");
        mCurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel()
    {
        if (mCurrentScene <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(mCurrentScene + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
            player.SetName("");
        }
        
    }
   
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    public void ReloadLevel()
    {
        LoadLevel(mCurrentScene);
    }
}

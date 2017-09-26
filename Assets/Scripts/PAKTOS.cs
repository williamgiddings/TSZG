using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PAKTOS : MonoBehaviour {
   
    bool  isLoading;
    Transform[] hideStuff;
    Transform[] showStuff;

    void Start (){
        UnityEngine.Cursor.visible = false;
    }

    void Update (){
        if (Input.anyKey && !isLoading)
        {
            for (int hideIt = 0; hideIt < hideStuff.Length; hideIt++)
            {
                hideStuff[hideIt].gameObject.SetActive(false);
            }
            for (int showIt = 0; showIt < showStuff.Length; showIt++)
            {
                showStuff[showIt].gameObject.SetActive(true);
            }
            Load();

        
        }
    }

    void Load (){
        if (!isLoading)
        {
            isLoading = true;
            SceneManager.LoadSceneAsync("Test_Scene", LoadSceneMode.Single);
        }
   
    }
}
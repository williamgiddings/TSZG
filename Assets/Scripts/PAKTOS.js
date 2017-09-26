import UnityEngine.SceneManagement;

var isLoading : boolean;
var hideStuff : Transform[];
var showStuff : Transform[];

function Start ()
{
    UnityEngine.Cursor.visible = false;
}

function Update()
{
    if (Input.anyKey && !isLoading)
    {
        for (var hideIt = 0; hideIt < hideStuff.length; hideIt++)
        {
            hideStuff[hideIt].gameObject.SetActive(false);
        }
        for (var showIt = 0; showIt < showStuff.length; showIt++)
        {
            showStuff[showIt].gameObject.SetActive(true);
        }
        Load();

        
    }
}

function Load()
{
    if (!isLoading)
    {
        isLoading = true;
        SceneManager.LoadSceneAsync("Test_Scene", LoadSceneMode.Single);
    }
   
}
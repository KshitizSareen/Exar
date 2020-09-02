using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }
    public void setAsset(string AssetName)
    {
        if(Application.internetReachability==NetworkReachability.NotReachable)
        {
            Panel.SetActive(true);
            return;
        }
        else
        {
            Panel.SetActive(false);
        }
        Debug.Log(AssetName);
        LoadAssets.AssetName = AssetName;
        SceneManager.LoadScene("GameScene");
        
    }
    public void HidePanel()
    {
        Panel.SetActive(false);
    }
}

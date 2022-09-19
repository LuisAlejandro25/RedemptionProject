using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels;
    // Start is called before the first frame update
    void Start()
    {
        ShowPanel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CleanUI(){
        if(panels.Length <= 0)
            return;
        for(int i = 0; i < panels.Length;i++){
            panels[i].SetActive(false);
        }
    }

    public void ShowPanel(int index){
        if(panels.Length>index){
            CleanUI();
            panels[index].SetActive(true);
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene(1);
    }

    public void ExitGame(){
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels;
    private bool initPanel;
    [SerializeField]
    private AudioMixer audioMaster;
    [SerializeField]
    private Slider[] slidersRef;
    private float auxVolume = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        initPanel = true;
        ShowPanel(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!initPanel)
            return;
        if(Input.GetKeyDown(KeyCode.Space)){
            initPanel = false;
            ShowPanel(1);
        }
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
            if(index == 0)
                initPanel = true;
            if(index == 3)
                ApplyAudioSettings();
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene(1);
    }

    public void ExitGame(){
        Application.Quit();
    }

    private void ApplyAudioSettings(){
        if(audioMaster.GetFloat("master", out auxVolume))
            slidersRef[0].value = auxVolume;
        if(audioMaster.GetFloat("music", out auxVolume))
            slidersRef[1].value = auxVolume;
        if(audioMaster.GetFloat("sfx",out auxVolume))
            slidersRef[2].value = auxVolume;
    }

    public void OnGlobalVolumeChange(float gVolume){
        audioMaster.SetFloat("master",gVolume);
    }

    public void OnMusicVolumeChange(float mVolume){
        audioMaster.SetFloat("music",mVolume);
    }

    public void OnSFXVolumeChange(float sfxVolume){
        audioMaster.SetFloat("sfx",sfxVolume);
    }
}

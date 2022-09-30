using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels;
    [SerializeField]
    private AudioMixer audioMaster;
    [SerializeField]
    private Slider[] slidersRef;
    private float auxVolume = 0.0f;
    private bool pause;
    private bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pause && !end){
            Time.timeScale = 0.0f;
            pause = true;
            ShowPanel(0);
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
            if(index == 1)
                ApplyAudioSettings();
        }
    }

    public void ResumeGame(){
        CleanUI();
        pause = false;
        Time.timeScale = 1.0f;
    }

    public void ExitToMenu(){
        SceneManager.LoadScene(0);
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

    public void EndGame(){
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame(){
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }

    public void FinishTuto(){
        end = true;
        ShowPanel(2);
    }

    IEnumerator ReturnToMenu(){
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(0);
    }
}

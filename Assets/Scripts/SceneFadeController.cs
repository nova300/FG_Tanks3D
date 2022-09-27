using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeController : MonoBehaviour
{
    [SerializeField] Image fadeOutImage;
    private IEnumerator coroutine;
    private bool isActive;

    void OnEnable(){
        isActive = true;
        StartCoroutine(fadeOut(false));
    }


    public IEnumerator fadeOut(bool fade = true, float fadeSpeed = 0.5f){
        Color objectColor = fadeOutImage.color;
        float fadeAmount;
        if(fade){
            while(fadeOutImage.color.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeOutImage.color = objectColor;
                yield return null;
            }
            isActive = false;
        }else {
            while(fadeOutImage.color.a > 0){
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeOutImage.color = objectColor;
                yield return null;
            }
            isActive = false;
        }
    }
    public IEnumerator fadeOutAndLoadScene(string sceneToLoad, bool fade = true, float fadeSpeed = 0.5f){
        if (!isActive){
            isActive = true;
            Debug.Log(isActive);
            yield return fadeOut(fade, fadeSpeed);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

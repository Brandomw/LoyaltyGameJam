using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class flashImage : MonoBehaviour
{
    Image _image = null;
    Coroutine _currentFlashRoutine = null;
    // Start is called before the first frame update
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void Startflash(float secondsforoneflash,float maxAlpha,Color newcolor)
    {
        _image.color = newcolor;

        //ensure maxAlpha isnt above 1
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);
        if (_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(Flash(secondsforoneflash,maxAlpha));
    }

    IEnumerator Flash(float secondsforoneflash,float maxalpha)
    {
        //animate flash in
        float flashInDuration = secondsforoneflash / 2;
        for (float t = 0; t <= flashInDuration ; t+= Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxalpha, t / flashInDuration);
            _image.color = colorThisFrame;
            //wait ttil next frame
            yield return null;
        }
        //animate flash out

        float FlashoutDuration = secondsforoneflash / 2;
        for (float t = 0; t < FlashoutDuration; t+= Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxalpha, 0, t / FlashoutDuration);
            _image.color = colorThisFrame;
            yield return null;

        }

        //ensure alpha set to 0
        _image.color = new Color32(0, 0, 0, 0);
    }
}

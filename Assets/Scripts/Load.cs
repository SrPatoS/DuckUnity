using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        _slider.value = SceneLoader.Instance.LoadProgressValue;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _slider.gameObject.SetActive(true);
            SceneLoader.Instance.LoadScene(1);
        }

        SceneLoader.Instance.OnLoadedScene.AddListener(delegate{_slider.gameObject.SetActive(false);});
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {get; private set;}
    public float LoadProgressValue {get; private set;}

    private float _progressFinalValue;
    private float _progressSpeed = 5f;
    public UnityEvent OnLoadedScene;
    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(LoadProgressValue != _progressFinalValue)
            LoadProgressValue = Mathf.Lerp(LoadProgressValue, _progressFinalValue, _progressSpeed * Time.deltaTime);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        LoadProgressValue = 0f;
        _progressFinalValue = 0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while(!operation.isDone)
        {
            _progressFinalValue = operation.progress;
            yield return null;
        }

        OnLoadedScene.Invoke();
    }
}

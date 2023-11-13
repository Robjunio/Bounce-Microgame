using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject transitionObj;
    private Animator transitionAnimator;
    
    private const string FadeIn = "FadeIn";
    private const string FadeOut = "FadeOut";

    public static ChangeScene Instance;

    private int counter;

    private void Awake()
    {
        if (FindObjectsOfType<ChangeScene>().Length > 1)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        transitionAnimator = GetComponent<Animator>();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameWithTransition());
    }

    public void ChangeSceneTransition(string scene)
    {
        StartCoroutine(ChangeSceneWithTransition(scene));
    }

    IEnumerator StartGameWithTransition()
    {
        yield return FadeTransition(FadeIn);
        
        transitionObj.SetActive(true);
        transitionAnimator.Play("Idle");
        
        yield return SceneManager.LoadSceneAsync("Bounce-Gameplay");

        yield return FadeTransition(FadeOut);
    }

    public IEnumerator StartGameOver()
    {
        yield return FadeTransition(FadeIn);
        
        transitionObj.SetActive(true);
        transitionAnimator.Play("Idle");
    }

    IEnumerator ChangeSceneWithTransition(string scene)
    {
        yield return SceneManager.LoadSceneAsync(scene);

        yield return FadeTransition(FadeOut);
    }
    
    
    IEnumerator FadeTransition(string type)
    {
        transitionObj.SetActive(true);
        transitionAnimator.Play(type);

        while ((transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
        {
            yield return null;
        }

        transitionObj.SetActive(false);
    }
}

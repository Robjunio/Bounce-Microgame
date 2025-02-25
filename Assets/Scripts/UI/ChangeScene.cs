using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject transitionObj;
    [SerializeField] private TextMeshProUGUI text;
    
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

    public async void StartGame()
    {
        await StartGameWithTransition();
    }

    public async void ChangeSceneTransition(string scene)
    {
        await ChangeSceneWithTransition(scene);
    }

    private async Task StartGameWithTransition()
    {
        await FadeTransition(FadeIn);

        text.gameObject.SetActive(true);

        await LoadSceneAsync("Bounce-Gameplay");

        text.gameObject.SetActive(false);

        await FadeTransition(FadeOut);
    }

    public async Task StartGameOver()
    {
        await FadeTransition(FadeIn);
    }

    private async Task ChangeSceneWithTransition(string scene)
    {
        text.gameObject.SetActive(true);

        transitionObj.SetActive(true);

        await LoadSceneAsync(scene);

        text.gameObject.SetActive(false);

        await FadeTransition(FadeOut);
    }

    private async Task LoadSceneAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        var tcs = new TaskCompletionSource<bool>();

        operation.completed += _ => tcs.SetResult(true);

        await tcs.Task;
    }


    private async Task FadeTransition(string type)
    {
        transitionObj.SetActive(true);

        await transitionObj.transform.DOScale(type == FadeIn ? 250 : 0, 1f).AsyncWaitForCompletion();

        transitionObj.SetActive(type == FadeIn ? true : false);
    }
}

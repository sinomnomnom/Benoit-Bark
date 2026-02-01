using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ComicIntroController : MonoBehaviour
{
    [Header("Comic Panels")]
    public Sprite[] panels;

    [Header("UI")]
    public Image panelImage;
    public CanvasGroup canvasGroup;
    public CanvasGroup backgroundCanvasGroup;
    public AudioSource audioSource;
    public AudioClip music;

    [Header("Input")]
    public KeyCode advanceKey = KeyCode.Space;

    private int currentIndex = 0;

    void Start()
    {
        audioSource.PlayOneShot(music);
        if (panels.Length == 0)
        {
            Debug.LogError("No comic panels assigned!");
            return;
        }

        panelImage.sprite = panels[currentIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(advanceKey))
        {
            AdvancePanel();
        }
    }

    void AdvancePanel()
    {
        currentIndex++;
        

        if (currentIndex >= panels.Length)
        {
            EndComic();
        }
        else
        {
            StartCoroutine(FadePanel(panels[currentIndex]));
        }
    }

    IEnumerator FadePanel(Sprite next)
    {
        for (float t = 1; t > 0; t -= Time.deltaTime * 4)
        {
            canvasGroup.alpha = t;
            yield return null;
        }

        if (next != null)
        {
            panelImage.sprite = next;
        }
        else
        {
            yield break;
        }

        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            canvasGroup.alpha = t;
            yield return null;
        }
    }

    async Task FadeOut()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            backgroundCanvasGroup.alpha = t;
            await Task.Yield();
        }
    }

    async void EndComic()
    {
        StartCoroutine(FadePanel(null));
        await Task.Delay(2000);
        await FadeOut();
        audioSource.Stop();
        // Hide comic UI
        panelImage.gameObject.SetActive(false);

        // Start your game here
        // Example:
        // Services.GameController.StartGame();
        // or SceneManager.LoadScene("MainScene");

        Debug.Log("Comic finished");
        gameObject.SetActive(false);
    }
}
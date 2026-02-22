using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int totalBallCount;
    [SerializeField] private int requiredStarCount;
    
    // Win Panel
    [SerializeField] GameObject WinPanel;
    public Image[] stars;          
    public RectTransform nextBtn;
    public RectTransform replayBtn;

    // Lose Panel
    [SerializeField] GameObject LosePanel;
    [SerializeField] RectTransform loseReplayBtn;
    [SerializeField] RectTransform loseImage;

    [HideInInspector] public bool IsGameFinish;
    private int currentStarCount;
  
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                RopeManager rope = hit.collider.GetComponentInParent<RopeManager>();

                if (rope != null)
                {
                    rope.CutFromJoint(hit.collider.gameObject);
                }
            }
        }
    }

    public void OnTargetReached()
    {
        if (IsGameFinish) return;

        IsGameFinish = true;
        StartCoroutine(ShowWinPanel());     
    }

    public void OnFailAreaEntered()
    {
        if (IsGameFinish) return;

        IsGameFinish = true;
        StartCoroutine(ShowLosePanel());
    }

    public void AddDiamond()
    {
        currentStarCount++;
    }

    private IEnumerator ShowWinPanel()
    {
        WinPanel.SetActive(true);

        RectTransform panelRT = WinPanel.GetComponent<RectTransform>();
        panelRT.localScale = Vector3.zero;
        yield return panelRT.DOScale(1f, 0.5f)
                            .SetEase(Ease.OutBack)
                            .WaitForCompletion();

        yield return new WaitForSeconds(0.2f);

        // Stars
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].transform.localScale = Vector3.zero;
            stars[i].color = new Color(1, 1, 1, 0);

            if (i < currentStarCount)
            {
                stars[i].transform
                    .DOScale(4f, 1f)
                    .SetEase(Ease.OutBack);

                stars[i]
                    .DOFade(1f, 0.3f);
            }
            else
            {
                // Fade out uncollected star
                stars[i].DOFade(0.3f, 0.2f);
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);

        // Buttons
        nextBtn.localScale = Vector3.zero;
        replayBtn.localScale = Vector3.zero;

        nextBtn.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.1f);

        replayBtn.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    private IEnumerator ShowLosePanel()
    {
        LosePanel.SetActive(true);
        RectTransform panelRT = LosePanel.GetComponent<RectTransform>();
        panelRT.localScale = Vector3.zero;

        yield return panelRT.DOScale(1f, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        
        yield return new WaitForSeconds(0.2f);
        loseReplayBtn.localScale = Vector3.zero;
        loseImage.localScale = Vector3.zero;

        yield return new WaitForSeconds(0.2f);
        loseImage.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.5f);
        loseReplayBtn.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }


    public void OnClickButton(string ActionType)
    {
        switch (ActionType)
        {
            case "NextLevel":
                int currentIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentIndex + 1);
                PlayerPrefs.SetInt("CurrentLevel", currentIndex + 1);
                break;
            case "RestartLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Quit":
                Application.Quit();
                break;
        } 
    }


}

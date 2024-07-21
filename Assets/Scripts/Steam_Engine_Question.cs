using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import for TextMeshPro

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public Button[] optionButtons;
    public GameObject[] models; 
    public TMP_Text Text;
    public GameObject Question; // Added declaration for Question object

    private Renderer[] modelRenderers;
    private float fadeSpeed = 0.5f;
    private bool correctlyAnswered = false;

    //public TextMeshProUGUI scoreText; // Reference to the TextMeshPro element
    //private int score = 0; 

    void Start()
    {
        Text.gameObject.SetActive(false);

        modelRenderers = new Renderer[models.Length];
        for (int i = 0; i < models.Length; i++)
        {
            modelRenderers[i] = models[i].GetComponentInChildren<Renderer>();
            if (modelRenderers[i] != null)
            {
                SetAlpha(modelRenderers[i], 0);
            }
            else
            {
                Debug.LogError("Renderer not found in model: " + models[i].name);
            }
        }

        SetupQuiz();
        //UpdateScoreDisplay(); // Initialize score display
    }

    void SetupQuiz()
    {
        optionButtons[0].onClick.AddListener(() => OnOptionSelected(false));
        optionButtons[1].onClick.AddListener(() => OnOptionSelected(true));
        optionButtons[2].onClick.AddListener(() => OnOptionSelected(false));
        optionButtons[3].onClick.AddListener(() => OnOptionSelected(false));
    }

    void OnOptionSelected(bool isCorrect)
    {
        if (isCorrect && !correctlyAnswered)
        {
            //score++; // Increase score by 1
            //UpdateScoreDisplay(); // Call method

            StartCoroutine(RevealModels());
            correctlyAnswered = true;
            Text.gameObject.SetActive(true);
            Question.SetActive(false);
            foreach (Button button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Wrong answer!");
        }
    }

    System.Collections.IEnumerator RevealModels()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            foreach (Renderer renderer in modelRenderers)
            {
                if (renderer != null)
                {
                    SetAlpha(renderer, alpha);
                    Debug.Log(alpha);
                }
            }
            yield return null;
        }
    }

    void SetAlpha(Renderer renderer, float alpha)
    {
        foreach (Material mat in renderer.materials)
        {
            Color color = mat.color;
            color.a = Mathf.Clamp01(alpha);
            mat.color = color;
        }
    }

    //void UpdateScoreDisplay()
    //{
    //    scoreText.text = "Score: " + score.ToString();
    //}
}


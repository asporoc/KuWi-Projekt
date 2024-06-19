using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public TMP_Text questionText;
    public Button[] optionButtons;
    public GameObject[] models; // Array of models to be revealed
    private Renderer[] modelRenderers;
    private float fadeSpeed = 0.5f;

    void Start()
    {
        // Initialize the model renderers and set their alpha to 0
        modelRenderers = new Renderer[models.Length];
        for (int i = 0; i < models.Length; i++)
        {
            modelRenderers[i] = models[i].GetComponent<Renderer>();
            SetAlpha(modelRenderers[i], 0);
        }

        // Set up quiz question and options
        SetupQuiz();
    }

    void SetupQuiz()
    {
        questionText.text = "What is the capital of France?";
        optionButtons[0].GetComponentInChildren<TMP_Text>().text = "Berlin";
        optionButtons[1].GetComponentInChildren<TMP_Text>().text = "Paris";
        optionButtons[2].GetComponentInChildren<TMP_Text>().text = "Madrid";

        // Assign button click events
        optionButtons[0].onClick.AddListener(() => OnOptionSelected(false));
        optionButtons[1].onClick.AddListener(() => OnOptionSelected(true));
        optionButtons[2].onClick.AddListener(() => OnOptionSelected(false));
    }

    void OnOptionSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            // Correct answer, start revealing models
            StartCoroutine(RevealModels());
        }
        else
        {
            // Wrong answer, show feedback or retry logic
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
                SetAlpha(renderer, alpha);
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
}

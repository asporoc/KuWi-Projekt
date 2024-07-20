using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import für textMeshPro

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public Button[] optionButtons;
    public GameObject[] models; 
    private Renderer[] modelRenderers;
    private float fadeSpeed = 0.5f;
    private bool correctlyanswered = false;
    
    //public TextMeshProUGUI scoreText; // Referenz auf das TextMeshPro-Element
    //private int score = 0; 

    void Start()
    {
        
        modelRenderers = new MeshRenderer[models.Length];
        for (int i = 0; i < models.Length; i++)
        {
            modelRenderers[i] = models[i].GetComponentInChildren<MeshRenderer>();
            SetAlpha(modelRenderers[i], 0);
            
        }

        SetupQuiz();
    //    UpdateScoreDisplay(); //Score-Anzeige initialisieren
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
        if (isCorrect && !correctlyanswered)
        {
        //    score++; // Punktzahl um 1 erhöht
         //   UpdateScoreDisplay(); // Methodenaufruf

            StartCoroutine(RevealModels());
            correctlyanswered = true;
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
                SetAlpha(renderer, alpha);
                Debug.Log(alpha);
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

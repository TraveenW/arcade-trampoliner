using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] float activateElementDelay = 1;
    [SerializeField] GameObject sceneTransition;

    [Header("Points Counter")]
    [SerializeField] GameObject pointsInput;
    [SerializeField] GameObject pointsOutput;

    [Header("Other UI Elements")]
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject restartPrompt;

    GameObject[] textUIChildren;
    List<TextMeshProUGUI> textUI;

    // Start is called before the first frame update
    void Start()
    {
        textUI = new List<TextMeshProUGUI>();

        textUIChildren = new GameObject[] { gameOverText, pointsOutput, restartPrompt };
        foreach (GameObject u in textUIChildren)
        {
            textUI.Add(u.GetComponent<TextMeshProUGUI>());
        }

        StartCoroutine(ExecuteGameOver());
    }

    // Coroutine for handling the game over screen and restarting the game
    IEnumerator ExecuteGameOver()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(activateElementDelay);

        // Game Over screen elements
        sceneTransition.SetActive(true);
        GetComponent<UIImageFade>().FadeImage(1);
        pointsOutput.GetComponent<TMP_Text>().text = "Score: " + pointsInput.GetComponent<PointSystem>().pointNumber;
        foreach (TextMeshProUGUI line in textUI)
        {
            yield return new WaitForSecondsRealtime(activateElementDelay);
            line.color = new Color(line.color.r, line.color.g, line.color.b, 1);
        }

        // Wait for mouse input, then restart scene
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1));
        sceneTransition.GetComponent<SceneTransition>().ReloadScene();
    }
}

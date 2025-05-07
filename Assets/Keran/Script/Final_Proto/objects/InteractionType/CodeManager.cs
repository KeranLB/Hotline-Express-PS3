using System.Collections;
using TMPro;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    [SerializeField] private string _finalCode;
    [HideInInspector] public string currentCode;
    [HideInInspector] public bool isCorrect = false;
    [SerializeField] private TextMeshPro text;

    [Header("UI Texts (optionnel)")]
    [SerializeField] private TextMeshPro messageText;  // 3D TextMeshPro dans la scène;
    [SerializeField] private string defaultMessage = "Entrez le code.";
    [SerializeField] private string correctMessage = "Code correct !";
    [SerializeField] private string incorrectMessage = "Code incorrect.";

    private void Start()
    {
        if (messageText != null)
            messageText.text = defaultMessage;
    }

    public void AddToCode(int newValue)
    {
        if (!isCorrect && currentCode.Length < 4)
        {
            currentCode += newValue.ToString();
            text.text = currentCode;

            if (currentCode.Length == 4)
            {
                StartCoroutine(VerifCode());
            }
        }
    }

    IEnumerator VerifCode()
    {
        yield return new WaitForSeconds(1);

        if (currentCode == _finalCode)
        {
            isCorrect = true;
            text.text = "Correct";
            if (messageText != null) messageText.text = correctMessage;
        }
        else
        {
            text.text = "False";
            if (messageText != null) messageText.text = incorrectMessage;
            yield return new WaitForSeconds(1);
            currentCode = "";
            text.text = "";
            if (messageText != null) messageText.text = defaultMessage;
        }
    }
}
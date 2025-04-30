using System.Collections;
using TMPro;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    [SerializeField] private string _finalCode;
    [HideInInspector] public string currentCode;
    [HideInInspector] public bool isCorrect = false;
    [SerializeField] private TextMeshPro text;

    public void AddToCode(int newValue)
    {
        currentCode += newValue.ToString();
        text.text = currentCode;
        StartCoroutine(VerifCode());
    }

    IEnumerator VerifCode()
    {
        yield return new WaitForSeconds(1);
        if (currentCode.Length == 4)
        {
            if (currentCode == _finalCode)
            {
                isCorrect = true;
            }
            else
            {
                currentCode = "";
                text.text = currentCode;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    [SerializeField] private string finalCode;
    [HideInInspector] public List<int> currentCode;
    [HideInInspector] public string verifStringCurrentCode;
    [HideInInspector] public string stringCurrentCode;
    [HideInInspector] public bool isCorrect = false;
    [SerializeField] private TextMeshPro text;

    private void Start()
    {
        text.text = "_ _ _ _ ";
    }
    public void AddToCode(int newValue)
    {
        if (!isCorrect && currentCode.Count < 4)
        {
            currentCode.Add(newValue);
            stringCurrentCode = "";
            foreach (int code in currentCode)
            {
                stringCurrentCode += code.ToString() + " ";
            }
            verifStringCurrentCode += newValue.ToString();
            for (int code = currentCode.Count; code < 4; code++)
            {
                stringCurrentCode += "_ ";
            }
            text.text = stringCurrentCode;
            StartCoroutine(VerifCode());
        }
    }

    IEnumerator VerifCode()
    {
        yield return new WaitForSeconds(1);
        if (currentCode.Count == 4)
        {
            if (verifStringCurrentCode == finalCode)
            {
                isCorrect = true;
                text.text = "correct";
            }
            else
            {
                verifStringCurrentCode = "";
                currentCode.Clear();
                stringCurrentCode = "_ _ _ _ ";
                text.text = stringCurrentCode;
            }
        }
    }
}
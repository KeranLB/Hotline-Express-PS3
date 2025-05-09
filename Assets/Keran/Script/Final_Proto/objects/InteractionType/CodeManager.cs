using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    [SerializeField] private string _finalCode;
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

    /*
    public void AddToCode(int newValue)
    {
        if (!isCorrect && currentCode.Length < 4)
        {
            currentCode += newValue.ToString();
            text.text = currentCode;
            StartCoroutine(VerifCode());
        }
    }
    */
    IEnumerator VerifCode()
    {
        yield return new WaitForSeconds(1);
        if (currentCode.Count == 4)
        {
            Debug.Log(verifStringCurrentCode);
            if (verifStringCurrentCode == _finalCode)
            {
                Debug.Log("is correct");
                isCorrect = true;
                text.text = "correct";
            }
            else
            {
                stringCurrentCode = "_ _ _ _ ";
                text.text = stringCurrentCode;
            }
        }
    }
}

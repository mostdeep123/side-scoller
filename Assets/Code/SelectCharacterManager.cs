using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class SelectCharacterManager : MonoBehaviour
{
    [Header("Properties")]
    public int characterSelectIndex;
    public int maxCharacterIndex;
    public List<Transform> characters = new List<Transform>();


    [Header("Animation Properties")]
    public float moveDistance = 600f;
    public float moveDuration = 0.4f;
    private bool isAnimating = false;


    [Header("Selection Properties")]
    public string characterSelectionKey;
    public List<string> charactersKey = new List<string>();

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputChangeIndex();
    }
    
    void InputChangeIndex ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(characterSelectIndex < maxCharacterIndex)
            {
                characterSelectIndex++;
                AnimateSelection(moveDistance, false);
                KeyShiftRight();
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(characterSelectIndex > 0)
            {
                characterSelectIndex--;
                AnimateSelection(-moveDistance, true);
                KeyShiftLeft();
            }
        }
    }

     void AnimateSelection(float offset, bool toLeft)
    {
        isAnimating = true;

        int finished = 0;
        foreach (var card in characters)
        {
            Vector3 targetPos = card.localPosition + new Vector3(offset, 0, 0);
            LeanTween.moveLocal(card.gameObject, targetPos, moveDuration)
                     .setEaseOutQuad()
                     .setOnComplete(() =>
                     {
                         finished++;
                         if (finished == characters.Count)
                         {
                             Rearrange(toLeft);
                             isAnimating = false;
                         }
                     });
        }
    }

    void Rearrange(bool toLeft)
    {
        if (toLeft)
        {
            Transform first = characters[0];
            characters.RemoveAt(0);
            characters.Add(first);
        }
        else
        {
            Transform last = characters[characters.Count - 1];
            characters.RemoveAt(characters.Count - 1);
            characters.Insert(0, last);
        }

        for (int i = 0; i < characters.Count; i++)
        {
            float newX = (i - 1) * moveDistance;
            characters[i].localPosition = new Vector3(newX, 0, 0);
        }

        characters[0].transform.localScale = Vector3.one;
        characters[1].transform.localScale = new Vector3(
            1.2f,
            1.2f,
            1.2f
        );
        characters[2].transform.localScale = Vector3.one;
    }

    void KeyShiftRight()
    {
        string leftKey = charactersKey[0];
        string centerKey = charactersKey[1];
        string rightKey = charactersKey[2];

        charactersKey[0] = rightKey;
        charactersKey[1] = leftKey;
        charactersKey[2] = centerKey;

        characterSelectionKey = charactersKey[1];
        PlayerPrefs.SetString("character", characterSelectionKey);
    }

    void KeyShiftLeft ()
    {
        string leftKey = charactersKey[0];
        string centerKey = charactersKey[1];
        string rightKey = charactersKey[2];

        charactersKey[0] = centerKey;
        charactersKey[1] = rightKey;
        charactersKey[2] = leftKey;

        characterSelectionKey = charactersKey[1];
        PlayerPrefs.SetString("character", characterSelectionKey);
    }
}

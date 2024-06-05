using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RectTransform nameTag;
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 4.5f, moveAccuracy = 0.15f;
    public AnimationData[] playerAnimations;

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point){
        Vector2 positionDifference = point - (Vector2)myObject.position;

        //flip object
        if (myObject.GetComponentInChildren<SpriteRenderer>() && positionDifference.x != 0)
            myObject.GetComponentInChildren<SpriteRenderer>().flipX = positionDifference.x > 0;
        

        while (positionDifference.magnitude > moveAccuracy)
        {
            myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
            positionDifference= point - (Vector2)myObject.position;
            yield return null;
        }
        myObject.position = point;
        
        if (myObject == FindObjectOfType<ClickManager>().player){
            FindObjectOfType<ClickManager>().playerWalking = false;
        }
        
        yield return null;
    }

    public void ChangeScene(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }

    public void UpdateNameTag(ItemData item){
        
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        //change size
        nameTag.sizeDelta = item.nameTagSize;
        //move tag
        nameTag.localPosition = new Vector2(item.nameTagSize.x/2, -0.5f);
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    public bool playerWalking= true;
    public Transform player;
    GameManager gameManager;

    float goToClickMaxY = 1.7f;
    Camera myCamera;
    Coroutine goToClickCoroutine;

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
            goToClickCoroutine = StartCoroutine(GoToClick(Input.mousePosition));
    }

    public IEnumerator GoToClick(Vector2 mousePos)
    {
        //wait to make room for GoToItem() checks
        yield return new WaitForSeconds(0.05f);

        Vector2 targetPos = myCamera.ScreenToWorldPoint(mousePos);
        if (targetPos.y > goToClickMaxY || playerWalking)
            yield break;

        //start walking
        playerWalking = true;
        StartCoroutine(gameManager.MoveToPoint(player, targetPos));
        //play animation
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
        //stop walking
        StartCoroutine(CleanAfterClick());
    }
    private IEnumerator CleanAfterClick()
    {
        while (playerWalking)
            yield return new WaitForSeconds(0.05f);
        player.GetComponent<SpriteAnimator>().PlayAnimation(null);
        yield return null;
    }


    private void Start(){
        gameManager = FindObjectOfType<GameManager>();
        myCamera = GetComponent<Camera>();
    }

    public void GoToItem(ItemData item){
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
        playerWalking = true;
        TryGettingItem(item);
    }

    private void TryGettingItem(ItemData item){
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);

        if (canGetItem){
            GameManager.collectedItems.Add(item.itemID);
            Debug.Log("Spoke to Guard");
            
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
        
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem){
        while (playerWalking){
            yield return new WaitForSeconds(0.05f);
        }

        if(canGetItem){
            foreach(GameObject g in item.objectsToRemove){
                Destroy(g); 
            }
        }

        player.GetComponent<SpriteAnimator>().PlayAnimation(null);
        yield return null;

    }
}
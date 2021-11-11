using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChickScript : MonoBehaviour
{
    [SerializeField] Transform[] owlLocation;
    [SerializeField] float moveSpeed;
    [SerializeField] float timeOfIdling = 1f;
    bool timeToMove = true;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Vector3 xPos = new Vector3(owlLocation[1].position.x, transform.position.y);
        MoveOwl(xPos);
    }
    void MoveOwl(Vector3 newPosition)
    {
        animator.SetBool("Hop", true);
        if(transform != null){
            transform.DOMove(newPosition, moveSpeed).SetEase(Ease.Linear).OnComplete((() =>{
            if(timeToMove){
                timeToMove = !timeToMove;
                int index = 0;
                Vector3 xPos = new Vector3(owlLocation[index].position.x, transform.position.y);
                transform.localScale = new Vector3(-1,1,1);
                StartCoroutine(Idling(index));

            }else{
                timeToMove = !timeToMove;
                int index = 1;
                Vector3 xPos = new Vector3(owlLocation[index].position.x, transform.position.y);
                transform.localScale = new Vector3(1,1,1);
                StartCoroutine(Idling(index));

            }
         })).Play();
        }
         
    }

    IEnumerator Idling(int indexOfPos)
    {
        Vector3 xPos = new Vector3(owlLocation[indexOfPos].position.x, transform.position.y);
        animator.SetBool("Hop", false);
        yield return new WaitForSeconds(timeOfIdling);
        MoveOwl(xPos);
    }


}

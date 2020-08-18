using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class CommentsLayout : MonoBehaviour
{
    public GameObject CommentElement;
    public GameObject CommentElementParent;
    public GameObject CreateCommentLayout;
    public GameObject CreateCommentButton;
    public GameObject NoCommentsPanel;
    private List<GameObject> commentObjects = new List<GameObject>();

    public static int EventID;

    public void ShowComments(List<ReviewData> commentsInfos)
    {
        CreateCommentButton.SetActive(Links.DeviceInformation.isParticipating(EventID));
        NoCommentsPanel.SetActive(commentsInfos.Count < 1);
        foreach (var obj in commentObjects)
        {
            Destroy(obj);
        }

        commentObjects.Clear();

        foreach (ReviewData comment in commentsInfos)
        {
            GameObject tmp = Instantiate(CommentElement, CommentElementParent.transform);
            tmp.GetComponent<CommentElement>().CommentText.text = comment.review;
            tmp.GetComponent<CommentElement>().NicknameText.text = comment.nickname;
            commentObjects.Add(tmp);
        }

        gameObject.SetActive(true);
        StartCoroutine(delayAnimation());
    }


    IEnumerator delayAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        if (commentObjects.Count > 0)
        {
            commentObjects[0].SetActive(false);
            yield return new WaitForSeconds(0.1f);
            commentObjects[0].SetActive(true);
        }

        yield return null;
    }

    public void AddCommentPressed()
    {
        CreateCommentLayout.GetComponent<CreateCommentLayout>().ShowLayout(EventID);
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace UI_scripts
{
    public class CreateCommentLayout : MonoBehaviour
    {
        public Text NewCommentText;

        private int eventID;
        
        public void ShowLayout(int EventID)
        {
            gameObject.SetActive(true);
            eventID = EventID;
        }
        
        public void CreateComment()
        {
            //300 символов
            if (NewCommentText.text.Length < 5)
            {
                Links.ToastController.Show("Отзыв должен содержать 5 символов");
                return;
            }
            Links.RequestController.RequsetAddComment(NewCommentText.text, eventID);
            gameObject.SetActive(false);
            Links.MainMenuController.CommentsLayout.SetActive(false);
        }
    }
}
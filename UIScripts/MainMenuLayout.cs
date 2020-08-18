using UnityEngine;
using UnityEngine.UI;

namespace UI_scripts
{
    public class MainMenuLayout : MonoBehaviour
    {
        public Text scoreText;
        public Text ratingText;


        // Start is called before the first frame update
        private void OnEnable()
        {
            Links.MainMenuLayout = this;
            scoreText.text = Links.DeviceInformation.PlayerData.Money.ToString();
            ratingText.text = Links.DeviceInformation.PlayerData.Rating.ToString();
            Links.RequestController.RequestPlayerEvents();
            Links.RequestController.RequestPlayerUpdate();
        }
    }
}
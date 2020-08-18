using UnityEngine;

namespace UI_scripts
{
    public class BottomPanelScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }


        public void WarmapLayoutButtonClicked()
        {
         
            Links.MainMenuController.Background.SetActive(false);
            Links.LevelGroup.SetActive(true);
            Links.MainMenuController.CreateEventLayout.gameObject.SetActive(false);
            Links.MainMenuController.ShowWarmapLayout();
            Links.MainMenuController.HideMainMenuLayout();
            Links.MainMenuController.HideEventsLayout();
            Links.MainMenuController.HideSettingsLayout();
            Links.MainMenuController.HideTopPlayersLayout();
            Links.MainMenuController.ShopLayout.gameObject.SetActive(false);
            Links.MainMenuController.HideNotificationsLayout();
            Links.EventInfoLayout.Hide();
        }

        public void MainMenuLayoutButtonClicked()
        {
            Links.MainMenuController.Background.SetActive(true);
            Links.MainMenuController.CreateEventLayout.gameObject.SetActive(false);
            Links.LevelGroup.SetActive(false);
            Links.MainMenuController.ShowMainMenuLayout();
            Links.MainMenuController.HideWarmapLayout();
            Links.MainMenuController.HideEventsLayout();
            Links.MainMenuController.HideSettingsLayout();
            Links.MainMenuController.HideTopPlayersLayout();
            Links.MainMenuController.ShopLayout.gameObject.SetActive(false);
            Links.MainMenuController.HideNotificationsLayout();
            Links.EventInfoLayout.Hide();
        }

        public void EventsLayoutButtonClicked()
        {
            Links.MainMenuController.Background.SetActive(true);
            Links.LevelGroup.SetActive(false);
            Links.MainMenuController.CreateEventLayout.gameObject.SetActive(false);
            Links.MainMenuController.ShowEventsLayout();
            Links.MainMenuController.HideWarmapLayout();
            Links.MainMenuController.HideMainMenuLayout();
            Links.MainMenuController.HideSettingsLayout();
            Links.MainMenuController.HideTopPlayersLayout();
            Links.MainMenuController.ShopLayout.gameObject.SetActive(false);
            Links.MainMenuController.HideNotificationsLayout();
            Links.EventInfoLayout.Hide();
        }
    }
}
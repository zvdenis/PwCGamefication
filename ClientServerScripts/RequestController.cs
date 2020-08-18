using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ClientServerScripts;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class RequestController : MonoBehaviour
{
    private void Awake()
    {
        Links.RequestController = this;
    }

    public void TryLogIn(string login, string password)
    {
        if (!Links.TcpClient.socketAvailible)
        {
            Links.LoginLayoutController.OpenLoginLayoutWithError("No socket connection");
            return;
        }

        AutorizationRequest autorizationRequest = new AutorizationRequest(login, password);
        Links.TcpClient.SendData(autorizationRequest.Serialize());
    }

    public void TryRegister(string login, string password, string nickname)
    {
        if (!Links.TcpClient.socketAvailible)
        {
            Links.LoginLayoutController.OpenRegisterWithError("No socket connection");
            return;
        }

        RegistrationRequest registrationRequest = new RegistrationRequest(nickname, login, password);
        Links.TcpClient.SendData(registrationRequest.Serialize());
    }

    public void MapInfoReceieved(DataInfo dataInfo)
    {
        //TODO
    }

    public void PlayerInfoReceieved(DataInfo dataInfo)
    {
        PlayerData playerInfo = (PlayerData) dataInfo;
    }

    public void RequestLeaderboard()
    {
        RequestInfo requestInfo = new RequestInfo(RequestInfo.RequestType.FirstPlayers);
        Links.TcpClient.SendData(requestInfo.Serialize());
    }

    public void EnterAppRequest()
    {
        RequestInfo requestInfo = new RequestInfo(RequestInfo.RequestType.MainInfo);
        Links.TcpClient.SendData(requestInfo.Serialize());
    }

    public void ResponseInfoReceieved(DataInfo dataInfo)
    {
        ResponseInfo responseInfo = (ResponseInfo) dataInfo;
        Debug.Log("Ответ: " + responseInfo.responseType);
        switch (responseInfo.responseType)
        {
            case ResponseInfo.ResponseType.VisitedOK:
                SyncContext.RunOnUnityThread(() => Links.ToastController.Show("Посещение засчитано!"));
                break;
            case ResponseInfo.ResponseType.ResponseString:
                ResponseString responseString = (ResponseString) responseInfo;
                SyncContext.RunOnUnityThread(() => Links.MainMenuController.QRlayout.ShowLayout(responseString.String));
                break;
            case ResponseInfo.ResponseType.AutorizationOk:
                SyncContext.RunOnUnityThread(Links.LoginLayoutController.OpenMainMenu);
                break;
            case ResponseInfo.ResponseType.ResponseUserInfo:
                ResponseUser tmp = (ResponseUser) responseInfo;

                if (tmp.responseUserType == ResponseUser.ResponseUserType.PurchaseOk)
                {
                    Links.DeviceInformation.PlayerData = tmp.data;
                    //TODO
                    //Отрисовать купленный предмет
                    return;
                }

                if (tmp.responseUserType == ResponseUser.ResponseUserType.DescriptionUpdate)
                {
                    //Обновить описание!!!
                    //TODO
                    //Links.DeviceInformation.PlayerData.Description = ""
                    Links.DeviceInformation.PlayerData = tmp.data;
                    return;
                }

                Links.DeviceInformation.PlayerData = tmp.data;

                Links.LoginLayoutController.OpenMainMenu();
                break;
            case ResponseInfo.ResponseType.AutorizationFalied_WrongLogin:
                SyncContext.RunOnUnityThread(() =>
                    Links.LoginLayoutController.OpenLoginLayoutWithError("Wrong login!"));
                break;
            case ResponseInfo.ResponseType.AutorizationFailed_WrongPassword:
                SyncContext.RunOnUnityThread(() =>
                    Links.LoginLayoutController.OpenLoginLayoutWithError("Wrong password!"));
                break;
            case ResponseInfo.ResponseType.RegistartionFailed_ExistLogin:
                SyncContext.RunOnUnityThread(() =>
                    Links.LoginLayoutController.OpenRegisterWithError("Login already exists!"));
                break;
            case ResponseInfo.ResponseType.ResponseEvents:
                //события на которые записан пользователь
                //TODO

                //actual: все события
                ResponseUserEvent responseUserEvent = (ResponseUserEvent) responseInfo;
                //responseUserEvent.data[0].visited  (не визит а записан)
                Links.DeviceInformation.PlayerEvents = responseUserEvent.data;
                SyncContext.RunOnUnityThread(() => Links.EventsLayout.UpdateList(responseUserEvent.data));
                break;
            case ResponseInfo.ResponseType.ResponseUsers:
                ResponseUsers response = (ResponseUsers) responseInfo;
                switch (response.usersType)
                {
                    case ResponseUsers.UsersType.Name:
                        SyncContext.RunOnUnityThread(() =>
                            Links.FindUserLayout.UpdateList(response.players));
                        break;
                    case ResponseUsers.UsersType.Rating:
                        SyncContext.RunOnUnityThread(() => Links.TopPlayersLayout.UpdateTop(response.players));
                        break;
                }

                break;
            case ResponseInfo.ResponseType.ResponseUserEvents:
                //не Все предстоящие события (хз вообще что это)
                ResponseEvent responseEvent = (ResponseEvent) responseInfo;
                Links.DeviceInformation.FutureEvents = responseEvent.data;
                //SyncContext.RunOnUnityThread(() => Links.EventsLayout.UpdateList(responseEvent.data));
                break;
            case ResponseInfo.ResponseType.ResponseMainInfo:
                //Серверное время
                ResponseMainInfo mainInfo = (ResponseMainInfo) responseInfo;
                SyncContext.RunOnUnityThread(() => Links.CurrentPlayerGameManagerScript.ResponseMainInfo(mainInfo));
                break;
            case ResponseInfo.ResponseType.ResponseCastle:
                ResponseCastle responsedCastle = (ResponseCastle) responseInfo;
                SyncContext.RunOnUnityThread(() => Links.LastOpenedCastle.CastleResponse(responsedCastle));
                break;
            case ResponseInfo.ResponseType.ResponseReviews:
                ResponseReviews responsedReviews = (ResponseReviews) responseInfo;
                if (responsedReviews.responseReviewsType == ResponseReviews.ResponseReviewsType.CastleReviews)
                {
                    SyncContext.RunOnUnityThread(() => Links.LastOpenedCastle.CastleHistoryResponse(responsedReviews));
                }

                if (responsedReviews.responseReviewsType == ResponseReviews.ResponseReviewsType.EventReviews)
                {
                    Links.MainMenuController.CommentsLayout.GetComponent<CommentsLayout>()
                        .ShowComments(responsedReviews.reviews);
                }

                break;
            case ResponseInfo.ResponseType.ResponseUpdate:
                ResponseUpdate responsedUpdate = (ResponseUpdate) responseInfo;

                switch (responsedUpdate.responseUpdateType)
                {
                    case ResponseUpdate.ResponseUpdateType.UpdateEventReviews:
                        //TODO
                        //TODO
                        ResponseReviews reviews = (ResponseReviews) responsedUpdate.updatingData;

                        //обновить список комментов!
                        //reviews.reviews
                        break;

                    case ResponseUpdate.ResponseUpdateType.UpdateCastleReviews:
                        SyncContext.RunOnUnityThread(() =>
                            Links.LastOpenedCastle.CastleUpdateResponse(responsedUpdate));
                        break;
                }

                break;
        }
    }

    public void RequestPutItem(int itemID)
    {
        RequestChangeSubscription changeSubscription = new RequestChangeSubscription(
            Links.DeviceInformation.PlayerData.Id, itemID, RequestChangeSubscription.SubscriptionType.putItem);
        Links.TcpClient.SendData(changeSubscription.Serialize());
    }

    public void RequestDescriptionChange(string newString)
    {
        RequestString requestString = new RequestString(Links.DeviceInformation.PlayerData.Id, newString,
            RequestString.RequestStringType.ChangeUserDescription);
        Links.TcpClient.SendData(requestString.Serialize());
    }

    public void RequestPlayerUpdate()
    {
        RequestString requestString = new RequestString(Links.DeviceInformation.PlayerData.Id, Links.DeviceInformation.PlayerData.Description,
            RequestString.RequestStringType.ChangeUserDescription);
        Links.TcpClient.SendData(requestString.Serialize());
    }
    public void RequestEventSubscription(EventData eventData)
    {
        RequestInfo requestInfo = new RequestChangeSubscription(Links.DeviceInformation.PlayerData.Id, eventData.id,
            RequestChangeSubscription.SubscriptionType.addUserToEvent);
        Links.TcpClient.SendData(requestInfo.Serialize());
        Links.RequestController.RequestFutureEvents();
    }

    public void RequestEventUnsubscription(EventData eventData)
    {
        RequestInfo requestInfo = new RequestChangeSubscription(Links.DeviceInformation.PlayerData.Id, eventData.id,
            RequestChangeSubscription.SubscriptionType.deleteUserFromEvent);
        Links.TcpClient.SendData(requestInfo.Serialize());
        Links.RequestController.RequestFutureEvents();
    }

    public void RequsetAddComment(string comment, int EventID)
    {
        RequestString requestString =
            new RequestString(EventID, comment, RequestString.RequestStringType.AddEventReview);
        Links.TcpClient.SendData(requestString.Serialize());
    }

    public void RequsetComments(int EventID)
    {
        GetByIdRequest request = new GetByIdRequest(EventID, GetByIdRequest.GetById.Reviews);
        Links.TcpClient.SendData(request.Serialize());
    }

    public void RequestQRhash(int eventId)
    {
        //TODO
        GetByIdRequest request = new GetByIdRequest(eventId, GetByIdRequest.GetById.QR);
        Links.TcpClient.SendData(request.Serialize());
    }


    public void RequestVisit(string res)
    {
        RequestString requestString = new RequestString(Links.DeviceInformation.PlayerData.Id, res,
            RequestString.RequestStringType.MakeUserVisited);
        Links.TcpClient.SendData(requestString.Serialize());
        RequestDescriptionChange(Links.DeviceInformation.PlayerData.Description);
    }

    public void RequestFutureEvents()
    {
        //все события
        //
        RequestInfo requestInfo = new RequestInfo(RequestInfo.RequestType.FutureEvents);
        Links.TcpClient.SendData(requestInfo.Serialize());
    }

    public void RequestItemBuy(int itemID)
    {
        RequestChangeSubscription request = new RequestChangeSubscription(Links.DeviceInformation.PlayerData.Id, itemID,
            RequestChangeSubscription.SubscriptionType.buyItem);
        Links.TcpClient.SendData(request.Serialize());
    }

    public void RequestPlayerEvents()
    {
        //Все на что зареган пользователь
        GetByIdRequest request = new GetByIdRequest(Links.DeviceInformation.PlayerData.Id,
            GetByIdRequest.GetById.RegisteredEvents);
        Links.TcpClient.SendData(request.Serialize());
    }

    public void RequestPlayersByName(string phrase)
    {
        RequestPeople tmp = new RequestPeople(phrase);
        Links.TcpClient.SendData(tmp.Serialize());
    }

    public void RegisterEvent(EventData data)
    {
        RequestAddEvent requestAddEvent = new RequestAddEvent(data);
        Links.TcpClient.SendData(requestAddEvent.Serialize());
    }

    public void ConnectionFailed()
    {
        Links.LoginLayoutController.OpenLoginLayoutWithError("connection failed!");
    }
}
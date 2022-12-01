using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Events;

namespace HyperCasualNamespace {

  [RequireComponent(typeof(Button))]
  public class NewRewardAds : MonoBehaviour, IUnityAdsListener {
    [SerializeField] private bool _testMode = true;
    [SerializeField] private Button _adsButton;
    private string _gameId = "4725307"; //ваш game id
    private string _video = "Interstitial_Android";
    private string _rewardedVideo = "Rewarded_Android";
    public UnityEvent ContinueEvent;
    public UnityEvent ReloadEvent;

    void Awake() {
      //ContinueEvent = new UnityEvent();
      ContinueEvent.RemoveAllListeners();
      ReloadEvent.RemoveAllListeners();
    }
    void Start() {     
      Advertisement.AddListener(this);
      Advertisement.Initialize(_gameId, true);
    }

    public bool AdsReadyness() {
      if (Advertisement.IsReady()) {
        return true;
      } else {
        return false;
      }
    }

    public bool ShowRewardedVideo() {
      if (Advertisement.IsReady()) {      
        Advertisement.Show(_rewardedVideo);
        return true;
      } else {
        return false;
      }
    }
    public void ShowAdsVideo() {
      if (Advertisement.IsReady()) {
        Advertisement.Show(_video);
      } else {
        ReloadEvent?.Invoke();
      }
    }

    public void OnUnityAdsReady(string placementId) {
      if (placementId == _rewardedVideo) {
        //_adsButton.interactable = true; //действия, если реклама доступна
      }
    }

    public void OnUnityAdsDidError(string message) {
      //ошибка рекламы
    }

    public void OnUnityAdsDidStart(string placementId) {
      // только запустили рекламу
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) //обработка рекламы (тут определеяем вознаграждение)
    {
      if (showResult == ShowResult.Finished) {
        //ContinueEvent?.Invoke();
        if (placementId == "Rewarded_Android") {
          ContinueEvent?.Invoke();                   
        }
        //GameLogic.S.IncrementPoint2AfterAds();
        //действия, если пользователь посмотрел рекламу до конца
      } else if (showResult == ShowResult.Skipped) {
        //действия, если пользователь пропустил рекламу
      } else if (showResult == ShowResult.Failed) {
        //действия при ошибке
      }

      if (placementId == "Interstitial_Android") {
        ReloadEvent?.Invoke();
      }
    }
  }
}


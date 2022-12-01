using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class RewardedAds : MonoBehaviour, IUnityAdsListener {
  [SerializeField] private bool _testMode = true;
  [SerializeField] private Button _adsButton;

  private string _gameId = "4725307"; //��� game id

  private string _rewardedVideo = "Rewarded_Android";
  
  //
  public UnityEvent ContinueEvent;
  //

  void Awake() {
    ContinueEvent = new UnityEvent();
  }
  void Start() {
    _adsButton = GetComponent<Button>();
    _adsButton.interactable = Advertisement.IsReady(_rewardedVideo);

    if (_adsButton)
      _adsButton.onClick.AddListener(ShowRewardedVideo);

    Advertisement.AddListener(this);
    Advertisement.Initialize(_gameId, true);



  }

  public void ShowRewardedVideo() {
    Advertisement.Show(_rewardedVideo);
  }

  public void OnUnityAdsReady(string placementId) {
    if (placementId == _rewardedVideo) {
      _adsButton.interactable = true; //��������, ���� ������� ��������
    }
  }

  public void OnUnityAdsDidError(string message) {
    //������ �������
  }

  public void OnUnityAdsDidStart(string placementId) {
    // ������ ��������� �������
  }

  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) //��������� ������� (��� ����������� ��������������)
  {
    if (showResult == ShowResult.Finished) {
      if (placementId == "Rewarded_Android") {
        //ContinueEvent?.Invoke();
        GameLinker.SceneGameLinker.ToContinue();
        //Disable button
        Debug.Log("RevardAdd");
        //_adsButton.interactable = false;
        //Start respaw Event

      }
      //GameLogic.S.IncrementPoint2AfterAds();
      //��������, ���� ������������ ��������� ������� �� �����
    } else if (showResult == ShowResult.Skipped) {
      //��������, ���� ������������ ��������� �������
    } else if (showResult == ShowResult.Failed) {
      //�������� ��� ������
    }
  }
}

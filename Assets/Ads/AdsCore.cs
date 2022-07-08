using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsCore : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool _testMode = true;
    private string _gameId = "4725307"; //ваш game id

    private string _video = "Interstitial_Android";
    private string _rewardedVideo = "Rewarded_Android";
    private string _banner = "Banner_Android";

    void Start() // инициализаци€ сервисов
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, _testMode);

        #region Banner

        StartCoroutine(ShowBannerWhenInitialized());
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        #endregion
    }

    public static void ShowAdsVideo(string placementId) //инициализаци€ рекламы по типу
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementId);
        }
        else
        {
            Debug.Log("Advertisement not ready!");
        }
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(_banner);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == _rewardedVideo)
        {
            //действи€, если реклама доступна
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        //ошибка рекламы
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // только запустили рекламу
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) //обработка рекламы (тут определе€ем вознаграждение)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("+1");
            //действи€, если пользователь посмотрел рекламу до конца
            GameLinker.SceneGameLinker.Respawn();
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("„јѕќЋ№");
            //действи€, если пользователь пропустил рекламу + „јѕќЋ№
            GameLinker.SceneGameLinker.Respawn();
        }
        else if (showResult == ShowResult.Failed)
        {
            //действи€ при ошибке
        } else {
          Debug.Log("whatHappend? " + showResult);
        }
    }
}


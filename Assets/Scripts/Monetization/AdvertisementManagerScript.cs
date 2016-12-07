using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdvertisementManagerScript : MonoBehaviour
{
#if UNITY_ANDROID
    public void PlayUnskippedAdvertisement()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }
    public void PlaySckippedAdvertisements()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }
    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed: break;
            case ShowResult.Skipped:
                if (PlayerStats.Current.Equals(PlayerStats.Empty))
                {
                    PlayerStats.Current.Money += 20;
                }
                break;
            case ShowResult.Finished:
                if (!PlayerStats.Current.Equals(PlayerStats.Empty))
                {
                    PlayerStats.Current.Money += 50;
                }
                break;
        }
    }
#endif
}

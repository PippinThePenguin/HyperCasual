using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace HyperCasualNamespace {
  public class ShopPurchase : MonoBehaviour {

    private string _noAdsName = "com.PigandPog.HyperCasual.AdRemoval";
    

    public void OnPurchase(Product product) {
      if (product.definition.id == _noAdsName) {
        Debug.Log("we got it! Money!");
        //other things
        ProfileScriptable.CurrentProfile.IsBought = true;
        FindObjectOfType<ReviveButtonSettings>().CheckPicture();
        this.gameObject.SetActive(false);
      }
    }

    public void OnFailedPurchase(Product product, PurchaseFailureReason reason) {
      Debug.Log(product + "failed because of" + reason);
    }
  }

}

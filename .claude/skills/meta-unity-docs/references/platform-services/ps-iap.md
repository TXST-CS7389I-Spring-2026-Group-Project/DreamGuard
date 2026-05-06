# Ps Iap

**Documentation Index:** Learn about ps iap in this documentation.

---

---
title: "Add-ons Integration"
description: "Integrate in-app purchases into your app to offer purchasable items, subscriptions, and downloadable content."
last_updated: "2026-03-02"
---

This topic discusses how to integrate add-ons into your app.

* For an overview of add-ons, see [Add-ons (DLC and IAP)](/resources/add-ons/).
* For guidance on setting up add-ons, see [Setting Up Add-ons](/resources/add-ons-setup).
* For best practices and other considerations, see [Working with Add-ons](/resources/working-with-add-ons).
* For details on server-to-server APIs, see [IAP Server to Server APIs](/documentation/unity/ps-iap-s2s/).
* For guidance on testing, see [Testing Add-ons](/documentation/unity/ps-iap-test/).
* For IAP consumable item behavior, see [Consuming Purchased IAP Items](/resources/working-with-add-ons#consuming-purchased-iap-items).

## Requirements

You must complete a few requirements first in order to integrate add-ons with your application. The following sections outline how to satisfy the prerequisites.

<oc-devui-note type="note" heading="Note">You must have at least one version of your APK uploaded to the Developer Dashboard before the Monetization APIs will function. Setting up the app alone is not sufficient — a valid APK package must be uploaded so the system can verify your app's identity and correctly associate it with the Monetization APIs.</oc-devui-note>

### Setting up items for purchase

Before you begin integrating add-ons in your app, you need to define the items that you will be offering for purchase. For guidance on setting up add-ons, see [Setting Up Add-ons](/resources/add-ons-setup).

### Engine support

Unity offers a packaging feature called AssetBundles that is compatible with the generic assets in the Meta Horizon platform.
For more information, see:

  * Unity: [AssetBundles](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)

### Indicate that the app needs an Internet connection

For add-ons associated with downloadable content, mark your app as requiring an Internet connection.

1. From the [developer dashboard](/manage), access the app specifications page by following: **Distribution** > **App Submissions** > `{Your app}` > **App Metadata** > **Specs**.
2. From the **Internet Connection** dropdown menu, select **Internet connection required for downloadable contents**.

### Enable IAP Services

1. From the [developer dashboard](/manage), access the app services page by following: **Development** > **All Platform Services** > **Add-ons**. Click **Add Service**
2. From the [developer dashboard](/manage), access the data use checkup page by following: **Requirements** > **Data Use Checkup**. Click **Add** for **User ID** and **In-app purchases and/or downloadable content**

## Integrating with add-ons {#integrate}

Once you've defined the items that you would like to offer as purchases, you can start building them as purchasable items into your app.

The following SDK methods can be called from your client app. Detail about each function can be found in the Platform SDK [Reference Content](/documentation/unity/ps-reference/).

<oc-docs-device devices="quest,go" markdown="block">

## Retrieve a list of available items and prices by SKU

To retrieve a list of add-on items that are available to the user to purchase by SKU, use the following method. The SKUs must have a description to be retrieved by this method.
<oc-docs-device devices="quest,go" markdown="block">

This method also returns any virtual SKUs associated with subscription periods.
</oc-docs-device>

 `Platform.IAP.GetProductsBySKU()` 

If your app displays a price for any add-on, you should use the localized price returned from this endpoint. Do not hard-code price amounts inside the app.

## SKUs for subscriptions

If a subscription tier only has a single subscription period, you can reference that single subscription period as an add-on using the SKU of its tier.

However, to differentiate between multiple subscription periods of the same SKU, we create a virtual SKU for each period by appending the subscription period (**WEEKLY**, **BIWEEKLY**, **MONTHLY**, **QUARTERLY**, **SEMIANNUAL**, **ANNUAL**) to the SKU in this format:

```
<SKU>:SUBSCRIPTION__<PERIOD>
```

For example, consider a subscription tier with the SKU `MyApp-Subscription` that has both monthly and annual subscription periods. You would reference the subscription add-on items by virtual SKU as follows:

* `MyApp-Subscription:SUBSCRIPTION__MONTHLY`
* `MyApp-Subscription:SUBSCRIPTION__ANNUAL`

The **GetProductsBySKU** method also returns virtual SKUs, so to prevent errors, we recommend you call that method to obtain the full list of available SKUs instead of hardcoding virtual SKU strings into your product.
</oc-docs-device>

For more information, see [Subscriptions](/resources/subscriptions/).

## Launch the checkout flow for a SKU

To launch the checkout flow for a user to complete the purchase of a specified SKU, use the following method.

 `Platform.IAP.LaunchCheckoutFlow()` 

## Retrieve all of the user's purchased items {#purchased}

To retrieve a list of IAP purchases that the user has made, use the following method. The returned list includes all durable type purchases and any consumable type purchases that have not been consumed.

 `Platform.IAP.GetViewerPurchases()` 

## Retrieve a cached list of durable items a user has purchased {#durable}

To retrieve a list of durable add-on items that the user has purchased, use the following method. The returned list contains non-consumable purchases and is populated from the device cache. You should always use [`GetViewerPurchases`](#purchased) first and then this method if the other call fails.

 `Platform.IAP.GetViewerPurchasesDurableCache()` 

## Consume a purchased item

To consume a purchased item on behalf of a user, which then marks the item as used, in-app, use the following method.

 `Platform.IAP.ConsumePurchase()` 

## Example implementation

The following Unity example demonstrates the end-to-end flow of:
1. Retrieving information for an IAP item
2. Displaying that information to the user
3. Consuming any outstanding purchases
4. Initiating the checkout flow when a user indicates that they would like to make a purchase

This example is from the [Ultimate Glove Ball sample app](https://github.com/oculus-samples/Unity-UltimateGloveBall) GitHub repo.
It has been edited from its original form for clarity and readability.

See [Unity code samples](/code-samples/unity/) for more information about the code samples and sample apps that are available.

```csharp

using System;
using System.Collections.Generic;
using Meta.XR.Samples;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

namespace UltimateGloveBall.App
{
    /// <summary>
    /// Manages in-app purchases. It's a wrapper on the Oculus.Platform.IAP functionalities.
    /// This makes it easy to fetch all products and purchases as well as make a purchase.
    /// Referenced from: https://developers.meta.com/horizon/documentation/unity/ps-iap/
    /// </summary>
    [MetaCodeSample("UltimateGloveBall")]
    public class IAPManager
    {
        #region Singleton
        private static IAPManager s_instance;

        public static IAPManager Instance
        {
            get
            {
                s_instance ??= new IAPManager();
                return s_instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DestroyInstance()
        {
            s_instance = null;
        }
        #endregion // Singleton

        /// <summary>
        /// The data we get from the error message json string on purchase
        /// </summary>
        private class PurchaseErrorMessage
        {
            // Since we convert from json the naming must stay the same as the json format, which is all lowercase
            public string category;
            public int code;
            public string message;
        }

        private Dictionary<string, Product> m_products = new();
        private Dictionary<string, Purchase> m_purchases = new();

        private Dictionary<string, List<string>> m_productsByCategory = new();
        private List<string> m_availableSkus = new();
        public IList<string> AvailableSkus => m_availableSkus;

        /// <summary>
        /// Asynchronously fetch all products based on the SKU
        /// </summary>
        public void FetchProducts(string[] skus, string category = null)
        {
            _ = IAP.GetProductsBySKU(skus)?.OnComplete(message =>
            {
                GetProductsBySKUCallback(message, category);
            });
        }

        /// <summary>
        /// Asynchronously fetch all purchases that were made by the user
        /// </summary>
        public void FetchPurchases()
        {
            _ = IAP.GetViewerPurchases()?.OnComplete(GetViewerPurchasesCallback);
        }

        public List<string> GetProductSkusForCategory(string category)
        {
            return m_productsByCategory.TryGetValue(category, out var categorySkus) ? categorySkus : null;
        }

        /// <summary>
        /// Returns a product by SKU, otherwise returns null
        /// </summary>
        public Product GetProduct(string sku)
        {
            if (m_products.TryGetValue(sku, out var product))
            {
                return product;
            }

            Debug.LogError($"[IAPManager] Product {sku} doesn't exist!");
            return null;
        }
        /// <summary>
        /// Returns true if the user has purchased the SKU, false if user has not purchased the SKU or if the SKU doesn't exist
        /// </summary>
        public bool IsPurchased(string sku)
        {
            return m_purchases.TryGetValue(sku, out _);
        }

        /// <summary>
        /// Returns a purchase by SKU, otherwise returns null
        /// </summary>
        public Purchase GetPurchase(string sku)
        {
            return m_purchases.TryGetValue(sku, out var purchase) ? purchase : null;
        }

        /// <summary>
        /// Initiating the checkout flow for a SKU
        /// </summary>
        public void Purchase(string sku, Action<string, bool, string> onPurchaseFlowCompleted)
        {
#if UNITY_EDITOR
            m_purchases[sku] = null; // Keep a reference to the purchse, although purchases cannot be made in Editor
            onPurchaseFlowCompleted?.Invoke(sku, true, null);
#else
            IAP.LaunchCheckoutFlow(sku).OnComplete((Message<Purchase> msg) =>
            {
                if (msg.IsError)
                {
                    var errorMsgString = msg.GetError().Message;
                    Debug.LogError($"[IAPManager] Error while purchasing: {errorMsgString}");
                    var errorData = JsonUtility.FromJson<PurchaseErrorMessage>(errorMsgString);
                    onPurchaseFlowCompleted?.Invoke(sku, false, errorData.message);
                    return;
                }

                var p = msg.GetPurchase();
                Debug.Log("[IAPManager] Purchased " + p.Sku);
                m_purchases[sku] = p;
                onPurchaseFlowCompleted?.Invoke(sku, true, null);
            });
#endif
        }

        /// <summary>
        /// Consume a purchased item on behalf of a user
        /// </summary>
        public void ConsumePurchase(string sku, Action<string, bool> onConsumptionCompleted)
        {
#if UNITY_EDITOR
            m_purchases.Remove(sku);
            onConsumptionCompleted?.Invoke(sku, true);
#else
            _ = IAP.ConsumePurchase(sku).OnComplete(msg =>
            {
                if (msg.IsError)
                {
                    Debug.LogError($"[IAPManager] Error while consuming: {msg.GetError().Message}");
                    onConsumptionCompleted?.Invoke(sku, false);
                    return;
                }

                Debug.Log("[IAPManager] Consumed " + sku);
                m_purchases.Remove(sku);
                onConsumptionCompleted?.Invoke(sku, true);
            });
#endif
        }

        private void GetProductsBySKUCallback(Message<ProductList> msg, string category)
        {
            if (msg.IsError)
            {
                Debug.LogError($"[IAPManager] Failed to fetch products, {msg.GetError().Message}");
                return;
            }

            foreach (var p in msg.GetProductList())
            {
                Debug.LogFormat("[IAPManager] Product: sku:{0} name:{1} price:{2}", p.Sku, p.Name, p.FormattedPrice);
                m_products[p.Sku] = p;
                m_availableSkus.Add(p.Sku);
                if (!string.IsNullOrWhiteSpace(category))
                {
                    if (!m_productsByCategory.TryGetValue(category, out var categorySkus))
                    {
                        categorySkus = new List<string>();
                        m_productsByCategory[category] = categorySkus;
                    }

                    categorySkus.Add(p.Sku);
                }
            }
        }

        private void GetViewerPurchasesCallback(Message<PurchaseList> msg)
        {
            if (msg.IsError)
            {
                Debug.LogError($"[IAPManager] Failed to fetch purchased products, {msg.GetError().Message}");
                return;
            }

            foreach (var p in msg.GetPurchaseList())
            {
                Debug.Log($"[IAPManager] Purchased: sku:{p.Sku} granttime:{p.GrantTime} id:{p.ID}");
                m_purchases[p.Sku] = p;
            }
        }
    }
}
```

## Integrating with downloadable content

The following methods allow you to integrate downloadable content (DLC) with your app. Use these methods to retrieve, download, and manage asset files associated with your app.

## Get a list of assets associated with an app
At app startup, this method gets a list of all the assets associated with the app.

 `Platform.AssetFile.GetList` 

This method returns a list of available asset details. Each item in the array has the following properties:

- `assetFileName` - the file name,
- `assetFileID` - Asset identifier
- `IapStatus`, which is one of the following values: `free`, `entitled`, or `not-entitled`
-  `downloadStatus`, which is one of the following values:
     - `installed` meaning that user has installed the file
     - `available` meaning that user can download the file
     - `in-progress` meaning the file is being downloaded or is installing for that user.

## Initiate downloads
If there is a file that is available to download, meaning its status is free or entitled, and `download_status` = `available`, you can initiate the download by calling:

 * `Platform.AssetFile.DownloadById` 

To make this call, pass the ID of the item as returned by the initial `GetList` call.

When you make this call, you will receive an immediate `DownloadResult` response with the path to the asset as a confirmation that the request was successful. You should also listen for `DownloadUpdate` notifications which return info about transferred bytes, and a complete flag that notifies you when the download is complete.
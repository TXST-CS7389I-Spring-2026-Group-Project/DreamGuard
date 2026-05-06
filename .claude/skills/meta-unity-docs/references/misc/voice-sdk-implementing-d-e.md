# Voice Sdk Implementing D E

**Documentation Index:** Learn about voice sdk implementing d e in this documentation.

---

---
title: "Implementing Dynamic Entities"
description: "Define keyword sets and synonyms in scriptable objects to improve voice recognition accuracy for specific entities."
---

You can create static sets of dynamic entity definitions with a `DynamicEntities` scriptable object. This object allows you to define all of the keywords and synonyms for each entity you want to cover. A single `DynamicEntities` scriptable object can contain multiple entities, their keywords, and their synonyms.

## To create a `DynamicEntities` scriptable object
1. In the Unity editor, on the **Assets** menu, go to **Create** > **Voice SDK** > **Dynamic Entities**.
2. In the **Assets** folder, create a subfolder called `_Projects/Data/Voice` to store the dynamic entities file. Alternatively, navigate to the location where you store files of this type. Save the dynamic entities file.
3. In the **Inspector** window, select **+** just beneath the entity list and then record your first entity. All information in the first expanding section will refer to this entity.
4. To enter a keyword for this entity, select **+** just below the keyword list, and then enter your keyword.
5. To enter synonyms for the keyword, enter the number of synonyms, and then enter the individual synonyms for each element.
6. Repeat steps 3 and 4 for as many keywords desired for the entity.
7. Repeat steps 2 through 5 for as many entities as desired.
    For example, the dynamic entity data for a chess board might look like this:
    {:width="466px"}

## To add the `DynamicEntities` scriptable object to your scene
1. If your scene does not already have an **App Voice Experience** GameObject associated with it, add one.
    Otherwise:
    1. Select your scene in the **Hierarchy** window.
    2. On the **Assets** menu, go to **Create** > **Voice SDK** > **Add App Voice Experience to Scene**.
2. Select the **App Voice Experience** GameObject in your scene.
3. In the **Inspector** window, choose **Add Component**.
4. In the **Add Component** search window, enter `dynamic`, and then select **Dynamic Entity Data Provider** from the list.
5. If necessary, expand **Dynamic Entity Data Provider (Script)** in the **Inspector** window.
6. Expand **Entities Definition**, and then select **+** just below the entities list.
7. Select the target icon in the **Element 0** box, and then double-click the dynamic entities file that you created earlier:
    {:width="430px"}

## Dynamic Entity Provider
A dynamic entity provider works much the same way the `DynamicEntityDataProvider` works and you can add it to the App Voice Experience in much the same way. However, instead of defining your entities within a scriptable object as with the `DynamicEntityDataProvider`, with the `DynamicEntityProvider`, you define your entities directly in the provider.
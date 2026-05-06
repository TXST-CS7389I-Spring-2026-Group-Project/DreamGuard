# Move Eye Tracking

**Documentation Index:** Learn about move eye tracking in this documentation.

---

---
title: "Eye Tracking for Movement SDK for Unity"
description: "Integrate eye gaze tracking into your Unity project using the OVREyeGaze component on Meta Quest Pro."
last_updated: "2025-05-31"
---

{::comment}**DEVICES**{:/comment}

{::comment}**MOVEMENT SDK**{:/comment}

{::comment}**BODY TRACKING**{:/comment}

{::comment}**CHARACTER**{:/comment}

{::comment}**EYE TRACKING**{:/comment}

{::comment}**FACE TRACKING**{:/comment}

{::comment}**DEVICES**{:/comment}

{::comment}**MOVEMENT SDK**{:/comment}

{::comment}**BODY TRACKING**{:/comment}

{::comment}**CHARACTER**{:/comment}

{::comment}**EYE TRACKING**{:/comment}

{::comment}**FACE TRACKING**{:/comment}

[dup]:                      /policy/data-use/ "Developer Data Use Policy"
[dup-prohibited]:           /policy/data-use/#section-4-prohibited-uses-of-user-data "Developer Data Use Policy: Prohibited Practices"

[oc-sdk-license-agreement]: https://developers.meta.com/horizon/licenses/pc-3.3/ "Oculus Software Development Kit License Agreement"

{::comment}**TOPICS**{:/comment}

[move]:                     /documentation/unity/move-overview/ "Movement"
[move-openxr]:              /documentation/native/android/move-overview/ "Movement OpenXR Extensions"

[api]:                      /documentation/unity/move-ref-api/ "API Reference"
[api-openxr]:               /documentation/native/android/move-ref-api/ "API Reference"

[blendshapes]:              /documentation/unity/move-ref-blendshapes/ "Blendshape Visual Reference"
[blendshapes-openxr]:       /documentation/native/android/move-ref-blendshapes/ "Blendshape Visual Reference"

[body-joints]:              /documentation/unity/move-ref-body-joints/ "Body Joint Visual Reference"
[body-joints-openxr]:       /documentation/native/android/move-ref-body-joints/ "Body Joint Visual Reference"

[bt]:                       /documentation/unity/move-body-tracking/ "Body Tracking"
[bt-openxr]:                /documentation/native/android/move-body-tracking/ "Movement Body Tracking OpenXR Extensions"

[et]:                       /documentation/unity/move-eye-tracking/ "Eye Tracking"
[et-openxr]:                /documentation/native/android/move-eye-tracking/ "Movement Eye Tracking OpenXR Extensions"

[ft]:                       /documentation/unity/move-face-tracking/ "Natural Facial Expressions"
[ft-openxr]:                /documentation/native/android/move-face-tracking/ "Movement Face Tracking OpenXR Extensions"

[policies]:                 /documentation/unity/move-policy-data-disclaimer/" "Policies and Disclaimers"
[policies-openxr]:          /documentation/native/android/move-policy-data-disclaimer/" "Policies and Disclaimers"

[retargeting-bones]:        /documentation/unity/move-retargeting-bones/ "Retargeting Bones"
[retargeting-bones-openxr]: /documentation/native/android/move-retargeting-bones/ "Retargeting Bones"

{::comment}**UNITY SCRIPTS AND COMPONENTS**{:/comment}

[OVRBody]:                  /documentation/unity/move-ref-scripts/#ovrbody "The OVRBody component."
[OVRBody.cs]:               https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_body/ "OVRBody API Reference"

[OVRCustomSkeleton]:        /documentation/unity/move-ref-scripts/#ovrcustomskeleton "The OVRCustomSkeleton component."
[OVRCustomSkeleton.cs]:     https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_custom_skeleton/ "OVRCustomSkeleton API Reference"

[OVREyeGaze]:               /documentation/unity/move-ref-scripts/#ovreyegaze "The OVREyeGaze component."
[OVREyeGaze.cs]:            https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_eye_gaze/ "OVREyeGaze API Reference"

[OVRFaceExpressions]:       /documentation/unity/move-ref-scripts/#ovrfaceexpressions "The OVRFaceExpressions component."
[OVRFaceExpressions.cs]:    https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_face_expressions/ "OVRFaceExpressions API Reference"

[OVRSkeleton]:              /documentation/unity/move-ref-scripts/#ovrskeleton "The OVRSkeleton component."
[OVRSkeleton.cs]:           https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_skeleton/ "OVRSkeleton API Reference"

[Auto-Map-Bones]:               /images/movement-ovr-custom-skeleton-automap-button.png "Auto-Mapping Bones in a Custom Skeleton"

[Common-Custom-Skeleton-Setup]: /images/movement-ovr-common-setup-custom-skeleton-component.png "Common Setup For A Custom Skeleton"

[Movement-Enable-Tracking]:     /images/movement-enable-tracking.png "Settings for Enabling Tracking Features"

[Movement-OVR-Eye-Gaze]:        /images/movement-ovr-eye-gaze-component.png "OVR Eye Gaze Component"

[Movement-SDK-Splash]:          /images/movement-splash.jpg "Movement SDK"
{: height="718px" width="1000px" }

[Skeleton-Bones]:               /images/movement-skeleton-bones.png "Skeleton Bones"
[Movement-Samples-Fitness]:          /images/move-sample-fitness.png "Move Samples Fitness"
[Movement-Samples-Locomotion]:          /images/move-sample-locomotion.png "Move Samples Locomotion"
[Movement-Samples-ISDK-Integration]:          /images/move-sample-isdk-integration.png "Move Samples ISDK Integration"

[XR_Face_Expression_Neutral]:                /images/XR_Face_Expression_Neutral.png "The Neutral OpenXR Face Expression Blendshape"
[XR_Face_Expression_Brow_Lowerer_L]:         /images/XR_Face_Expression_Brow_Lowerer_L.png "The XR_Face_Expression_Brow_Lowerer_L Blendshape"
[XR_Face_Expression_Brow_Lowerer_R]:         /images/XR_Face_Expression_Brow_Lowerer_R.png "The XR_Face_Expression_Brow_Lowerer_R Blendshape"
[XR_Face_Expression_Cheek_Puff_L]:           /images/XR_Face_Expression_Cheek_Puff_L.png "The XR_Face_Expression_Cheek_Puff_L Blendshape"
[XR_Face_Expression_Cheek_Puff_R]:           /images/XR_Face_Expression_Cheek_Puff_R.png "The XR_Face_Expression_Cheek_Puff_R Blendshape"
[XR_Face_Expression_Cheek_Raiser_L]:         /images/XR_Face_Expression_Cheek_Raiser_L.png "The XR_Face_Expression_Cheek_Raiser_L Blendshape"
[XR_Face_Expression_Cheek_Raiser_R]:         /images/XR_Face_Expression_Cheek_Raiser_R.png "The XR_Face_Expression_Cheek_Raiser_R Blendshape"
[XR_Face_Expression_Cheek_Suck_L]:           /images/XR_Face_Expression_Cheek_Suck_L.png "The XR_Face_Expression_Cheek_Suck_L Blendshape"
[XR_Face_Expression_Cheek_Suck_R]:           /images/XR_Face_Expression_Cheek_Suck_R.png "The XR_Face_Expression_Cheek_Suck_R Blendshape"
[XR_Face_Expression_Chin_Raiser_B]:          /images/XR_Face_Expression_Chin_Raiser_B.png "The XR_Face_Expression_Chin_Raiser_B Blendshape"
[XR_Face_Expression_Chin_Raiser_T]:          /images/XR_Face_Expression_Chin_Raiser_T.png "The XR_Face_Expression_Chin_Raiser_T Blendshape"
[XR_Face_Expression_Dimpler_L]:              /images/XR_Face_Expression_Dimpler_L.png "The XR_Face_Expression_Dimpler_L Blendshape"
[XR_Face_Expression_Dimpler_R]:              /images/XR_Face_Expression_Dimpler_R.png "The XR_Face_Expression_Dimpler_R Blendshape"
[XR_Face_Expression_Eyes_Closed_L]:          /images/XR_Face_Expression_Eyes_Closed_L.png "The XR_Face_Expression_Eyes_Closed_L Blendshape"
[XR_Face_Expression_Eyes_Closed_R]:          /images/XR_Face_Expression_Eyes_Closed_R.png "The XR_Face_Expression_Eyes_Closed_R Blendshape"
[XR_Face_Expression_Eyes_Look_Down_L]:       /images/XR_Face_Expression_Eyes_Look_Down_L.png "The XR_Face_Expression_Eyes_Look_Down_L Blendshape"
[XR_Face_Expression_Eyes_Look_Down_R]:       /images/XR_Face_Expression_Eyes_Look_Down_R.png "The XR_Face_Expression_Eyes_Look_Down_R Blendshape"
[XR_Face_Expression_Eyes_Look_Left_L]:       /images/XR_Face_Expression_Eyes_Look_Left_L.png "The XR_Face_Expression_Eyes_Look_Left_L Blendshape"
[XR_Face_Expression_Eyes_Look_Left_R]:       /images/XR_Face_Expression_Eyes_Look_Left_R.png "The XR_Face_Expression_Eyes_Look_Left_R Blendshape"
[XR_Face_Expression_Eyes_Look_Right_L]:      /images/XR_Face_Expression_Eyes_Look_Right_L.png "The XR_Face_Expression_Eyes_Look_Right_L Blendshape"
[XR_Face_Expression_Eyes_Look_Right_R]:      /images/XR_Face_Expression_Eyes_Look_Right_R.png "The XR_Face_Expression_Eyes_Look_Right_R Blendshape"
[XR_Face_Expression_Eyes_Look_Up_L]:         /images/XR_Face_Expression_Eyes_Look_Up_L.png "The XR_Face_Expression_Eyes_Look_Up_L Blendshape"
[XR_Face_Expression_Eyes_Look_Up_R]:         /images/XR_Face_Expression_Eyes_Look_Up_R.png "The XR_Face_Expression_Eyes_Look_Up_R Blendshape"
[XR_Face_Expression_Inner_Brow_Raiser_L]:    /images/XR_Face_Expression_Inner_Brow_Raiser_L.png "The XR_Face_Expression_Inner_Brow_Raiser_L Blendshape"
[XR_Face_Expression_Inner_Brow_Raiser_R]:    /images/XR_Face_Expression_Inner_Brow_Raiser_R.png "The XR_Face_Expression_Inner_Brow_Raiser_R Blendshape"
[XR_Face_Expression_Jaw_Drop]:               /images/XR_Face_Expression_Jaw_Drop.png "The XR_Face_Expression_Jaw_Drop Blendshape"
[XR_Face_Expression_Jaw_Sideways_Left]:      /images/XR_Face_Expression_Jaw_Sideways_Left.png "The XR_Face_Expression_Jaw_Sideways_Left Blendshape"
[XR_Face_Expression_Jaw_Sideways_Right]:     /images/XR_Face_Expression_Jaw_Sideways_Right.png "The XR_Face_Expression_Jaw_Sideways_Right Blendshape"
[XR_Face_Expression_Jaw_Thrust]:             /images/XR_Face_Expression_Jaw_Thrust.png "The XR_Face_Expression_Jaw_Thrust Blendshape"
[XR_Face_Expression_Lid_Tightener_L]:        /images/XR_Face_Expression_Lid_Tightener_L.png "The XR_Face_Expression_Lid_Tightener_L Blendshape"
[XR_Face_Expression_Lid_Tightener_R]:        /images/XR_Face_Expression_Lid_Tightener_R.png "The XR_Face_Expression_Lid_Tightener_R Blendshape"
[XR_Face_Expression_Lip_Corner_Depressor_L]: /images/XR_Face_Expression_Lip_Corner_Depressor_L.png "The XR_Face_Expression_Lip_Corner_Depressor_L Blendshape"
[XR_Face_Expression_Lip_Corner_Depressor_R]: /images/XR_Face_Expression_Lip_Corner_Depressor_R.png "The XR_Face_Expression_Lip_Corner_Depressor_R Blendshape"
[XR_Face_Expression_Lip_Corner_Puller_L]:    /images/XR_Face_Expression_Lip_Corner_Puller_L.png "The XR_Face_Expression_Lip_Corner_Puller_L Blendshape"
[XR_Face_Expression_Lip_Corner_Puller_R]:    /images/XR_Face_Expression_Lip_Corner_Puller_R.png "The XR_Face_Expression_Lip_Corner_Puller_R Blendshape"
[XR_Face_Expression_Lip_Funneler_LB]:        /images/XR_Face_Expression_Lip_Funneler_LB.png "The XR_Face_Expression_Lip_Funneler_LB Blendshape"
[XR_Face_Expression_Lip_Funneler_LT]:        /images/XR_Face_Expression_Lip_Funneler_LT.png "The XR_Face_Expression_Lip_Funneler_LT Blendshape"
[XR_Face_Expression_Lip_Funneler_RB]:        /images/XR_Face_Expression_Lip_Funneler_RB.png "The XR_Face_Expression_Lip_Funneler_RB Blendshape"
[XR_Face_Expression_Lip_Funneler_RT]:        /images/XR_Face_Expression_Lip_Funneler_RT.png "The XR_Face_Expression_Lip_Funneler_RT Blendshape"
[XR_Face_Expression_Lip_Pressor_L]:          /images/XR_Face_Expression_Lip_Pressor_L.png "The XR_Face_Expression_Lip_Pressor_L Blendshape"
[XR_Face_Expression_Lip_Pressor_R]:          /images/XR_Face_Expression_Lip_Pressor_R.png "The XR_Face_Expression_Lip_Pressor_R Blendshape"
[XR_Face_Expression_Lip_Pucker_L]:           /images/XR_Face_Expression_Lip_Pucker_L.png "The XR_Face_Expression_Lip_Pucker_L Blendshape"
[XR_Face_Expression_Lip_Pucker_R]:           /images/XR_Face_Expression_Lip_Pucker_R.png "The XR_Face_Expression_Lip_Pucker_R Blendshape"
[XR_Face_Expression_Lip_Stretcher_L]:        /images/XR_Face_Expression_Lip_Stretcher_L.png "The XR_Face_Expression_Lip_Stretcher_L Blendshape"
[XR_Face_Expression_Lip_Stretcher_R]:        /images/XR_Face_Expression_Lip_Stretcher_R.png "The XR_Face_Expression_Lip_Stretcher_R Blendshape"
[XR_Face_Expression_Lip_Suck_LB]:            /images/XR_Face_Expression_Lip_Suck_LB.png "The XR_Face_Expression_Lip_Suck_LB Blendshape"
[XR_Face_Expression_Lip_Suck_LT]:            /images/XR_Face_Expression_Lip_Suck_LT.png "The XR_Face_Expression_Lip_Suck_LT Blendshape"
[XR_Face_Expression_Lip_Suck_RB]:            /images/XR_Face_Expression_Lip_Suck_RB.png "The XR_Face_Expression_Lip_Suck_RB Blendshape"
[XR_Face_Expression_Lip_Suck_RT]:            /images/XR_Face_Expression_Lip_Suck_RT.png "The XR_Face_Expression_Lip_Suck_RT Blendshape"
[XR_Face_Expression_Lip_Tightener_L]:        /images/XR_Face_Expression_Lip_Tightener_L.png "The XR_Face_Expression_Lip_Tightener_L Blendshape"
[XR_Face_Expression_Lip_Tightener_R]:        /images/XR_Face_Expression_Lip_Tightener_R.png "The XR_Face_Expression_Lip_Tightener_R Blendshape"
[XR_Face_Expression_Lips_Toward]:            /images/XR_Face_Expression_Lips_Toward.png "The XR_Face_Expression_Lips_Toward Blendshape"
[XR_Face_Expression_Lower_Lip_Depressor_L]:  /images/XR_Face_Expression_Lower_Lip_Depressor_L.png "The XR_Face_Expression_Lower_Lip_Depressor_L Blendshape"
[XR_Face_Expression_Lower_Lip_Depressor_R]:  /images/XR_Face_Expression_Lower_Lip_Depressor_R.png "The XR_Face_Expression_Lower_Lip_Depressor_R Blendshape"
[XR_Face_Expression_Mouth_Left]:             /images/XR_Face_Expression_Mouth_Left.png "The XR_Face_Expression_Mouth_Left Blendshape"
[XR_Face_Expression_Mouth_Right]:            /images/XR_Face_Expression_Mouth_Right.png "The XR_Face_Expression_Mouth_Right Blendshape"
[XR_Face_Expression_Nose_Wrinkler_L]:        /images/XR_Face_Expression_Nose_Wrinkler_L.png "The XR_Face_Expression_Nose_Wrinkler_L Blendshape"
[XR_Face_Expression_Nose_Wrinkler_R]:        /images/XR_Face_Expression_Nose_Wrinkler_R.png "The XR_Face_Expression_Nose_Wrinkler_R Blendshape"
[XR_Face_Expression_Outer_Brow_Raiser_L]:    /images/XR_Face_Expression_Outer_Brow_Raiser_L.png "The XR_Face_Expression_Outer_Brow_Raiser_L Blendshape"
[XR_Face_Expression_Outer_Brow_Raiser_R]:    /images/XR_Face_Expression_Outer_Brow_Raiser_R.png "The XR_Face_Expression_Outer_Brow_Raiser_R Blendshape"
[XR_Face_Expression_Upper_Lid_Raiser_L]:     /images/XR_Face_Expression_Upper_Lid_Raiser_L.png "The XR_Face_Expression_Upper_Lid_Raiser_L Blendshape"
[XR_Face_Expression_Upper_Lid_Raiser_R]:     /images/XR_Face_Expression_Upper_Lid_Raiser_R.png "The XR_Face_Expression_Upper_Lid_Raiser_R Blendshape"
[XR_Face_Expression_Upper_Lip_Raiser_L]:     /images/XR_Face_Expression_Upper_Lip_Raiser_L.png "The XR_Face_Expression_Upper_Lip_Raiser_L Blendshape"
[XR_Face_Expression_Upper_Lip_Raiser_R]:     /images/XR_Face_Expression_Upper_Lip_Raiser_R.png "The XR_Face_Expression_Upper_Lip_Raiser_R Blendshape"

[XR_Face_Expression_2_Brow_Lowerer_L]:         /images/XR_Face_Expression_2_Brow_Lowerer_L.png "The XR_Face_Expression_Brow_Lowerer_L Blendshape"
[XR_Face_Expression_2_Brow_Lowerer_R]:         /images/XR_Face_Expression_2_Brow_Lowerer_R.png "The XR_Face_Expression_Brow_Lowerer_R Blendshape"
[XR_Face_Expression_2_Cheek_Puff_L]:           /images/XR_Face_Expression_2_Cheek_Puff_L.png "The XR_Face_Expression_Cheek_Puff_L Blendshape"
[XR_Face_Expression_2_Cheek_Puff_R]:           /images/XR_Face_Expression_2_Cheek_Puff_R.png "The XR_Face_Expression_Cheek_Puff_R Blendshape"
[XR_Face_Expression_2_Cheek_Raiser_L]:         /images/XR_Face_Expression_2_Cheek_Raiser_L.png "The XR_Face_Expression_Cheek_Raiser_L Blendshape"
[XR_Face_Expression_2_Cheek_Raiser_R]:         /images/XR_Face_Expression_2_Cheek_Raiser_R.png "The XR_Face_Expression_Cheek_Raiser_R Blendshape"
[XR_Face_Expression_2_Cheek_Suck_L]:           /images/XR_Face_Expression_2_Cheek_Suck_L.png "The XR_Face_Expression_Cheek_Suck_L Blendshape"
[XR_Face_Expression_2_Cheek_Suck_R]:           /images/XR_Face_Expression_2_Cheek_Suck_R.png "The XR_Face_Expression_Cheek_Suck_R Blendshape"
[XR_Face_Expression_2_Chin_Raiser_B]:          /images/XR_Face_Expression_2_Chin_Raiser_B.png "The XR_Face_Expression_Chin_Raiser_B Blendshape"
[XR_Face_Expression_2_Chin_Raiser_T]:          /images/XR_Face_Expression_2_Chin_Raiser_T.png "The XR_Face_Expression_Chin_Raiser_T Blendshape"
[XR_Face_Expression_2_Dimpler_L]:              /images/XR_Face_Expression_2_Dimpler_L.png "The XR_Face_Expression_Dimpler_L Blendshape"
[XR_Face_Expression_2_Dimpler_R]:              /images/XR_Face_Expression_2_Dimpler_R.png "The XR_Face_Expression_Dimpler_R Blendshape"
[XR_Face_Expression_2_Eyes_Closed_L]:          /images/XR_Face_Expression_2_Eyes_Closed_L.png "The XR_Face_Expression_Eyes_Closed_L Blendshape"
[XR_Face_Expression_2_Eyes_Closed_R]:          /images/XR_Face_Expression_2_Eyes_Closed_R.png "The XR_Face_Expression_Eyes_Closed_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Down_L]:       /images/XR_Face_Expression_2_Eyes_Look_Down_L.png "The XR_Face_Expression_Eyes_Look_Down_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Down_R]:       /images/XR_Face_Expression_2_Eyes_Look_Down_R.png "The XR_Face_Expression_Eyes_Look_Down_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Left_L]:       /images/XR_Face_Expression_2_Eyes_Look_Left_L.png "The XR_Face_Expression_Eyes_Look_Left_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Left_R]:       /images/XR_Face_Expression_2_Eyes_Look_Left_R.png "The XR_Face_Expression_Eyes_Look_Left_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Right_L]:      /images/XR_Face_Expression_2_Eyes_Look_Right_L.png "The XR_Face_Expression_Eyes_Look_Right_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Right_R]:      /images/XR_Face_Expression_2_Eyes_Look_Right_R.png "The XR_Face_Expression_Eyes_Look_Right_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Up_L]:         /images/XR_Face_Expression_2_Eyes_Look_Up_L.png "The XR_Face_Expression_Eyes_Look_Up_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Up_R]:         /images/XR_Face_Expression_2_Eyes_Look_Up_R.png "The XR_Face_Expression_Eyes_Look_Up_R Blendshape"
[XR_Face_Expression_2_Inner_Brow_Raiser_L]:    /images/XR_Face_Expression_2_Inner_Brow_Raiser_L.png "The XR_Face_Expression_Inner_Brow_Raiser_L Blendshape"
[XR_Face_Expression_2_Inner_Brow_Raiser_R]:    /images/XR_Face_Expression_2_Inner_Brow_Raiser_R.png "The XR_Face_Expression_Inner_Brow_Raiser_R Blendshape"
[XR_Face_Expression_2_Jaw_Drop]:               /images/XR_Face_Expression_2_Jaw_Drop.png "The XR_Face_Expression_Jaw_Drop Blendshape"
[XR_Face_Expression_2_Jaw_Sideways_Left]:      /images/XR_Face_Expression_2_Jaw_Sideways_Left.png "The XR_Face_Expression_Jaw_Sideways_Left Blendshape"
[XR_Face_Expression_2_Jaw_Sideways_Right]:     /images/XR_Face_Expression_2_Jaw_Sideways_Right.png "The XR_Face_Expression_Jaw_Sideways_Right Blendshape"
[XR_Face_Expression_2_Jaw_Thrust]:             /images/XR_Face_Expression_2_Jaw_Thrust.png "The XR_Face_Expression_Jaw_Thrust Blendshape"
[XR_Face_Expression_2_Lid_Tightener_L]:        /images/XR_Face_Expression_2_Lid_Tightener_L.png "The XR_Face_Expression_Lid_Tightener_L Blendshape"
[XR_Face_Expression_2_Lid_Tightener_R]:        /images/XR_Face_Expression_2_Lid_Tightener_R.png "The XR_Face_Expression_Lid_Tightener_R Blendshape"
[XR_Face_Expression_2_Lip_Corner_Depressor_L]: /images/XR_Face_Expression_2_Lip_Corner_Depressor_L.png "The XR_Face_Expression_Lip_Corner_Depressor_L Blendshape"
[XR_Face_Expression_2_Lip_Corner_Depressor_R]: /images/XR_Face_Expression_2_Lip_Corner_Depressor_R.png "The XR_Face_Expression_Lip_Corner_Depressor_R Blendshape"
[XR_Face_Expression_2_Lip_Corner_Puller_L]:    /images/XR_Face_Expression_2_Lip_Corner_Puller_L.png "The XR_Face_Expression_Lip_Corner_Puller_L Blendshape"
[XR_Face_Expression_2_Lip_Corner_Puller_R]:    /images/XR_Face_Expression_2_Lip_Corner_Puller_R.png "The XR_Face_Expression_Lip_Corner_Puller_R Blendshape"
[XR_Face_Expression_2_Lip_Funneler_LB]:        /images/XR_Face_Expression_2_Lip_Funneler_LB.png "The XR_Face_Expression_Lip_Funneler_LB Blendshape"
[XR_Face_Expression_2_Lip_Funneler_LT]:        /images/XR_Face_Expression_2_Lip_Funneler_LT.png "The XR_Face_Expression_Lip_Funneler_LT Blendshape"
[XR_Face_Expression_2_Lip_Funneler_RB]:        /images/XR_Face_Expression_2_Lip_Funneler_RB.png "The XR_Face_Expression_Lip_Funneler_RB Blendshape"
[XR_Face_Expression_2_Lip_Funneler_RT]:        /images/XR_Face_Expression_2_Lip_Funneler_RT.png "The XR_Face_Expression_Lip_Funneler_RT Blendshape"
[XR_Face_Expression_2_Lip_Pressor_L]:          /images/XR_Face_Expression_2_Lip_Pressor_L.png "The XR_Face_Expression_Lip_Pressor_L Blendshape"
[XR_Face_Expression_2_Lip_Pressor_R]:          /images/XR_Face_Expression_2_Lip_Pressor_R.png "The XR_Face_Expression_Lip_Pressor_R Blendshape"
[XR_Face_Expression_2_Lip_Pucker_L]:           /images/XR_Face_Expression_2_Lip_Pucker_L.png "The XR_Face_Expression_Lip_Pucker_L Blendshape"
[XR_Face_Expression_2_Lip_Pucker_R]:           /images/XR_Face_Expression_2_Lip_Pucker_R.png "The XR_Face_Expression_Lip_Pucker_R Blendshape"
[XR_Face_Expression_2_Lip_Stretcher_L]:        /images/XR_Face_Expression_2_Lip_Stretcher_L.png "The XR_Face_Expression_Lip_Stretcher_L Blendshape"
[XR_Face_Expression_2_Lip_Stretcher_R]:        /images/XR_Face_Expression_2_Lip_Stretcher_R.png "The XR_Face_Expression_Lip_Stretcher_R Blendshape"
[XR_Face_Expression_2_Lip_Suck_LB]:            /images/XR_Face_Expression_2_Lip_Suck_LB.png "The XR_Face_Expression_Lip_Suck_LB Blendshape"
[XR_Face_Expression_2_Lip_Suck_LT]:            /images/XR_Face_Expression_2_Lip_Suck_LT.png "The XR_Face_Expression_Lip_Suck_LT Blendshape"
[XR_Face_Expression_2_Lip_Suck_RB]:            /images/XR_Face_Expression_2_Lip_Suck_RB.png "The XR_Face_Expression_Lip_Suck_RB Blendshape"
[XR_Face_Expression_2_Lip_Suck_RT]:            /images/XR_Face_Expression_2_Lip_Suck_RT.png "The XR_Face_Expression_Lip_Suck_RT Blendshape"
[XR_Face_Expression_2_Lip_Tightener_L]:        /images/XR_Face_Expression_2_Lip_Tightener_L.png "The XR_Face_Expression_Lip_Tightener_L Blendshape"
[XR_Face_Expression_2_Lip_Tightener_R]:        /images/XR_Face_Expression_2_Lip_Tightener_R.png "The XR_Face_Expression_Lip_Tightener_R Blendshape"
[XR_Face_Expression_2_Lips_Toward]:            /images/XR_Face_Expression_2_Lips_Toward.png "The XR_Face_Expression_Lips_Toward Blendshape"
[XR_Face_Expression_2_Lower_Lip_Depressor_L]:  /images/XR_Face_Expression_2_Lower_Lip_Depressor_L.png "The XR_Face_Expression_Lower_Lip_Depressor_L Blendshape"
[XR_Face_Expression_2_Lower_Lip_Depressor_R]:  /images/XR_Face_Expression_2_Lower_Lip_Depressor_R.png "The XR_Face_Expression_Lower_Lip_Depressor_R Blendshape"
[XR_Face_Expression_2_Mouth_Left]:             /images/XR_Face_Expression_2_Mouth_Left.png "The XR_Face_Expression_Mouth_Left Blendshape"
[XR_Face_Expression_2_Mouth_Right]:            /images/XR_Face_Expression_2_Mouth_Right.png "The XR_Face_Expression_Mouth_Right Blendshape"
[XR_Face_Expression_2_Nose_Wrinkler_L]:        /images/XR_Face_Expression_2_Nose_Wrinkler_L.png "The XR_Face_Expression_Nose_Wrinkler_L Blendshape"
[XR_Face_Expression_2_Nose_Wrinkler_R]:        /images/XR_Face_Expression_2_Nose_Wrinkler_R.png "The XR_Face_Expression_Nose_Wrinkler_R Blendshape"
[XR_Face_Expression_2_Outer_Brow_Raiser_L]:    /images/XR_Face_Expression_2_Outer_Brow_Raiser_L.png "The XR_Face_Expression_Outer_Brow_Raiser_L Blendshape"
[XR_Face_Expression_2_Outer_Brow_Raiser_R]:    /images/XR_Face_Expression_2_Outer_Brow_Raiser_R.png "The XR_Face_Expression_Outer_Brow_Raiser_R Blendshape"
[XR_Face_Expression_2_Upper_Lid_Raiser_L]:     /images/XR_Face_Expression_2_Upper_Lid_Raiser_L.png "The XR_Face_Expression_Upper_Lid_Raiser_L Blendshape"
[XR_Face_Expression_2_Upper_Lid_Raiser_R]:     /images/XR_Face_Expression_2_Upper_Lid_Raiser_R.png "The XR_Face_Expression_Upper_Lid_Raiser_R Blendshape"
[XR_Face_Expression_2_Upper_Lip_Raiser_L]:     /images/XR_Face_Expression_2_Upper_Lip_Raiser_L.png "The XR_Face_Expression_Upper_Lip_Raiser_L Blendshape"
[XR_Face_Expression_2_Upper_Lip_Raiser_R]:     /images/XR_Face_Expression_2_Upper_Lip_Raiser_R.png "The XR_Face_Expression_Upper_Lip_Raiser_R Blendshape"
[XR_Face_Expression_2_Tongue_Tip_Interdental]:     /images/XR_Face_Expression_2_Tongue_Tip_Interdental.png "The XR_Face_Expression_Tongue_Tip_Interdental Blendshape"
[XR_Face_Expression_2_Tongue_Tip_Alveolar]:        /images/XR_Face_Expression_2_Tongue_Tip_Alveolar.png "The XR_Face_Expression_Tongue_Tip_Alveolar Blendshape"
[XR_Face_Expression_2_Tongue_Front_Dorsal_Palate]: /images/XR_Face_Expression_2_Tongue_Front_Dorsal_Palate.png "The XR_Face_Expression_Tongue_Front_Dorsal_Palate Blendshape"
[XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate]:   /images/XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate.png "The XR_Face_Expression_Tongue_Mid_Dorsal_Palate Blendshape"
[XR_Face_Expression_2_Tongue_Back_Dorsal_Velar]:   /images/XR_Face_Expression_2_Tongue_Back_Dorsal_Velar.png "The XR_Face_Expression_Tongue_Back_Dorsal_Velar Blendshape"
[XR_Face_Expression_2_Tongue_Out]:                 /images/XR_Face_Expression_2_Tongue_Out.png "The XR_Face_Expression_Tongue_Out Blendshape"
[XR_Face_Expression_2_Tongue_Retreat]:             /images/XR_Face_Expression_2_Tongue_Retreat.png "The XR_Face_Expression_Tongue_Retreat Blendshape"

[XR_META_Face_Tracking_Viseme_AA]:        /images/XR_META_Face_Tracking_Viseme_AA.jpg "The AA Viseme"
[XR_META_Face_Tracking_Viseme_AA_rot]:    /images/XR_META_Face_Tracking_Viseme_AA_rot.jpg "The AA Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_AA_emp]:    /images/XR_META_Face_Tracking_Viseme_AA_emp.jpg "The AA Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_CH]:        /images/XR_META_Face_Tracking_Viseme_CH.jpg "The CH Viseme"
[XR_META_Face_Tracking_Viseme_CH_rot]:    /images/XR_META_Face_Tracking_Viseme_CH_rot.jpg "The CH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_CH_emp]:    /images/XR_META_Face_Tracking_Viseme_CH_emp.jpg "The CH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_DD]:        /images/XR_META_Face_Tracking_Viseme_DD.jpg "The DD Viseme"
[XR_META_Face_Tracking_Viseme_DD_rot]:    /images/XR_META_Face_Tracking_Viseme_DD_rot.jpg "The DD Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_DD_emp]:    /images/XR_META_Face_Tracking_Viseme_DD_emp.jpg "The DD Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_E]:         /images/XR_META_Face_Tracking_Viseme_E.jpg "The E Viseme"
[XR_META_Face_Tracking_Viseme_E_rot]:     /images/XR_META_Face_Tracking_Viseme_E_rot.jpg "The E Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_E_emp]:     /images/XR_META_Face_Tracking_Viseme_E_emp.jpg "The E Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_FF]:        /images/XR_META_Face_Tracking_Viseme_FF.jpg "The FF Viseme"
[XR_META_Face_Tracking_Viseme_FF_rot]:    /images/XR_META_Face_Tracking_Viseme_FF_rot.jpg "The FF Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_FF_emp]:    /images/XR_META_Face_Tracking_Viseme_FF_emp.jpg "The FF Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_IH]:        /images/XR_META_Face_Tracking_Viseme_IH.jpg "The IH Viseme"
[XR_META_Face_Tracking_Viseme_IH_rot]:    /images/XR_META_Face_Tracking_Viseme_IH_rot.jpg "The IH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_IH_emp]:    /images/XR_META_Face_Tracking_Viseme_IH_emp.jpg "The IH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_KK]:        /images/XR_META_Face_Tracking_Viseme_KK.jpg "The KK Viseme"
[XR_META_Face_Tracking_Viseme_KK_rot]:    /images/XR_META_Face_Tracking_Viseme_KK_rot.jpg "The KK Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_KK_emp]:    /images/XR_META_Face_Tracking_Viseme_KK_emp.jpg "The KK Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_NN]:        /images/XR_META_Face_Tracking_Viseme_NN.jpg "The NN Viseme"
[XR_META_Face_Tracking_Viseme_NN_rot]:    /images/XR_META_Face_Tracking_Viseme_NN_rot.jpg "The NN Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_NN_emp]:    /images/XR_META_Face_Tracking_Viseme_NN_emp.jpg "The NN Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_OH]:        /images/XR_META_Face_Tracking_Viseme_OH.jpg "The OH Viseme"
[XR_META_Face_Tracking_Viseme_OH_rot]:    /images/XR_META_Face_Tracking_Viseme_OH_rot.jpg "The OH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_OH_emp]:    /images/XR_META_Face_Tracking_Viseme_OH_emp.jpg "The OH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_OU]:        /images/XR_META_Face_Tracking_Viseme_OU.jpg "The OU Viseme"
[XR_META_Face_Tracking_Viseme_OU_rot]:    /images/XR_META_Face_Tracking_Viseme_OU_rot.jpg "The OU Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_OU_emp]:    /images/XR_META_Face_Tracking_Viseme_OU_emp.jpg "The OU Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_PP]:        /images/XR_META_Face_Tracking_Viseme_PP.jpg "The PP Viseme"
[XR_META_Face_Tracking_Viseme_PP_rot]:    /images/XR_META_Face_Tracking_Viseme_PP_rot.jpg "The PP Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_PP_emp]:    /images/XR_META_Face_Tracking_Viseme_PP_emp.jpg "The PP Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_RR]:        /images/XR_META_Face_Tracking_Viseme_RR.jpg "The RR Viseme"
[XR_META_Face_Tracking_Viseme_RR_rot]:    /images/XR_META_Face_Tracking_Viseme_RR_rot.jpg "The RR Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_RR_emp]:    /images/XR_META_Face_Tracking_Viseme_RR_emp.jpg "The RR Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_SIL]:       /images/XR_META_Face_Tracking_Viseme_SIL.jpg "The SIL Viseme"
[XR_META_Face_Tracking_Viseme_SIL_rot]:   /images/XR_META_Face_Tracking_Viseme_SIL_rot.jpg "The SIL Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_SS]:        /images/XR_META_Face_Tracking_Viseme_SS.jpg "The SS Viseme"
[XR_META_Face_Tracking_Viseme_SS_rot]:    /images/XR_META_Face_Tracking_Viseme_SS_rot.jpg "The SS Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_SS_emp]:    /images/XR_META_Face_Tracking_Viseme_SS_emp.jpg "The SS Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_TH]:        /images/XR_META_Face_Tracking_Viseme_TH.jpg "The TH Viseme"
[XR_META_Face_Tracking_Viseme_TH_rot]:    /images/XR_META_Face_Tracking_Viseme_TH_rot.jpg "The TH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_TH_emp]:    /images/XR_META_Face_Tracking_Viseme_TH_emp.jpg "The TH Viseme (emphasized)"

[XR_BODY_JOINT_ROOT_FB]:                    /images/XR_BODY_JOINT_ROOT_FB.png "The XR_BODY_JOINT_ROOT_FB bone joint."
[XR_BODY_JOINT_HIPS_FB]:                    /images/XR_BODY_JOINT_HIPS_FB.png "The XR_BODY_JOINT_HIPS_FB bone joint."
[XR_BODY_JOINT_SPINE_LOWER_FB]:             /images/XR_BODY_JOINT_SPINE_LOWER_FB.png "The XR_BODY_JOINT_SPINE_LOWER_FB bone joint."
[XR_BODY_JOINT_SPINE_MIDDLE_FB]:            /images/XR_BODY_JOINT_SPINE_MIDDLE_FB.png "The XR_BODY_JOINT_SPINE_MIDDLE_FB bone joint."
[XR_BODY_JOINT_SPINE_UPPER_FB]:             /images/XR_BODY_JOINT_SPINE_UPPER_FB.png "The XR_BODY_JOINT_SPINE_UPPER_FB bone joint."
[XR_BODY_JOINT_CHEST_FB]:                   /images/XR_BODY_JOINT_CHEST_FB.png "The XR_BODY_JOINT_CHEST_FB bone joint."
[XR_BODY_JOINT_NECK_FB]:                    /images/XR_BODY_JOINT_NECK_FB.png "The XR_BODY_JOINT_NECK_FB bone joint."
[XR_BODY_JOINT_HEAD_FB]:                    /images/XR_BODY_JOINT_HEAD_FB.png "The XR_BODY_JOINT_HEAD_FB bone joint."
[XR_BODY_JOINT_LEFT_SHOULDER_FB]:           /images/XR_BODY_JOINT_LEFT_SHOULDER_FB.png "The XR_BODY_JOINT_LEFT_SHOULDER_FB bone joint."
[XR_BODY_JOINT_LEFT_SCAPULA_FB]:            /images/XR_BODY_JOINT_LEFT_SCAPULA_FB.png "The XR_BODY_JOINT_LEFT_SCAPULA_FB bone joint."
[XR_BODY_JOINT_LEFT_ARM_UPPER_FB]:          /images/XR_BODY_JOINT_LEFT_ARM_UPPER_FB.png "The XR_BODY_JOINT_LEFT_ARM_UPPER_FB bone joint."
[XR_BODY_JOINT_LEFT_ARM_LOWER_FB]:          /images/XR_BODY_JOINT_LEFT_ARM_LOWER_FB.png "The XR_BODY_JOINT_LEFT_ARM_LOWER_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB bone joint."
[XR_BODY_JOINT_RIGHT_SHOULDER_FB]:          /images/XR_BODY_JOINT_RIGHT_SHOULDER_FB.png "The XR_BODY_JOINT_RIGHT_SHOULDER_FB bone joint."
[XR_BODY_JOINT_RIGHT_SCAPULA_FB]:           /images/XR_BODY_JOINT_RIGHT_SCAPULA_FB.png "The XR_BODY_JOINT_RIGHT_SCAPULA_FB bone joint."
[XR_BODY_JOINT_RIGHT_ARM_UPPER_FB]:         /images/XR_BODY_JOINT_RIGHT_ARM_UPPER_FB.png "The XR_BODY_JOINT_RIGHT_ARM_UPPER_FB bone joint."
[XR_BODY_JOINT_RIGHT_ARM_LOWER_FB]:         /images/XR_BODY_JOINT_RIGHT_ARM_LOWER_FB.png "The XR_BODY_JOINT_RIGHT_ARM_LOWER_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_PALM_FB]:              /images/XR_BODY_JOINT_LEFT_HAND_PALM_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_PALM_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_WRIST_FB]:             /images/XR_BODY_JOINT_LEFT_HAND_WRIST_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_WRIST_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB]:  /images/XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB]:    /images/XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB]:      /images/XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB]:         /images/XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB]:  /images/XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB]:    /images/XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB]:      /images/XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB]:         /images/XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB]: /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB]:        /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB]:       /images/XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB]:          /images/XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB]: /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB]:        /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_PALM_FB]:             /images/XR_BODY_JOINT_RIGHT_HAND_PALM_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_PALM_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_WRIST_FB]:            /images/XR_BODY_JOINT_RIGHT_HAND_WRIST_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_WRIST_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB]:   /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB]:     /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB]:        /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB]:   /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB]:     /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB]:        /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB]:       /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB]:      /images/XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB]:         /images/XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB]:       /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB bone joint."

Eye tracking technology detects eye movements to control an avatar's eye transformations as the user looks around. The Meta Quest Pro headset is the only device that supports this feature, utilizing the [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) script.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## Overview
The [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) MonoBehaviour component provides eye tracking or gaze information. It retrieves eye pose data from the OVRPlugin in tracking space. When you add the [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) component to a GameObject, it can simulate an eye, updating its position and orientation based on actual human eye movements. This component enhances the expressiveness of both realistic and stylized characters and can select objects in a scene using raycasts. If you do not setup the necessary eye tracking permissions, tracking will not occur.

## Policies and Disclaimers

{::comment}**DEVICES**{:/comment}

{::comment}**MOVEMENT SDK**{:/comment}

{::comment}**BODY TRACKING**{:/comment}

{::comment}**CHARACTER**{:/comment}

{::comment}**EYE TRACKING**{:/comment}

{::comment}**FACE TRACKING**{:/comment}

{::comment}**DEVICES**{:/comment}

{::comment}**MOVEMENT SDK**{:/comment}

{::comment}**BODY TRACKING**{:/comment}

{::comment}**CHARACTER**{:/comment}

{::comment}**EYE TRACKING**{:/comment}

{::comment}**FACE TRACKING**{:/comment}

[dup]:                      /policy/data-use/ "Developer Data Use Policy"
[dup-prohibited]:           /policy/data-use/#section-4-prohibited-uses-of-user-data "Developer Data Use Policy: Prohibited Practices"

[oc-sdk-license-agreement]: https://developers.meta.com/horizon/licenses/pc-3.3/ "Oculus Software Development Kit License Agreement"

{::comment}**TOPICS**{:/comment}

[move]:                     /documentation/unity/move-overview/ "Movement"
[move-openxr]:              /documentation/native/android/move-overview/ "Movement OpenXR Extensions"

[api]:                      /documentation/unity/move-ref-api/ "API Reference"
[api-openxr]:               /documentation/native/android/move-ref-api/ "API Reference"

[blendshapes]:              /documentation/unity/move-ref-blendshapes/ "Blendshape Visual Reference"
[blendshapes-openxr]:       /documentation/native/android/move-ref-blendshapes/ "Blendshape Visual Reference"

[body-joints]:              /documentation/unity/move-ref-body-joints/ "Body Joint Visual Reference"
[body-joints-openxr]:       /documentation/native/android/move-ref-body-joints/ "Body Joint Visual Reference"

[bt]:                       /documentation/unity/move-body-tracking/ "Body Tracking"
[bt-openxr]:                /documentation/native/android/move-body-tracking/ "Movement Body Tracking OpenXR Extensions"

[et]:                       /documentation/unity/move-eye-tracking/ "Eye Tracking"
[et-openxr]:                /documentation/native/android/move-eye-tracking/ "Movement Eye Tracking OpenXR Extensions"

[ft]:                       /documentation/unity/move-face-tracking/ "Natural Facial Expressions"
[ft-openxr]:                /documentation/native/android/move-face-tracking/ "Movement Face Tracking OpenXR Extensions"

[policies]:                 /documentation/unity/move-policy-data-disclaimer/" "Policies and Disclaimers"
[policies-openxr]:          /documentation/native/android/move-policy-data-disclaimer/" "Policies and Disclaimers"

[retargeting-bones]:        /documentation/unity/move-retargeting-bones/ "Retargeting Bones"
[retargeting-bones-openxr]: /documentation/native/android/move-retargeting-bones/ "Retargeting Bones"

{::comment}**UNITY SCRIPTS AND COMPONENTS**{:/comment}

[OVRBody]:                  /documentation/unity/move-ref-scripts/#ovrbody "The OVRBody component."
[OVRBody.cs]:               https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_body/ "OVRBody API Reference"

[OVRCustomSkeleton]:        /documentation/unity/move-ref-scripts/#ovrcustomskeleton "The OVRCustomSkeleton component."
[OVRCustomSkeleton.cs]:     https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_custom_skeleton/ "OVRCustomSkeleton API Reference"

[OVREyeGaze]:               /documentation/unity/move-ref-scripts/#ovreyegaze "The OVREyeGaze component."
[OVREyeGaze.cs]:            https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_eye_gaze/ "OVREyeGaze API Reference"

[OVRFaceExpressions]:       /documentation/unity/move-ref-scripts/#ovrfaceexpressions "The OVRFaceExpressions component."
[OVRFaceExpressions.cs]:    https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_face_expressions/ "OVRFaceExpressions API Reference"

[OVRSkeleton]:              /documentation/unity/move-ref-scripts/#ovrskeleton "The OVRSkeleton component."
[OVRSkeleton.cs]:           https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_skeleton/ "OVRSkeleton API Reference"

[Auto-Map-Bones]:               /images/movement-ovr-custom-skeleton-automap-button.png "Auto-Mapping Bones in a Custom Skeleton"

[Common-Custom-Skeleton-Setup]: /images/movement-ovr-common-setup-custom-skeleton-component.png "Common Setup For A Custom Skeleton"

[Movement-Enable-Tracking]:     /images/movement-enable-tracking.png "Settings for Enabling Tracking Features"

[Movement-OVR-Eye-Gaze]:        /images/movement-ovr-eye-gaze-component.png "OVR Eye Gaze Component"

[Movement-SDK-Splash]:          /images/movement-splash.jpg "Movement SDK"
{: height="718px" width="1000px" }

[Skeleton-Bones]:               /images/movement-skeleton-bones.png "Skeleton Bones"
[Movement-Samples-Fitness]:          /images/move-sample-fitness.png "Move Samples Fitness"
[Movement-Samples-Locomotion]:          /images/move-sample-locomotion.png "Move Samples Locomotion"
[Movement-Samples-ISDK-Integration]:          /images/move-sample-isdk-integration.png "Move Samples ISDK Integration"

[XR_Face_Expression_Neutral]:                /images/XR_Face_Expression_Neutral.png "The Neutral OpenXR Face Expression Blendshape"
[XR_Face_Expression_Brow_Lowerer_L]:         /images/XR_Face_Expression_Brow_Lowerer_L.png "The XR_Face_Expression_Brow_Lowerer_L Blendshape"
[XR_Face_Expression_Brow_Lowerer_R]:         /images/XR_Face_Expression_Brow_Lowerer_R.png "The XR_Face_Expression_Brow_Lowerer_R Blendshape"
[XR_Face_Expression_Cheek_Puff_L]:           /images/XR_Face_Expression_Cheek_Puff_L.png "The XR_Face_Expression_Cheek_Puff_L Blendshape"
[XR_Face_Expression_Cheek_Puff_R]:           /images/XR_Face_Expression_Cheek_Puff_R.png "The XR_Face_Expression_Cheek_Puff_R Blendshape"
[XR_Face_Expression_Cheek_Raiser_L]:         /images/XR_Face_Expression_Cheek_Raiser_L.png "The XR_Face_Expression_Cheek_Raiser_L Blendshape"
[XR_Face_Expression_Cheek_Raiser_R]:         /images/XR_Face_Expression_Cheek_Raiser_R.png "The XR_Face_Expression_Cheek_Raiser_R Blendshape"
[XR_Face_Expression_Cheek_Suck_L]:           /images/XR_Face_Expression_Cheek_Suck_L.png "The XR_Face_Expression_Cheek_Suck_L Blendshape"
[XR_Face_Expression_Cheek_Suck_R]:           /images/XR_Face_Expression_Cheek_Suck_R.png "The XR_Face_Expression_Cheek_Suck_R Blendshape"
[XR_Face_Expression_Chin_Raiser_B]:          /images/XR_Face_Expression_Chin_Raiser_B.png "The XR_Face_Expression_Chin_Raiser_B Blendshape"
[XR_Face_Expression_Chin_Raiser_T]:          /images/XR_Face_Expression_Chin_Raiser_T.png "The XR_Face_Expression_Chin_Raiser_T Blendshape"
[XR_Face_Expression_Dimpler_L]:              /images/XR_Face_Expression_Dimpler_L.png "The XR_Face_Expression_Dimpler_L Blendshape"
[XR_Face_Expression_Dimpler_R]:              /images/XR_Face_Expression_Dimpler_R.png "The XR_Face_Expression_Dimpler_R Blendshape"
[XR_Face_Expression_Eyes_Closed_L]:          /images/XR_Face_Expression_Eyes_Closed_L.png "The XR_Face_Expression_Eyes_Closed_L Blendshape"
[XR_Face_Expression_Eyes_Closed_R]:          /images/XR_Face_Expression_Eyes_Closed_R.png "The XR_Face_Expression_Eyes_Closed_R Blendshape"
[XR_Face_Expression_Eyes_Look_Down_L]:       /images/XR_Face_Expression_Eyes_Look_Down_L.png "The XR_Face_Expression_Eyes_Look_Down_L Blendshape"
[XR_Face_Expression_Eyes_Look_Down_R]:       /images/XR_Face_Expression_Eyes_Look_Down_R.png "The XR_Face_Expression_Eyes_Look_Down_R Blendshape"
[XR_Face_Expression_Eyes_Look_Left_L]:       /images/XR_Face_Expression_Eyes_Look_Left_L.png "The XR_Face_Expression_Eyes_Look_Left_L Blendshape"
[XR_Face_Expression_Eyes_Look_Left_R]:       /images/XR_Face_Expression_Eyes_Look_Left_R.png "The XR_Face_Expression_Eyes_Look_Left_R Blendshape"
[XR_Face_Expression_Eyes_Look_Right_L]:      /images/XR_Face_Expression_Eyes_Look_Right_L.png "The XR_Face_Expression_Eyes_Look_Right_L Blendshape"
[XR_Face_Expression_Eyes_Look_Right_R]:      /images/XR_Face_Expression_Eyes_Look_Right_R.png "The XR_Face_Expression_Eyes_Look_Right_R Blendshape"
[XR_Face_Expression_Eyes_Look_Up_L]:         /images/XR_Face_Expression_Eyes_Look_Up_L.png "The XR_Face_Expression_Eyes_Look_Up_L Blendshape"
[XR_Face_Expression_Eyes_Look_Up_R]:         /images/XR_Face_Expression_Eyes_Look_Up_R.png "The XR_Face_Expression_Eyes_Look_Up_R Blendshape"
[XR_Face_Expression_Inner_Brow_Raiser_L]:    /images/XR_Face_Expression_Inner_Brow_Raiser_L.png "The XR_Face_Expression_Inner_Brow_Raiser_L Blendshape"
[XR_Face_Expression_Inner_Brow_Raiser_R]:    /images/XR_Face_Expression_Inner_Brow_Raiser_R.png "The XR_Face_Expression_Inner_Brow_Raiser_R Blendshape"
[XR_Face_Expression_Jaw_Drop]:               /images/XR_Face_Expression_Jaw_Drop.png "The XR_Face_Expression_Jaw_Drop Blendshape"
[XR_Face_Expression_Jaw_Sideways_Left]:      /images/XR_Face_Expression_Jaw_Sideways_Left.png "The XR_Face_Expression_Jaw_Sideways_Left Blendshape"
[XR_Face_Expression_Jaw_Sideways_Right]:     /images/XR_Face_Expression_Jaw_Sideways_Right.png "The XR_Face_Expression_Jaw_Sideways_Right Blendshape"
[XR_Face_Expression_Jaw_Thrust]:             /images/XR_Face_Expression_Jaw_Thrust.png "The XR_Face_Expression_Jaw_Thrust Blendshape"
[XR_Face_Expression_Lid_Tightener_L]:        /images/XR_Face_Expression_Lid_Tightener_L.png "The XR_Face_Expression_Lid_Tightener_L Blendshape"
[XR_Face_Expression_Lid_Tightener_R]:        /images/XR_Face_Expression_Lid_Tightener_R.png "The XR_Face_Expression_Lid_Tightener_R Blendshape"
[XR_Face_Expression_Lip_Corner_Depressor_L]: /images/XR_Face_Expression_Lip_Corner_Depressor_L.png "The XR_Face_Expression_Lip_Corner_Depressor_L Blendshape"
[XR_Face_Expression_Lip_Corner_Depressor_R]: /images/XR_Face_Expression_Lip_Corner_Depressor_R.png "The XR_Face_Expression_Lip_Corner_Depressor_R Blendshape"
[XR_Face_Expression_Lip_Corner_Puller_L]:    /images/XR_Face_Expression_Lip_Corner_Puller_L.png "The XR_Face_Expression_Lip_Corner_Puller_L Blendshape"
[XR_Face_Expression_Lip_Corner_Puller_R]:    /images/XR_Face_Expression_Lip_Corner_Puller_R.png "The XR_Face_Expression_Lip_Corner_Puller_R Blendshape"
[XR_Face_Expression_Lip_Funneler_LB]:        /images/XR_Face_Expression_Lip_Funneler_LB.png "The XR_Face_Expression_Lip_Funneler_LB Blendshape"
[XR_Face_Expression_Lip_Funneler_LT]:        /images/XR_Face_Expression_Lip_Funneler_LT.png "The XR_Face_Expression_Lip_Funneler_LT Blendshape"
[XR_Face_Expression_Lip_Funneler_RB]:        /images/XR_Face_Expression_Lip_Funneler_RB.png "The XR_Face_Expression_Lip_Funneler_RB Blendshape"
[XR_Face_Expression_Lip_Funneler_RT]:        /images/XR_Face_Expression_Lip_Funneler_RT.png "The XR_Face_Expression_Lip_Funneler_RT Blendshape"
[XR_Face_Expression_Lip_Pressor_L]:          /images/XR_Face_Expression_Lip_Pressor_L.png "The XR_Face_Expression_Lip_Pressor_L Blendshape"
[XR_Face_Expression_Lip_Pressor_R]:          /images/XR_Face_Expression_Lip_Pressor_R.png "The XR_Face_Expression_Lip_Pressor_R Blendshape"
[XR_Face_Expression_Lip_Pucker_L]:           /images/XR_Face_Expression_Lip_Pucker_L.png "The XR_Face_Expression_Lip_Pucker_L Blendshape"
[XR_Face_Expression_Lip_Pucker_R]:           /images/XR_Face_Expression_Lip_Pucker_R.png "The XR_Face_Expression_Lip_Pucker_R Blendshape"
[XR_Face_Expression_Lip_Stretcher_L]:        /images/XR_Face_Expression_Lip_Stretcher_L.png "The XR_Face_Expression_Lip_Stretcher_L Blendshape"
[XR_Face_Expression_Lip_Stretcher_R]:        /images/XR_Face_Expression_Lip_Stretcher_R.png "The XR_Face_Expression_Lip_Stretcher_R Blendshape"
[XR_Face_Expression_Lip_Suck_LB]:            /images/XR_Face_Expression_Lip_Suck_LB.png "The XR_Face_Expression_Lip_Suck_LB Blendshape"
[XR_Face_Expression_Lip_Suck_LT]:            /images/XR_Face_Expression_Lip_Suck_LT.png "The XR_Face_Expression_Lip_Suck_LT Blendshape"
[XR_Face_Expression_Lip_Suck_RB]:            /images/XR_Face_Expression_Lip_Suck_RB.png "The XR_Face_Expression_Lip_Suck_RB Blendshape"
[XR_Face_Expression_Lip_Suck_RT]:            /images/XR_Face_Expression_Lip_Suck_RT.png "The XR_Face_Expression_Lip_Suck_RT Blendshape"
[XR_Face_Expression_Lip_Tightener_L]:        /images/XR_Face_Expression_Lip_Tightener_L.png "The XR_Face_Expression_Lip_Tightener_L Blendshape"
[XR_Face_Expression_Lip_Tightener_R]:        /images/XR_Face_Expression_Lip_Tightener_R.png "The XR_Face_Expression_Lip_Tightener_R Blendshape"
[XR_Face_Expression_Lips_Toward]:            /images/XR_Face_Expression_Lips_Toward.png "The XR_Face_Expression_Lips_Toward Blendshape"
[XR_Face_Expression_Lower_Lip_Depressor_L]:  /images/XR_Face_Expression_Lower_Lip_Depressor_L.png "The XR_Face_Expression_Lower_Lip_Depressor_L Blendshape"
[XR_Face_Expression_Lower_Lip_Depressor_R]:  /images/XR_Face_Expression_Lower_Lip_Depressor_R.png "The XR_Face_Expression_Lower_Lip_Depressor_R Blendshape"
[XR_Face_Expression_Mouth_Left]:             /images/XR_Face_Expression_Mouth_Left.png "The XR_Face_Expression_Mouth_Left Blendshape"
[XR_Face_Expression_Mouth_Right]:            /images/XR_Face_Expression_Mouth_Right.png "The XR_Face_Expression_Mouth_Right Blendshape"
[XR_Face_Expression_Nose_Wrinkler_L]:        /images/XR_Face_Expression_Nose_Wrinkler_L.png "The XR_Face_Expression_Nose_Wrinkler_L Blendshape"
[XR_Face_Expression_Nose_Wrinkler_R]:        /images/XR_Face_Expression_Nose_Wrinkler_R.png "The XR_Face_Expression_Nose_Wrinkler_R Blendshape"
[XR_Face_Expression_Outer_Brow_Raiser_L]:    /images/XR_Face_Expression_Outer_Brow_Raiser_L.png "The XR_Face_Expression_Outer_Brow_Raiser_L Blendshape"
[XR_Face_Expression_Outer_Brow_Raiser_R]:    /images/XR_Face_Expression_Outer_Brow_Raiser_R.png "The XR_Face_Expression_Outer_Brow_Raiser_R Blendshape"
[XR_Face_Expression_Upper_Lid_Raiser_L]:     /images/XR_Face_Expression_Upper_Lid_Raiser_L.png "The XR_Face_Expression_Upper_Lid_Raiser_L Blendshape"
[XR_Face_Expression_Upper_Lid_Raiser_R]:     /images/XR_Face_Expression_Upper_Lid_Raiser_R.png "The XR_Face_Expression_Upper_Lid_Raiser_R Blendshape"
[XR_Face_Expression_Upper_Lip_Raiser_L]:     /images/XR_Face_Expression_Upper_Lip_Raiser_L.png "The XR_Face_Expression_Upper_Lip_Raiser_L Blendshape"
[XR_Face_Expression_Upper_Lip_Raiser_R]:     /images/XR_Face_Expression_Upper_Lip_Raiser_R.png "The XR_Face_Expression_Upper_Lip_Raiser_R Blendshape"

[XR_Face_Expression_2_Brow_Lowerer_L]:         /images/XR_Face_Expression_2_Brow_Lowerer_L.png "The XR_Face_Expression_Brow_Lowerer_L Blendshape"
[XR_Face_Expression_2_Brow_Lowerer_R]:         /images/XR_Face_Expression_2_Brow_Lowerer_R.png "The XR_Face_Expression_Brow_Lowerer_R Blendshape"
[XR_Face_Expression_2_Cheek_Puff_L]:           /images/XR_Face_Expression_2_Cheek_Puff_L.png "The XR_Face_Expression_Cheek_Puff_L Blendshape"
[XR_Face_Expression_2_Cheek_Puff_R]:           /images/XR_Face_Expression_2_Cheek_Puff_R.png "The XR_Face_Expression_Cheek_Puff_R Blendshape"
[XR_Face_Expression_2_Cheek_Raiser_L]:         /images/XR_Face_Expression_2_Cheek_Raiser_L.png "The XR_Face_Expression_Cheek_Raiser_L Blendshape"
[XR_Face_Expression_2_Cheek_Raiser_R]:         /images/XR_Face_Expression_2_Cheek_Raiser_R.png "The XR_Face_Expression_Cheek_Raiser_R Blendshape"
[XR_Face_Expression_2_Cheek_Suck_L]:           /images/XR_Face_Expression_2_Cheek_Suck_L.png "The XR_Face_Expression_Cheek_Suck_L Blendshape"
[XR_Face_Expression_2_Cheek_Suck_R]:           /images/XR_Face_Expression_2_Cheek_Suck_R.png "The XR_Face_Expression_Cheek_Suck_R Blendshape"
[XR_Face_Expression_2_Chin_Raiser_B]:          /images/XR_Face_Expression_2_Chin_Raiser_B.png "The XR_Face_Expression_Chin_Raiser_B Blendshape"
[XR_Face_Expression_2_Chin_Raiser_T]:          /images/XR_Face_Expression_2_Chin_Raiser_T.png "The XR_Face_Expression_Chin_Raiser_T Blendshape"
[XR_Face_Expression_2_Dimpler_L]:              /images/XR_Face_Expression_2_Dimpler_L.png "The XR_Face_Expression_Dimpler_L Blendshape"
[XR_Face_Expression_2_Dimpler_R]:              /images/XR_Face_Expression_2_Dimpler_R.png "The XR_Face_Expression_Dimpler_R Blendshape"
[XR_Face_Expression_2_Eyes_Closed_L]:          /images/XR_Face_Expression_2_Eyes_Closed_L.png "The XR_Face_Expression_Eyes_Closed_L Blendshape"
[XR_Face_Expression_2_Eyes_Closed_R]:          /images/XR_Face_Expression_2_Eyes_Closed_R.png "The XR_Face_Expression_Eyes_Closed_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Down_L]:       /images/XR_Face_Expression_2_Eyes_Look_Down_L.png "The XR_Face_Expression_Eyes_Look_Down_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Down_R]:       /images/XR_Face_Expression_2_Eyes_Look_Down_R.png "The XR_Face_Expression_Eyes_Look_Down_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Left_L]:       /images/XR_Face_Expression_2_Eyes_Look_Left_L.png "The XR_Face_Expression_Eyes_Look_Left_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Left_R]:       /images/XR_Face_Expression_2_Eyes_Look_Left_R.png "The XR_Face_Expression_Eyes_Look_Left_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Right_L]:      /images/XR_Face_Expression_2_Eyes_Look_Right_L.png "The XR_Face_Expression_Eyes_Look_Right_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Right_R]:      /images/XR_Face_Expression_2_Eyes_Look_Right_R.png "The XR_Face_Expression_Eyes_Look_Right_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Up_L]:         /images/XR_Face_Expression_2_Eyes_Look_Up_L.png "The XR_Face_Expression_Eyes_Look_Up_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Up_R]:         /images/XR_Face_Expression_2_Eyes_Look_Up_R.png "The XR_Face_Expression_Eyes_Look_Up_R Blendshape"
[XR_Face_Expression_2_Inner_Brow_Raiser_L]:    /images/XR_Face_Expression_2_Inner_Brow_Raiser_L.png "The XR_Face_Expression_Inner_Brow_Raiser_L Blendshape"
[XR_Face_Expression_2_Inner_Brow_Raiser_R]:    /images/XR_Face_Expression_2_Inner_Brow_Raiser_R.png "The XR_Face_Expression_Inner_Brow_Raiser_R Blendshape"
[XR_Face_Expression_2_Jaw_Drop]:               /images/XR_Face_Expression_2_Jaw_Drop.png "The XR_Face_Expression_Jaw_Drop Blendshape"
[XR_Face_Expression_2_Jaw_Sideways_Left]:      /images/XR_Face_Expression_2_Jaw_Sideways_Left.png "The XR_Face_Expression_Jaw_Sideways_Left Blendshape"
[XR_Face_Expression_2_Jaw_Sideways_Right]:     /images/XR_Face_Expression_2_Jaw_Sideways_Right.png "The XR_Face_Expression_Jaw_Sideways_Right Blendshape"
[XR_Face_Expression_2_Jaw_Thrust]:             /images/XR_Face_Expression_2_Jaw_Thrust.png "The XR_Face_Expression_Jaw_Thrust Blendshape"
[XR_Face_Expression_2_Lid_Tightener_L]:        /images/XR_Face_Expression_2_Lid_Tightener_L.png "The XR_Face_Expression_Lid_Tightener_L Blendshape"
[XR_Face_Expression_2_Lid_Tightener_R]:        /images/XR_Face_Expression_2_Lid_Tightener_R.png "The XR_Face_Expression_Lid_Tightener_R Blendshape"
[XR_Face_Expression_2_Lip_Corner_Depressor_L]: /images/XR_Face_Expression_2_Lip_Corner_Depressor_L.png "The XR_Face_Expression_Lip_Corner_Depressor_L Blendshape"
[XR_Face_Expression_2_Lip_Corner_Depressor_R]: /images/XR_Face_Expression_2_Lip_Corner_Depressor_R.png "The XR_Face_Expression_Lip_Corner_Depressor_R Blendshape"
[XR_Face_Expression_2_Lip_Corner_Puller_L]:    /images/XR_Face_Expression_2_Lip_Corner_Puller_L.png "The XR_Face_Expression_Lip_Corner_Puller_L Blendshape"
[XR_Face_Expression_2_Lip_Corner_Puller_R]:    /images/XR_Face_Expression_2_Lip_Corner_Puller_R.png "The XR_Face_Expression_Lip_Corner_Puller_R Blendshape"
[XR_Face_Expression_2_Lip_Funneler_LB]:        /images/XR_Face_Expression_2_Lip_Funneler_LB.png "The XR_Face_Expression_Lip_Funneler_LB Blendshape"
[XR_Face_Expression_2_Lip_Funneler_LT]:        /images/XR_Face_Expression_2_Lip_Funneler_LT.png "The XR_Face_Expression_Lip_Funneler_LT Blendshape"
[XR_Face_Expression_2_Lip_Funneler_RB]:        /images/XR_Face_Expression_2_Lip_Funneler_RB.png "The XR_Face_Expression_Lip_Funneler_RB Blendshape"
[XR_Face_Expression_2_Lip_Funneler_RT]:        /images/XR_Face_Expression_2_Lip_Funneler_RT.png "The XR_Face_Expression_Lip_Funneler_RT Blendshape"
[XR_Face_Expression_2_Lip_Pressor_L]:          /images/XR_Face_Expression_2_Lip_Pressor_L.png "The XR_Face_Expression_Lip_Pressor_L Blendshape"
[XR_Face_Expression_2_Lip_Pressor_R]:          /images/XR_Face_Expression_2_Lip_Pressor_R.png "The XR_Face_Expression_Lip_Pressor_R Blendshape"
[XR_Face_Expression_2_Lip_Pucker_L]:           /images/XR_Face_Expression_2_Lip_Pucker_L.png "The XR_Face_Expression_Lip_Pucker_L Blendshape"
[XR_Face_Expression_2_Lip_Pucker_R]:           /images/XR_Face_Expression_2_Lip_Pucker_R.png "The XR_Face_Expression_Lip_Pucker_R Blendshape"
[XR_Face_Expression_2_Lip_Stretcher_L]:        /images/XR_Face_Expression_2_Lip_Stretcher_L.png "The XR_Face_Expression_Lip_Stretcher_L Blendshape"
[XR_Face_Expression_2_Lip_Stretcher_R]:        /images/XR_Face_Expression_2_Lip_Stretcher_R.png "The XR_Face_Expression_Lip_Stretcher_R Blendshape"
[XR_Face_Expression_2_Lip_Suck_LB]:            /images/XR_Face_Expression_2_Lip_Suck_LB.png "The XR_Face_Expression_Lip_Suck_LB Blendshape"
[XR_Face_Expression_2_Lip_Suck_LT]:            /images/XR_Face_Expression_2_Lip_Suck_LT.png "The XR_Face_Expression_Lip_Suck_LT Blendshape"
[XR_Face_Expression_2_Lip_Suck_RB]:            /images/XR_Face_Expression_2_Lip_Suck_RB.png "The XR_Face_Expression_Lip_Suck_RB Blendshape"
[XR_Face_Expression_2_Lip_Suck_RT]:            /images/XR_Face_Expression_2_Lip_Suck_RT.png "The XR_Face_Expression_Lip_Suck_RT Blendshape"
[XR_Face_Expression_2_Lip_Tightener_L]:        /images/XR_Face_Expression_2_Lip_Tightener_L.png "The XR_Face_Expression_Lip_Tightener_L Blendshape"
[XR_Face_Expression_2_Lip_Tightener_R]:        /images/XR_Face_Expression_2_Lip_Tightener_R.png "The XR_Face_Expression_Lip_Tightener_R Blendshape"
[XR_Face_Expression_2_Lips_Toward]:            /images/XR_Face_Expression_2_Lips_Toward.png "The XR_Face_Expression_Lips_Toward Blendshape"
[XR_Face_Expression_2_Lower_Lip_Depressor_L]:  /images/XR_Face_Expression_2_Lower_Lip_Depressor_L.png "The XR_Face_Expression_Lower_Lip_Depressor_L Blendshape"
[XR_Face_Expression_2_Lower_Lip_Depressor_R]:  /images/XR_Face_Expression_2_Lower_Lip_Depressor_R.png "The XR_Face_Expression_Lower_Lip_Depressor_R Blendshape"
[XR_Face_Expression_2_Mouth_Left]:             /images/XR_Face_Expression_2_Mouth_Left.png "The XR_Face_Expression_Mouth_Left Blendshape"
[XR_Face_Expression_2_Mouth_Right]:            /images/XR_Face_Expression_2_Mouth_Right.png "The XR_Face_Expression_Mouth_Right Blendshape"
[XR_Face_Expression_2_Nose_Wrinkler_L]:        /images/XR_Face_Expression_2_Nose_Wrinkler_L.png "The XR_Face_Expression_Nose_Wrinkler_L Blendshape"
[XR_Face_Expression_2_Nose_Wrinkler_R]:        /images/XR_Face_Expression_2_Nose_Wrinkler_R.png "The XR_Face_Expression_Nose_Wrinkler_R Blendshape"
[XR_Face_Expression_2_Outer_Brow_Raiser_L]:    /images/XR_Face_Expression_2_Outer_Brow_Raiser_L.png "The XR_Face_Expression_Outer_Brow_Raiser_L Blendshape"
[XR_Face_Expression_2_Outer_Brow_Raiser_R]:    /images/XR_Face_Expression_2_Outer_Brow_Raiser_R.png "The XR_Face_Expression_Outer_Brow_Raiser_R Blendshape"
[XR_Face_Expression_2_Upper_Lid_Raiser_L]:     /images/XR_Face_Expression_2_Upper_Lid_Raiser_L.png "The XR_Face_Expression_Upper_Lid_Raiser_L Blendshape"
[XR_Face_Expression_2_Upper_Lid_Raiser_R]:     /images/XR_Face_Expression_2_Upper_Lid_Raiser_R.png "The XR_Face_Expression_Upper_Lid_Raiser_R Blendshape"
[XR_Face_Expression_2_Upper_Lip_Raiser_L]:     /images/XR_Face_Expression_2_Upper_Lip_Raiser_L.png "The XR_Face_Expression_Upper_Lip_Raiser_L Blendshape"
[XR_Face_Expression_2_Upper_Lip_Raiser_R]:     /images/XR_Face_Expression_2_Upper_Lip_Raiser_R.png "The XR_Face_Expression_Upper_Lip_Raiser_R Blendshape"
[XR_Face_Expression_2_Tongue_Tip_Interdental]:     /images/XR_Face_Expression_2_Tongue_Tip_Interdental.png "The XR_Face_Expression_Tongue_Tip_Interdental Blendshape"
[XR_Face_Expression_2_Tongue_Tip_Alveolar]:        /images/XR_Face_Expression_2_Tongue_Tip_Alveolar.png "The XR_Face_Expression_Tongue_Tip_Alveolar Blendshape"
[XR_Face_Expression_2_Tongue_Front_Dorsal_Palate]: /images/XR_Face_Expression_2_Tongue_Front_Dorsal_Palate.png "The XR_Face_Expression_Tongue_Front_Dorsal_Palate Blendshape"
[XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate]:   /images/XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate.png "The XR_Face_Expression_Tongue_Mid_Dorsal_Palate Blendshape"
[XR_Face_Expression_2_Tongue_Back_Dorsal_Velar]:   /images/XR_Face_Expression_2_Tongue_Back_Dorsal_Velar.png "The XR_Face_Expression_Tongue_Back_Dorsal_Velar Blendshape"
[XR_Face_Expression_2_Tongue_Out]:                 /images/XR_Face_Expression_2_Tongue_Out.png "The XR_Face_Expression_Tongue_Out Blendshape"
[XR_Face_Expression_2_Tongue_Retreat]:             /images/XR_Face_Expression_2_Tongue_Retreat.png "The XR_Face_Expression_Tongue_Retreat Blendshape"

[XR_META_Face_Tracking_Viseme_AA]:        /images/XR_META_Face_Tracking_Viseme_AA.jpg "The AA Viseme"
[XR_META_Face_Tracking_Viseme_AA_rot]:    /images/XR_META_Face_Tracking_Viseme_AA_rot.jpg "The AA Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_AA_emp]:    /images/XR_META_Face_Tracking_Viseme_AA_emp.jpg "The AA Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_CH]:        /images/XR_META_Face_Tracking_Viseme_CH.jpg "The CH Viseme"
[XR_META_Face_Tracking_Viseme_CH_rot]:    /images/XR_META_Face_Tracking_Viseme_CH_rot.jpg "The CH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_CH_emp]:    /images/XR_META_Face_Tracking_Viseme_CH_emp.jpg "The CH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_DD]:        /images/XR_META_Face_Tracking_Viseme_DD.jpg "The DD Viseme"
[XR_META_Face_Tracking_Viseme_DD_rot]:    /images/XR_META_Face_Tracking_Viseme_DD_rot.jpg "The DD Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_DD_emp]:    /images/XR_META_Face_Tracking_Viseme_DD_emp.jpg "The DD Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_E]:         /images/XR_META_Face_Tracking_Viseme_E.jpg "The E Viseme"
[XR_META_Face_Tracking_Viseme_E_rot]:     /images/XR_META_Face_Tracking_Viseme_E_rot.jpg "The E Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_E_emp]:     /images/XR_META_Face_Tracking_Viseme_E_emp.jpg "The E Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_FF]:        /images/XR_META_Face_Tracking_Viseme_FF.jpg "The FF Viseme"
[XR_META_Face_Tracking_Viseme_FF_rot]:    /images/XR_META_Face_Tracking_Viseme_FF_rot.jpg "The FF Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_FF_emp]:    /images/XR_META_Face_Tracking_Viseme_FF_emp.jpg "The FF Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_IH]:        /images/XR_META_Face_Tracking_Viseme_IH.jpg "The IH Viseme"
[XR_META_Face_Tracking_Viseme_IH_rot]:    /images/XR_META_Face_Tracking_Viseme_IH_rot.jpg "The IH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_IH_emp]:    /images/XR_META_Face_Tracking_Viseme_IH_emp.jpg "The IH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_KK]:        /images/XR_META_Face_Tracking_Viseme_KK.jpg "The KK Viseme"
[XR_META_Face_Tracking_Viseme_KK_rot]:    /images/XR_META_Face_Tracking_Viseme_KK_rot.jpg "The KK Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_KK_emp]:    /images/XR_META_Face_Tracking_Viseme_KK_emp.jpg "The KK Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_NN]:        /images/XR_META_Face_Tracking_Viseme_NN.jpg "The NN Viseme"
[XR_META_Face_Tracking_Viseme_NN_rot]:    /images/XR_META_Face_Tracking_Viseme_NN_rot.jpg "The NN Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_NN_emp]:    /images/XR_META_Face_Tracking_Viseme_NN_emp.jpg "The NN Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_OH]:        /images/XR_META_Face_Tracking_Viseme_OH.jpg "The OH Viseme"
[XR_META_Face_Tracking_Viseme_OH_rot]:    /images/XR_META_Face_Tracking_Viseme_OH_rot.jpg "The OH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_OH_emp]:    /images/XR_META_Face_Tracking_Viseme_OH_emp.jpg "The OH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_OU]:        /images/XR_META_Face_Tracking_Viseme_OU.jpg "The OU Viseme"
[XR_META_Face_Tracking_Viseme_OU_rot]:    /images/XR_META_Face_Tracking_Viseme_OU_rot.jpg "The OU Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_OU_emp]:    /images/XR_META_Face_Tracking_Viseme_OU_emp.jpg "The OU Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_PP]:        /images/XR_META_Face_Tracking_Viseme_PP.jpg "The PP Viseme"
[XR_META_Face_Tracking_Viseme_PP_rot]:    /images/XR_META_Face_Tracking_Viseme_PP_rot.jpg "The PP Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_PP_emp]:    /images/XR_META_Face_Tracking_Viseme_PP_emp.jpg "The PP Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_RR]:        /images/XR_META_Face_Tracking_Viseme_RR.jpg "The RR Viseme"
[XR_META_Face_Tracking_Viseme_RR_rot]:    /images/XR_META_Face_Tracking_Viseme_RR_rot.jpg "The RR Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_RR_emp]:    /images/XR_META_Face_Tracking_Viseme_RR_emp.jpg "The RR Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_SIL]:       /images/XR_META_Face_Tracking_Viseme_SIL.jpg "The SIL Viseme"
[XR_META_Face_Tracking_Viseme_SIL_rot]:   /images/XR_META_Face_Tracking_Viseme_SIL_rot.jpg "The SIL Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_SS]:        /images/XR_META_Face_Tracking_Viseme_SS.jpg "The SS Viseme"
[XR_META_Face_Tracking_Viseme_SS_rot]:    /images/XR_META_Face_Tracking_Viseme_SS_rot.jpg "The SS Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_SS_emp]:    /images/XR_META_Face_Tracking_Viseme_SS_emp.jpg "The SS Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_TH]:        /images/XR_META_Face_Tracking_Viseme_TH.jpg "The TH Viseme"
[XR_META_Face_Tracking_Viseme_TH_rot]:    /images/XR_META_Face_Tracking_Viseme_TH_rot.jpg "The TH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_TH_emp]:    /images/XR_META_Face_Tracking_Viseme_TH_emp.jpg "The TH Viseme (emphasized)"

[XR_BODY_JOINT_ROOT_FB]:                    /images/XR_BODY_JOINT_ROOT_FB.png "The XR_BODY_JOINT_ROOT_FB bone joint."
[XR_BODY_JOINT_HIPS_FB]:                    /images/XR_BODY_JOINT_HIPS_FB.png "The XR_BODY_JOINT_HIPS_FB bone joint."
[XR_BODY_JOINT_SPINE_LOWER_FB]:             /images/XR_BODY_JOINT_SPINE_LOWER_FB.png "The XR_BODY_JOINT_SPINE_LOWER_FB bone joint."
[XR_BODY_JOINT_SPINE_MIDDLE_FB]:            /images/XR_BODY_JOINT_SPINE_MIDDLE_FB.png "The XR_BODY_JOINT_SPINE_MIDDLE_FB bone joint."
[XR_BODY_JOINT_SPINE_UPPER_FB]:             /images/XR_BODY_JOINT_SPINE_UPPER_FB.png "The XR_BODY_JOINT_SPINE_UPPER_FB bone joint."
[XR_BODY_JOINT_CHEST_FB]:                   /images/XR_BODY_JOINT_CHEST_FB.png "The XR_BODY_JOINT_CHEST_FB bone joint."
[XR_BODY_JOINT_NECK_FB]:                    /images/XR_BODY_JOINT_NECK_FB.png "The XR_BODY_JOINT_NECK_FB bone joint."
[XR_BODY_JOINT_HEAD_FB]:                    /images/XR_BODY_JOINT_HEAD_FB.png "The XR_BODY_JOINT_HEAD_FB bone joint."
[XR_BODY_JOINT_LEFT_SHOULDER_FB]:           /images/XR_BODY_JOINT_LEFT_SHOULDER_FB.png "The XR_BODY_JOINT_LEFT_SHOULDER_FB bone joint."
[XR_BODY_JOINT_LEFT_SCAPULA_FB]:            /images/XR_BODY_JOINT_LEFT_SCAPULA_FB.png "The XR_BODY_JOINT_LEFT_SCAPULA_FB bone joint."
[XR_BODY_JOINT_LEFT_ARM_UPPER_FB]:          /images/XR_BODY_JOINT_LEFT_ARM_UPPER_FB.png "The XR_BODY_JOINT_LEFT_ARM_UPPER_FB bone joint."
[XR_BODY_JOINT_LEFT_ARM_LOWER_FB]:          /images/XR_BODY_JOINT_LEFT_ARM_LOWER_FB.png "The XR_BODY_JOINT_LEFT_ARM_LOWER_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB bone joint."
[XR_BODY_JOINT_RIGHT_SHOULDER_FB]:          /images/XR_BODY_JOINT_RIGHT_SHOULDER_FB.png "The XR_BODY_JOINT_RIGHT_SHOULDER_FB bone joint."
[XR_BODY_JOINT_RIGHT_SCAPULA_FB]:           /images/XR_BODY_JOINT_RIGHT_SCAPULA_FB.png "The XR_BODY_JOINT_RIGHT_SCAPULA_FB bone joint."
[XR_BODY_JOINT_RIGHT_ARM_UPPER_FB]:         /images/XR_BODY_JOINT_RIGHT_ARM_UPPER_FB.png "The XR_BODY_JOINT_RIGHT_ARM_UPPER_FB bone joint."
[XR_BODY_JOINT_RIGHT_ARM_LOWER_FB]:         /images/XR_BODY_JOINT_RIGHT_ARM_LOWER_FB.png "The XR_BODY_JOINT_RIGHT_ARM_LOWER_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_PALM_FB]:              /images/XR_BODY_JOINT_LEFT_HAND_PALM_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_PALM_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_WRIST_FB]:             /images/XR_BODY_JOINT_LEFT_HAND_WRIST_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_WRIST_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB]:  /images/XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB]:    /images/XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB]:      /images/XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB]:         /images/XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB]:  /images/XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB]:    /images/XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB]:      /images/XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB]:         /images/XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB]: /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB]:        /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB]:       /images/XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB]:          /images/XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB]: /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB]:        /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_PALM_FB]:             /images/XR_BODY_JOINT_RIGHT_HAND_PALM_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_PALM_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_WRIST_FB]:            /images/XR_BODY_JOINT_RIGHT_HAND_WRIST_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_WRIST_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB]:   /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB]:     /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB]:        /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB]:   /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB]:     /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB]:        /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB]:       /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB]:      /images/XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB]:         /images/XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB]:       /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB bone joint."

Your use of the Eye Tracking API must at all times be consistent with the [Oculus SDK License Agreement][oc-sdk-license-agreement] and the [Developer Data Use Policy][dup] and all applicable Oculus and Meta policies, terms and conditions. Applicable privacy and data protection laws may cover your use of Movement, and all applicable privacy and data protection laws.

In particular, you must post and abide by a publicly available and easily accessible privacy policy that clearly explains your collection, use, retention, and processing of data through the Eye Tracking API. You must ensure that a user is provided with clear and comprehensive information about, and consents to, your access to and use of abstracted gaze data prior to collection, including as required by applicable privacy and data protection laws.

Please note that we reserve the right to monitor your use of the Eye Tracking API to enforce compliance with our policies.

{::comment}**DEVICES**{:/comment}

{::comment}**MOVEMENT SDK**{:/comment}

{::comment}**BODY TRACKING**{:/comment}

{::comment}**CHARACTER**{:/comment}

{::comment}**EYE TRACKING**{:/comment}

{::comment}**FACE TRACKING**{:/comment}

{::comment}**DEVICES**{:/comment}

{::comment}**MOVEMENT SDK**{:/comment}

{::comment}**BODY TRACKING**{:/comment}

{::comment}**CHARACTER**{:/comment}

{::comment}**EYE TRACKING**{:/comment}

{::comment}**FACE TRACKING**{:/comment}

[dup]:                      /policy/data-use/ "Developer Data Use Policy"
[dup-prohibited]:           /policy/data-use/#section-4-prohibited-uses-of-user-data "Developer Data Use Policy: Prohibited Practices"

[oc-sdk-license-agreement]: https://developers.meta.com/horizon/licenses/pc-3.3/ "Oculus Software Development Kit License Agreement"

{::comment}**TOPICS**{:/comment}

[move]:                     /documentation/unity/move-overview/ "Movement"
[move-openxr]:              /documentation/native/android/move-overview/ "Movement OpenXR Extensions"

[api]:                      /documentation/unity/move-ref-api/ "API Reference"
[api-openxr]:               /documentation/native/android/move-ref-api/ "API Reference"

[blendshapes]:              /documentation/unity/move-ref-blendshapes/ "Blendshape Visual Reference"
[blendshapes-openxr]:       /documentation/native/android/move-ref-blendshapes/ "Blendshape Visual Reference"

[body-joints]:              /documentation/unity/move-ref-body-joints/ "Body Joint Visual Reference"
[body-joints-openxr]:       /documentation/native/android/move-ref-body-joints/ "Body Joint Visual Reference"

[bt]:                       /documentation/unity/move-body-tracking/ "Body Tracking"
[bt-openxr]:                /documentation/native/android/move-body-tracking/ "Movement Body Tracking OpenXR Extensions"

[et]:                       /documentation/unity/move-eye-tracking/ "Eye Tracking"
[et-openxr]:                /documentation/native/android/move-eye-tracking/ "Movement Eye Tracking OpenXR Extensions"

[ft]:                       /documentation/unity/move-face-tracking/ "Natural Facial Expressions"
[ft-openxr]:                /documentation/native/android/move-face-tracking/ "Movement Face Tracking OpenXR Extensions"

[policies]:                 /documentation/unity/move-policy-data-disclaimer/" "Policies and Disclaimers"
[policies-openxr]:          /documentation/native/android/move-policy-data-disclaimer/" "Policies and Disclaimers"

[retargeting-bones]:        /documentation/unity/move-retargeting-bones/ "Retargeting Bones"
[retargeting-bones-openxr]: /documentation/native/android/move-retargeting-bones/ "Retargeting Bones"

{::comment}**UNITY SCRIPTS AND COMPONENTS**{:/comment}

[OVRBody]:                  /documentation/unity/move-ref-scripts/#ovrbody "The OVRBody component."
[OVRBody.cs]:               https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_body/ "OVRBody API Reference"

[OVRCustomSkeleton]:        /documentation/unity/move-ref-scripts/#ovrcustomskeleton "The OVRCustomSkeleton component."
[OVRCustomSkeleton.cs]:     https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_custom_skeleton/ "OVRCustomSkeleton API Reference"

[OVREyeGaze]:               /documentation/unity/move-ref-scripts/#ovreyegaze "The OVREyeGaze component."
[OVREyeGaze.cs]:            https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_eye_gaze/ "OVREyeGaze API Reference"

[OVRFaceExpressions]:       /documentation/unity/move-ref-scripts/#ovrfaceexpressions "The OVRFaceExpressions component."
[OVRFaceExpressions.cs]:    https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_face_expressions/ "OVRFaceExpressions API Reference"

[OVRSkeleton]:              /documentation/unity/move-ref-scripts/#ovrskeleton "The OVRSkeleton component."
[OVRSkeleton.cs]:           https://developers.meta.com/horizon/reference/unity/latest/class_o_v_r_skeleton/ "OVRSkeleton API Reference"

[Auto-Map-Bones]:               /images/movement-ovr-custom-skeleton-automap-button.png "Auto-Mapping Bones in a Custom Skeleton"

[Common-Custom-Skeleton-Setup]: /images/movement-ovr-common-setup-custom-skeleton-component.png "Common Setup For A Custom Skeleton"

[Movement-Enable-Tracking]:     /images/movement-enable-tracking.png "Settings for Enabling Tracking Features"

[Movement-OVR-Eye-Gaze]:        /images/movement-ovr-eye-gaze-component.png "OVR Eye Gaze Component"

[Movement-SDK-Splash]:          /images/movement-splash.jpg "Movement SDK"
{: height="718px" width="1000px" }

[Skeleton-Bones]:               /images/movement-skeleton-bones.png "Skeleton Bones"
[Movement-Samples-Fitness]:          /images/move-sample-fitness.png "Move Samples Fitness"
[Movement-Samples-Locomotion]:          /images/move-sample-locomotion.png "Move Samples Locomotion"
[Movement-Samples-ISDK-Integration]:          /images/move-sample-isdk-integration.png "Move Samples ISDK Integration"

[XR_Face_Expression_Neutral]:                /images/XR_Face_Expression_Neutral.png "The Neutral OpenXR Face Expression Blendshape"
[XR_Face_Expression_Brow_Lowerer_L]:         /images/XR_Face_Expression_Brow_Lowerer_L.png "The XR_Face_Expression_Brow_Lowerer_L Blendshape"
[XR_Face_Expression_Brow_Lowerer_R]:         /images/XR_Face_Expression_Brow_Lowerer_R.png "The XR_Face_Expression_Brow_Lowerer_R Blendshape"
[XR_Face_Expression_Cheek_Puff_L]:           /images/XR_Face_Expression_Cheek_Puff_L.png "The XR_Face_Expression_Cheek_Puff_L Blendshape"
[XR_Face_Expression_Cheek_Puff_R]:           /images/XR_Face_Expression_Cheek_Puff_R.png "The XR_Face_Expression_Cheek_Puff_R Blendshape"
[XR_Face_Expression_Cheek_Raiser_L]:         /images/XR_Face_Expression_Cheek_Raiser_L.png "The XR_Face_Expression_Cheek_Raiser_L Blendshape"
[XR_Face_Expression_Cheek_Raiser_R]:         /images/XR_Face_Expression_Cheek_Raiser_R.png "The XR_Face_Expression_Cheek_Raiser_R Blendshape"
[XR_Face_Expression_Cheek_Suck_L]:           /images/XR_Face_Expression_Cheek_Suck_L.png "The XR_Face_Expression_Cheek_Suck_L Blendshape"
[XR_Face_Expression_Cheek_Suck_R]:           /images/XR_Face_Expression_Cheek_Suck_R.png "The XR_Face_Expression_Cheek_Suck_R Blendshape"
[XR_Face_Expression_Chin_Raiser_B]:          /images/XR_Face_Expression_Chin_Raiser_B.png "The XR_Face_Expression_Chin_Raiser_B Blendshape"
[XR_Face_Expression_Chin_Raiser_T]:          /images/XR_Face_Expression_Chin_Raiser_T.png "The XR_Face_Expression_Chin_Raiser_T Blendshape"
[XR_Face_Expression_Dimpler_L]:              /images/XR_Face_Expression_Dimpler_L.png "The XR_Face_Expression_Dimpler_L Blendshape"
[XR_Face_Expression_Dimpler_R]:              /images/XR_Face_Expression_Dimpler_R.png "The XR_Face_Expression_Dimpler_R Blendshape"
[XR_Face_Expression_Eyes_Closed_L]:          /images/XR_Face_Expression_Eyes_Closed_L.png "The XR_Face_Expression_Eyes_Closed_L Blendshape"
[XR_Face_Expression_Eyes_Closed_R]:          /images/XR_Face_Expression_Eyes_Closed_R.png "The XR_Face_Expression_Eyes_Closed_R Blendshape"
[XR_Face_Expression_Eyes_Look_Down_L]:       /images/XR_Face_Expression_Eyes_Look_Down_L.png "The XR_Face_Expression_Eyes_Look_Down_L Blendshape"
[XR_Face_Expression_Eyes_Look_Down_R]:       /images/XR_Face_Expression_Eyes_Look_Down_R.png "The XR_Face_Expression_Eyes_Look_Down_R Blendshape"
[XR_Face_Expression_Eyes_Look_Left_L]:       /images/XR_Face_Expression_Eyes_Look_Left_L.png "The XR_Face_Expression_Eyes_Look_Left_L Blendshape"
[XR_Face_Expression_Eyes_Look_Left_R]:       /images/XR_Face_Expression_Eyes_Look_Left_R.png "The XR_Face_Expression_Eyes_Look_Left_R Blendshape"
[XR_Face_Expression_Eyes_Look_Right_L]:      /images/XR_Face_Expression_Eyes_Look_Right_L.png "The XR_Face_Expression_Eyes_Look_Right_L Blendshape"
[XR_Face_Expression_Eyes_Look_Right_R]:      /images/XR_Face_Expression_Eyes_Look_Right_R.png "The XR_Face_Expression_Eyes_Look_Right_R Blendshape"
[XR_Face_Expression_Eyes_Look_Up_L]:         /images/XR_Face_Expression_Eyes_Look_Up_L.png "The XR_Face_Expression_Eyes_Look_Up_L Blendshape"
[XR_Face_Expression_Eyes_Look_Up_R]:         /images/XR_Face_Expression_Eyes_Look_Up_R.png "The XR_Face_Expression_Eyes_Look_Up_R Blendshape"
[XR_Face_Expression_Inner_Brow_Raiser_L]:    /images/XR_Face_Expression_Inner_Brow_Raiser_L.png "The XR_Face_Expression_Inner_Brow_Raiser_L Blendshape"
[XR_Face_Expression_Inner_Brow_Raiser_R]:    /images/XR_Face_Expression_Inner_Brow_Raiser_R.png "The XR_Face_Expression_Inner_Brow_Raiser_R Blendshape"
[XR_Face_Expression_Jaw_Drop]:               /images/XR_Face_Expression_Jaw_Drop.png "The XR_Face_Expression_Jaw_Drop Blendshape"
[XR_Face_Expression_Jaw_Sideways_Left]:      /images/XR_Face_Expression_Jaw_Sideways_Left.png "The XR_Face_Expression_Jaw_Sideways_Left Blendshape"
[XR_Face_Expression_Jaw_Sideways_Right]:     /images/XR_Face_Expression_Jaw_Sideways_Right.png "The XR_Face_Expression_Jaw_Sideways_Right Blendshape"
[XR_Face_Expression_Jaw_Thrust]:             /images/XR_Face_Expression_Jaw_Thrust.png "The XR_Face_Expression_Jaw_Thrust Blendshape"
[XR_Face_Expression_Lid_Tightener_L]:        /images/XR_Face_Expression_Lid_Tightener_L.png "The XR_Face_Expression_Lid_Tightener_L Blendshape"
[XR_Face_Expression_Lid_Tightener_R]:        /images/XR_Face_Expression_Lid_Tightener_R.png "The XR_Face_Expression_Lid_Tightener_R Blendshape"
[XR_Face_Expression_Lip_Corner_Depressor_L]: /images/XR_Face_Expression_Lip_Corner_Depressor_L.png "The XR_Face_Expression_Lip_Corner_Depressor_L Blendshape"
[XR_Face_Expression_Lip_Corner_Depressor_R]: /images/XR_Face_Expression_Lip_Corner_Depressor_R.png "The XR_Face_Expression_Lip_Corner_Depressor_R Blendshape"
[XR_Face_Expression_Lip_Corner_Puller_L]:    /images/XR_Face_Expression_Lip_Corner_Puller_L.png "The XR_Face_Expression_Lip_Corner_Puller_L Blendshape"
[XR_Face_Expression_Lip_Corner_Puller_R]:    /images/XR_Face_Expression_Lip_Corner_Puller_R.png "The XR_Face_Expression_Lip_Corner_Puller_R Blendshape"
[XR_Face_Expression_Lip_Funneler_LB]:        /images/XR_Face_Expression_Lip_Funneler_LB.png "The XR_Face_Expression_Lip_Funneler_LB Blendshape"
[XR_Face_Expression_Lip_Funneler_LT]:        /images/XR_Face_Expression_Lip_Funneler_LT.png "The XR_Face_Expression_Lip_Funneler_LT Blendshape"
[XR_Face_Expression_Lip_Funneler_RB]:        /images/XR_Face_Expression_Lip_Funneler_RB.png "The XR_Face_Expression_Lip_Funneler_RB Blendshape"
[XR_Face_Expression_Lip_Funneler_RT]:        /images/XR_Face_Expression_Lip_Funneler_RT.png "The XR_Face_Expression_Lip_Funneler_RT Blendshape"
[XR_Face_Expression_Lip_Pressor_L]:          /images/XR_Face_Expression_Lip_Pressor_L.png "The XR_Face_Expression_Lip_Pressor_L Blendshape"
[XR_Face_Expression_Lip_Pressor_R]:          /images/XR_Face_Expression_Lip_Pressor_R.png "The XR_Face_Expression_Lip_Pressor_R Blendshape"
[XR_Face_Expression_Lip_Pucker_L]:           /images/XR_Face_Expression_Lip_Pucker_L.png "The XR_Face_Expression_Lip_Pucker_L Blendshape"
[XR_Face_Expression_Lip_Pucker_R]:           /images/XR_Face_Expression_Lip_Pucker_R.png "The XR_Face_Expression_Lip_Pucker_R Blendshape"
[XR_Face_Expression_Lip_Stretcher_L]:        /images/XR_Face_Expression_Lip_Stretcher_L.png "The XR_Face_Expression_Lip_Stretcher_L Blendshape"
[XR_Face_Expression_Lip_Stretcher_R]:        /images/XR_Face_Expression_Lip_Stretcher_R.png "The XR_Face_Expression_Lip_Stretcher_R Blendshape"
[XR_Face_Expression_Lip_Suck_LB]:            /images/XR_Face_Expression_Lip_Suck_LB.png "The XR_Face_Expression_Lip_Suck_LB Blendshape"
[XR_Face_Expression_Lip_Suck_LT]:            /images/XR_Face_Expression_Lip_Suck_LT.png "The XR_Face_Expression_Lip_Suck_LT Blendshape"
[XR_Face_Expression_Lip_Suck_RB]:            /images/XR_Face_Expression_Lip_Suck_RB.png "The XR_Face_Expression_Lip_Suck_RB Blendshape"
[XR_Face_Expression_Lip_Suck_RT]:            /images/XR_Face_Expression_Lip_Suck_RT.png "The XR_Face_Expression_Lip_Suck_RT Blendshape"
[XR_Face_Expression_Lip_Tightener_L]:        /images/XR_Face_Expression_Lip_Tightener_L.png "The XR_Face_Expression_Lip_Tightener_L Blendshape"
[XR_Face_Expression_Lip_Tightener_R]:        /images/XR_Face_Expression_Lip_Tightener_R.png "The XR_Face_Expression_Lip_Tightener_R Blendshape"
[XR_Face_Expression_Lips_Toward]:            /images/XR_Face_Expression_Lips_Toward.png "The XR_Face_Expression_Lips_Toward Blendshape"
[XR_Face_Expression_Lower_Lip_Depressor_L]:  /images/XR_Face_Expression_Lower_Lip_Depressor_L.png "The XR_Face_Expression_Lower_Lip_Depressor_L Blendshape"
[XR_Face_Expression_Lower_Lip_Depressor_R]:  /images/XR_Face_Expression_Lower_Lip_Depressor_R.png "The XR_Face_Expression_Lower_Lip_Depressor_R Blendshape"
[XR_Face_Expression_Mouth_Left]:             /images/XR_Face_Expression_Mouth_Left.png "The XR_Face_Expression_Mouth_Left Blendshape"
[XR_Face_Expression_Mouth_Right]:            /images/XR_Face_Expression_Mouth_Right.png "The XR_Face_Expression_Mouth_Right Blendshape"
[XR_Face_Expression_Nose_Wrinkler_L]:        /images/XR_Face_Expression_Nose_Wrinkler_L.png "The XR_Face_Expression_Nose_Wrinkler_L Blendshape"
[XR_Face_Expression_Nose_Wrinkler_R]:        /images/XR_Face_Expression_Nose_Wrinkler_R.png "The XR_Face_Expression_Nose_Wrinkler_R Blendshape"
[XR_Face_Expression_Outer_Brow_Raiser_L]:    /images/XR_Face_Expression_Outer_Brow_Raiser_L.png "The XR_Face_Expression_Outer_Brow_Raiser_L Blendshape"
[XR_Face_Expression_Outer_Brow_Raiser_R]:    /images/XR_Face_Expression_Outer_Brow_Raiser_R.png "The XR_Face_Expression_Outer_Brow_Raiser_R Blendshape"
[XR_Face_Expression_Upper_Lid_Raiser_L]:     /images/XR_Face_Expression_Upper_Lid_Raiser_L.png "The XR_Face_Expression_Upper_Lid_Raiser_L Blendshape"
[XR_Face_Expression_Upper_Lid_Raiser_R]:     /images/XR_Face_Expression_Upper_Lid_Raiser_R.png "The XR_Face_Expression_Upper_Lid_Raiser_R Blendshape"
[XR_Face_Expression_Upper_Lip_Raiser_L]:     /images/XR_Face_Expression_Upper_Lip_Raiser_L.png "The XR_Face_Expression_Upper_Lip_Raiser_L Blendshape"
[XR_Face_Expression_Upper_Lip_Raiser_R]:     /images/XR_Face_Expression_Upper_Lip_Raiser_R.png "The XR_Face_Expression_Upper_Lip_Raiser_R Blendshape"

[XR_Face_Expression_2_Brow_Lowerer_L]:         /images/XR_Face_Expression_2_Brow_Lowerer_L.png "The XR_Face_Expression_Brow_Lowerer_L Blendshape"
[XR_Face_Expression_2_Brow_Lowerer_R]:         /images/XR_Face_Expression_2_Brow_Lowerer_R.png "The XR_Face_Expression_Brow_Lowerer_R Blendshape"
[XR_Face_Expression_2_Cheek_Puff_L]:           /images/XR_Face_Expression_2_Cheek_Puff_L.png "The XR_Face_Expression_Cheek_Puff_L Blendshape"
[XR_Face_Expression_2_Cheek_Puff_R]:           /images/XR_Face_Expression_2_Cheek_Puff_R.png "The XR_Face_Expression_Cheek_Puff_R Blendshape"
[XR_Face_Expression_2_Cheek_Raiser_L]:         /images/XR_Face_Expression_2_Cheek_Raiser_L.png "The XR_Face_Expression_Cheek_Raiser_L Blendshape"
[XR_Face_Expression_2_Cheek_Raiser_R]:         /images/XR_Face_Expression_2_Cheek_Raiser_R.png "The XR_Face_Expression_Cheek_Raiser_R Blendshape"
[XR_Face_Expression_2_Cheek_Suck_L]:           /images/XR_Face_Expression_2_Cheek_Suck_L.png "The XR_Face_Expression_Cheek_Suck_L Blendshape"
[XR_Face_Expression_2_Cheek_Suck_R]:           /images/XR_Face_Expression_2_Cheek_Suck_R.png "The XR_Face_Expression_Cheek_Suck_R Blendshape"
[XR_Face_Expression_2_Chin_Raiser_B]:          /images/XR_Face_Expression_2_Chin_Raiser_B.png "The XR_Face_Expression_Chin_Raiser_B Blendshape"
[XR_Face_Expression_2_Chin_Raiser_T]:          /images/XR_Face_Expression_2_Chin_Raiser_T.png "The XR_Face_Expression_Chin_Raiser_T Blendshape"
[XR_Face_Expression_2_Dimpler_L]:              /images/XR_Face_Expression_2_Dimpler_L.png "The XR_Face_Expression_Dimpler_L Blendshape"
[XR_Face_Expression_2_Dimpler_R]:              /images/XR_Face_Expression_2_Dimpler_R.png "The XR_Face_Expression_Dimpler_R Blendshape"
[XR_Face_Expression_2_Eyes_Closed_L]:          /images/XR_Face_Expression_2_Eyes_Closed_L.png "The XR_Face_Expression_Eyes_Closed_L Blendshape"
[XR_Face_Expression_2_Eyes_Closed_R]:          /images/XR_Face_Expression_2_Eyes_Closed_R.png "The XR_Face_Expression_Eyes_Closed_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Down_L]:       /images/XR_Face_Expression_2_Eyes_Look_Down_L.png "The XR_Face_Expression_Eyes_Look_Down_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Down_R]:       /images/XR_Face_Expression_2_Eyes_Look_Down_R.png "The XR_Face_Expression_Eyes_Look_Down_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Left_L]:       /images/XR_Face_Expression_2_Eyes_Look_Left_L.png "The XR_Face_Expression_Eyes_Look_Left_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Left_R]:       /images/XR_Face_Expression_2_Eyes_Look_Left_R.png "The XR_Face_Expression_Eyes_Look_Left_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Right_L]:      /images/XR_Face_Expression_2_Eyes_Look_Right_L.png "The XR_Face_Expression_Eyes_Look_Right_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Right_R]:      /images/XR_Face_Expression_2_Eyes_Look_Right_R.png "The XR_Face_Expression_Eyes_Look_Right_R Blendshape"
[XR_Face_Expression_2_Eyes_Look_Up_L]:         /images/XR_Face_Expression_2_Eyes_Look_Up_L.png "The XR_Face_Expression_Eyes_Look_Up_L Blendshape"
[XR_Face_Expression_2_Eyes_Look_Up_R]:         /images/XR_Face_Expression_2_Eyes_Look_Up_R.png "The XR_Face_Expression_Eyes_Look_Up_R Blendshape"
[XR_Face_Expression_2_Inner_Brow_Raiser_L]:    /images/XR_Face_Expression_2_Inner_Brow_Raiser_L.png "The XR_Face_Expression_Inner_Brow_Raiser_L Blendshape"
[XR_Face_Expression_2_Inner_Brow_Raiser_R]:    /images/XR_Face_Expression_2_Inner_Brow_Raiser_R.png "The XR_Face_Expression_Inner_Brow_Raiser_R Blendshape"
[XR_Face_Expression_2_Jaw_Drop]:               /images/XR_Face_Expression_2_Jaw_Drop.png "The XR_Face_Expression_Jaw_Drop Blendshape"
[XR_Face_Expression_2_Jaw_Sideways_Left]:      /images/XR_Face_Expression_2_Jaw_Sideways_Left.png "The XR_Face_Expression_Jaw_Sideways_Left Blendshape"
[XR_Face_Expression_2_Jaw_Sideways_Right]:     /images/XR_Face_Expression_2_Jaw_Sideways_Right.png "The XR_Face_Expression_Jaw_Sideways_Right Blendshape"
[XR_Face_Expression_2_Jaw_Thrust]:             /images/XR_Face_Expression_2_Jaw_Thrust.png "The XR_Face_Expression_Jaw_Thrust Blendshape"
[XR_Face_Expression_2_Lid_Tightener_L]:        /images/XR_Face_Expression_2_Lid_Tightener_L.png "The XR_Face_Expression_Lid_Tightener_L Blendshape"
[XR_Face_Expression_2_Lid_Tightener_R]:        /images/XR_Face_Expression_2_Lid_Tightener_R.png "The XR_Face_Expression_Lid_Tightener_R Blendshape"
[XR_Face_Expression_2_Lip_Corner_Depressor_L]: /images/XR_Face_Expression_2_Lip_Corner_Depressor_L.png "The XR_Face_Expression_Lip_Corner_Depressor_L Blendshape"
[XR_Face_Expression_2_Lip_Corner_Depressor_R]: /images/XR_Face_Expression_2_Lip_Corner_Depressor_R.png "The XR_Face_Expression_Lip_Corner_Depressor_R Blendshape"
[XR_Face_Expression_2_Lip_Corner_Puller_L]:    /images/XR_Face_Expression_2_Lip_Corner_Puller_L.png "The XR_Face_Expression_Lip_Corner_Puller_L Blendshape"
[XR_Face_Expression_2_Lip_Corner_Puller_R]:    /images/XR_Face_Expression_2_Lip_Corner_Puller_R.png "The XR_Face_Expression_Lip_Corner_Puller_R Blendshape"
[XR_Face_Expression_2_Lip_Funneler_LB]:        /images/XR_Face_Expression_2_Lip_Funneler_LB.png "The XR_Face_Expression_Lip_Funneler_LB Blendshape"
[XR_Face_Expression_2_Lip_Funneler_LT]:        /images/XR_Face_Expression_2_Lip_Funneler_LT.png "The XR_Face_Expression_Lip_Funneler_LT Blendshape"
[XR_Face_Expression_2_Lip_Funneler_RB]:        /images/XR_Face_Expression_2_Lip_Funneler_RB.png "The XR_Face_Expression_Lip_Funneler_RB Blendshape"
[XR_Face_Expression_2_Lip_Funneler_RT]:        /images/XR_Face_Expression_2_Lip_Funneler_RT.png "The XR_Face_Expression_Lip_Funneler_RT Blendshape"
[XR_Face_Expression_2_Lip_Pressor_L]:          /images/XR_Face_Expression_2_Lip_Pressor_L.png "The XR_Face_Expression_Lip_Pressor_L Blendshape"
[XR_Face_Expression_2_Lip_Pressor_R]:          /images/XR_Face_Expression_2_Lip_Pressor_R.png "The XR_Face_Expression_Lip_Pressor_R Blendshape"
[XR_Face_Expression_2_Lip_Pucker_L]:           /images/XR_Face_Expression_2_Lip_Pucker_L.png "The XR_Face_Expression_Lip_Pucker_L Blendshape"
[XR_Face_Expression_2_Lip_Pucker_R]:           /images/XR_Face_Expression_2_Lip_Pucker_R.png "The XR_Face_Expression_Lip_Pucker_R Blendshape"
[XR_Face_Expression_2_Lip_Stretcher_L]:        /images/XR_Face_Expression_2_Lip_Stretcher_L.png "The XR_Face_Expression_Lip_Stretcher_L Blendshape"
[XR_Face_Expression_2_Lip_Stretcher_R]:        /images/XR_Face_Expression_2_Lip_Stretcher_R.png "The XR_Face_Expression_Lip_Stretcher_R Blendshape"
[XR_Face_Expression_2_Lip_Suck_LB]:            /images/XR_Face_Expression_2_Lip_Suck_LB.png "The XR_Face_Expression_Lip_Suck_LB Blendshape"
[XR_Face_Expression_2_Lip_Suck_LT]:            /images/XR_Face_Expression_2_Lip_Suck_LT.png "The XR_Face_Expression_Lip_Suck_LT Blendshape"
[XR_Face_Expression_2_Lip_Suck_RB]:            /images/XR_Face_Expression_2_Lip_Suck_RB.png "The XR_Face_Expression_Lip_Suck_RB Blendshape"
[XR_Face_Expression_2_Lip_Suck_RT]:            /images/XR_Face_Expression_2_Lip_Suck_RT.png "The XR_Face_Expression_Lip_Suck_RT Blendshape"
[XR_Face_Expression_2_Lip_Tightener_L]:        /images/XR_Face_Expression_2_Lip_Tightener_L.png "The XR_Face_Expression_Lip_Tightener_L Blendshape"
[XR_Face_Expression_2_Lip_Tightener_R]:        /images/XR_Face_Expression_2_Lip_Tightener_R.png "The XR_Face_Expression_Lip_Tightener_R Blendshape"
[XR_Face_Expression_2_Lips_Toward]:            /images/XR_Face_Expression_2_Lips_Toward.png "The XR_Face_Expression_Lips_Toward Blendshape"
[XR_Face_Expression_2_Lower_Lip_Depressor_L]:  /images/XR_Face_Expression_2_Lower_Lip_Depressor_L.png "The XR_Face_Expression_Lower_Lip_Depressor_L Blendshape"
[XR_Face_Expression_2_Lower_Lip_Depressor_R]:  /images/XR_Face_Expression_2_Lower_Lip_Depressor_R.png "The XR_Face_Expression_Lower_Lip_Depressor_R Blendshape"
[XR_Face_Expression_2_Mouth_Left]:             /images/XR_Face_Expression_2_Mouth_Left.png "The XR_Face_Expression_Mouth_Left Blendshape"
[XR_Face_Expression_2_Mouth_Right]:            /images/XR_Face_Expression_2_Mouth_Right.png "The XR_Face_Expression_Mouth_Right Blendshape"
[XR_Face_Expression_2_Nose_Wrinkler_L]:        /images/XR_Face_Expression_2_Nose_Wrinkler_L.png "The XR_Face_Expression_Nose_Wrinkler_L Blendshape"
[XR_Face_Expression_2_Nose_Wrinkler_R]:        /images/XR_Face_Expression_2_Nose_Wrinkler_R.png "The XR_Face_Expression_Nose_Wrinkler_R Blendshape"
[XR_Face_Expression_2_Outer_Brow_Raiser_L]:    /images/XR_Face_Expression_2_Outer_Brow_Raiser_L.png "The XR_Face_Expression_Outer_Brow_Raiser_L Blendshape"
[XR_Face_Expression_2_Outer_Brow_Raiser_R]:    /images/XR_Face_Expression_2_Outer_Brow_Raiser_R.png "The XR_Face_Expression_Outer_Brow_Raiser_R Blendshape"
[XR_Face_Expression_2_Upper_Lid_Raiser_L]:     /images/XR_Face_Expression_2_Upper_Lid_Raiser_L.png "The XR_Face_Expression_Upper_Lid_Raiser_L Blendshape"
[XR_Face_Expression_2_Upper_Lid_Raiser_R]:     /images/XR_Face_Expression_2_Upper_Lid_Raiser_R.png "The XR_Face_Expression_Upper_Lid_Raiser_R Blendshape"
[XR_Face_Expression_2_Upper_Lip_Raiser_L]:     /images/XR_Face_Expression_2_Upper_Lip_Raiser_L.png "The XR_Face_Expression_Upper_Lip_Raiser_L Blendshape"
[XR_Face_Expression_2_Upper_Lip_Raiser_R]:     /images/XR_Face_Expression_2_Upper_Lip_Raiser_R.png "The XR_Face_Expression_Upper_Lip_Raiser_R Blendshape"
[XR_Face_Expression_2_Tongue_Tip_Interdental]:     /images/XR_Face_Expression_2_Tongue_Tip_Interdental.png "The XR_Face_Expression_Tongue_Tip_Interdental Blendshape"
[XR_Face_Expression_2_Tongue_Tip_Alveolar]:        /images/XR_Face_Expression_2_Tongue_Tip_Alveolar.png "The XR_Face_Expression_Tongue_Tip_Alveolar Blendshape"
[XR_Face_Expression_2_Tongue_Front_Dorsal_Palate]: /images/XR_Face_Expression_2_Tongue_Front_Dorsal_Palate.png "The XR_Face_Expression_Tongue_Front_Dorsal_Palate Blendshape"
[XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate]:   /images/XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate.png "The XR_Face_Expression_Tongue_Mid_Dorsal_Palate Blendshape"
[XR_Face_Expression_2_Tongue_Back_Dorsal_Velar]:   /images/XR_Face_Expression_2_Tongue_Back_Dorsal_Velar.png "The XR_Face_Expression_Tongue_Back_Dorsal_Velar Blendshape"
[XR_Face_Expression_2_Tongue_Out]:                 /images/XR_Face_Expression_2_Tongue_Out.png "The XR_Face_Expression_Tongue_Out Blendshape"
[XR_Face_Expression_2_Tongue_Retreat]:             /images/XR_Face_Expression_2_Tongue_Retreat.png "The XR_Face_Expression_Tongue_Retreat Blendshape"

[XR_META_Face_Tracking_Viseme_AA]:        /images/XR_META_Face_Tracking_Viseme_AA.jpg "The AA Viseme"
[XR_META_Face_Tracking_Viseme_AA_rot]:    /images/XR_META_Face_Tracking_Viseme_AA_rot.jpg "The AA Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_AA_emp]:    /images/XR_META_Face_Tracking_Viseme_AA_emp.jpg "The AA Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_CH]:        /images/XR_META_Face_Tracking_Viseme_CH.jpg "The CH Viseme"
[XR_META_Face_Tracking_Viseme_CH_rot]:    /images/XR_META_Face_Tracking_Viseme_CH_rot.jpg "The CH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_CH_emp]:    /images/XR_META_Face_Tracking_Viseme_CH_emp.jpg "The CH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_DD]:        /images/XR_META_Face_Tracking_Viseme_DD.jpg "The DD Viseme"
[XR_META_Face_Tracking_Viseme_DD_rot]:    /images/XR_META_Face_Tracking_Viseme_DD_rot.jpg "The DD Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_DD_emp]:    /images/XR_META_Face_Tracking_Viseme_DD_emp.jpg "The DD Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_E]:         /images/XR_META_Face_Tracking_Viseme_E.jpg "The E Viseme"
[XR_META_Face_Tracking_Viseme_E_rot]:     /images/XR_META_Face_Tracking_Viseme_E_rot.jpg "The E Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_E_emp]:     /images/XR_META_Face_Tracking_Viseme_E_emp.jpg "The E Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_FF]:        /images/XR_META_Face_Tracking_Viseme_FF.jpg "The FF Viseme"
[XR_META_Face_Tracking_Viseme_FF_rot]:    /images/XR_META_Face_Tracking_Viseme_FF_rot.jpg "The FF Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_FF_emp]:    /images/XR_META_Face_Tracking_Viseme_FF_emp.jpg "The FF Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_IH]:        /images/XR_META_Face_Tracking_Viseme_IH.jpg "The IH Viseme"
[XR_META_Face_Tracking_Viseme_IH_rot]:    /images/XR_META_Face_Tracking_Viseme_IH_rot.jpg "The IH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_IH_emp]:    /images/XR_META_Face_Tracking_Viseme_IH_emp.jpg "The IH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_KK]:        /images/XR_META_Face_Tracking_Viseme_KK.jpg "The KK Viseme"
[XR_META_Face_Tracking_Viseme_KK_rot]:    /images/XR_META_Face_Tracking_Viseme_KK_rot.jpg "The KK Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_KK_emp]:    /images/XR_META_Face_Tracking_Viseme_KK_emp.jpg "The KK Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_NN]:        /images/XR_META_Face_Tracking_Viseme_NN.jpg "The NN Viseme"
[XR_META_Face_Tracking_Viseme_NN_rot]:    /images/XR_META_Face_Tracking_Viseme_NN_rot.jpg "The NN Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_NN_emp]:    /images/XR_META_Face_Tracking_Viseme_NN_emp.jpg "The NN Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_OH]:        /images/XR_META_Face_Tracking_Viseme_OH.jpg "The OH Viseme"
[XR_META_Face_Tracking_Viseme_OH_rot]:    /images/XR_META_Face_Tracking_Viseme_OH_rot.jpg "The OH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_OH_emp]:    /images/XR_META_Face_Tracking_Viseme_OH_emp.jpg "The OH Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_OU]:        /images/XR_META_Face_Tracking_Viseme_OU.jpg "The OU Viseme"
[XR_META_Face_Tracking_Viseme_OU_rot]:    /images/XR_META_Face_Tracking_Viseme_OU_rot.jpg "The OU Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_OU_emp]:    /images/XR_META_Face_Tracking_Viseme_OU_emp.jpg "The OU Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_PP]:        /images/XR_META_Face_Tracking_Viseme_PP.jpg "The PP Viseme"
[XR_META_Face_Tracking_Viseme_PP_rot]:    /images/XR_META_Face_Tracking_Viseme_PP_rot.jpg "The PP Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_PP_emp]:    /images/XR_META_Face_Tracking_Viseme_PP_emp.jpg "The PP Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_RR]:        /images/XR_META_Face_Tracking_Viseme_RR.jpg "The RR Viseme"
[XR_META_Face_Tracking_Viseme_RR_rot]:    /images/XR_META_Face_Tracking_Viseme_RR_rot.jpg "The RR Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_RR_emp]:    /images/XR_META_Face_Tracking_Viseme_RR_emp.jpg "The RR Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_SIL]:       /images/XR_META_Face_Tracking_Viseme_SIL.jpg "The SIL Viseme"
[XR_META_Face_Tracking_Viseme_SIL_rot]:   /images/XR_META_Face_Tracking_Viseme_SIL_rot.jpg "The SIL Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_SS]:        /images/XR_META_Face_Tracking_Viseme_SS.jpg "The SS Viseme"
[XR_META_Face_Tracking_Viseme_SS_rot]:    /images/XR_META_Face_Tracking_Viseme_SS_rot.jpg "The SS Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_SS_emp]:    /images/XR_META_Face_Tracking_Viseme_SS_emp.jpg "The SS Viseme (emphasized)"
[XR_META_Face_Tracking_Viseme_TH]:        /images/XR_META_Face_Tracking_Viseme_TH.jpg "The TH Viseme"
[XR_META_Face_Tracking_Viseme_TH_rot]:    /images/XR_META_Face_Tracking_Viseme_TH_rot.jpg "The TH Viseme (rotated)"
[XR_META_Face_Tracking_Viseme_TH_emp]:    /images/XR_META_Face_Tracking_Viseme_TH_emp.jpg "The TH Viseme (emphasized)"

[XR_BODY_JOINT_ROOT_FB]:                    /images/XR_BODY_JOINT_ROOT_FB.png "The XR_BODY_JOINT_ROOT_FB bone joint."
[XR_BODY_JOINT_HIPS_FB]:                    /images/XR_BODY_JOINT_HIPS_FB.png "The XR_BODY_JOINT_HIPS_FB bone joint."
[XR_BODY_JOINT_SPINE_LOWER_FB]:             /images/XR_BODY_JOINT_SPINE_LOWER_FB.png "The XR_BODY_JOINT_SPINE_LOWER_FB bone joint."
[XR_BODY_JOINT_SPINE_MIDDLE_FB]:            /images/XR_BODY_JOINT_SPINE_MIDDLE_FB.png "The XR_BODY_JOINT_SPINE_MIDDLE_FB bone joint."
[XR_BODY_JOINT_SPINE_UPPER_FB]:             /images/XR_BODY_JOINT_SPINE_UPPER_FB.png "The XR_BODY_JOINT_SPINE_UPPER_FB bone joint."
[XR_BODY_JOINT_CHEST_FB]:                   /images/XR_BODY_JOINT_CHEST_FB.png "The XR_BODY_JOINT_CHEST_FB bone joint."
[XR_BODY_JOINT_NECK_FB]:                    /images/XR_BODY_JOINT_NECK_FB.png "The XR_BODY_JOINT_NECK_FB bone joint."
[XR_BODY_JOINT_HEAD_FB]:                    /images/XR_BODY_JOINT_HEAD_FB.png "The XR_BODY_JOINT_HEAD_FB bone joint."
[XR_BODY_JOINT_LEFT_SHOULDER_FB]:           /images/XR_BODY_JOINT_LEFT_SHOULDER_FB.png "The XR_BODY_JOINT_LEFT_SHOULDER_FB bone joint."
[XR_BODY_JOINT_LEFT_SCAPULA_FB]:            /images/XR_BODY_JOINT_LEFT_SCAPULA_FB.png "The XR_BODY_JOINT_LEFT_SCAPULA_FB bone joint."
[XR_BODY_JOINT_LEFT_ARM_UPPER_FB]:          /images/XR_BODY_JOINT_LEFT_ARM_UPPER_FB.png "The XR_BODY_JOINT_LEFT_ARM_UPPER_FB bone joint."
[XR_BODY_JOINT_LEFT_ARM_LOWER_FB]:          /images/XR_BODY_JOINT_LEFT_ARM_LOWER_FB.png "The XR_BODY_JOINT_LEFT_ARM_LOWER_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_WRIST_TWIST_FB bone joint."
[XR_BODY_JOINT_RIGHT_SHOULDER_FB]:          /images/XR_BODY_JOINT_RIGHT_SHOULDER_FB.png "The XR_BODY_JOINT_RIGHT_SHOULDER_FB bone joint."
[XR_BODY_JOINT_RIGHT_SCAPULA_FB]:           /images/XR_BODY_JOINT_RIGHT_SCAPULA_FB.png "The XR_BODY_JOINT_RIGHT_SCAPULA_FB bone joint."
[XR_BODY_JOINT_RIGHT_ARM_UPPER_FB]:         /images/XR_BODY_JOINT_RIGHT_ARM_UPPER_FB.png "The XR_BODY_JOINT_RIGHT_ARM_UPPER_FB bone joint."
[XR_BODY_JOINT_RIGHT_ARM_LOWER_FB]:         /images/XR_BODY_JOINT_RIGHT_ARM_LOWER_FB.png "The XR_BODY_JOINT_RIGHT_ARM_LOWER_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_PALM_FB]:              /images/XR_BODY_JOINT_LEFT_HAND_PALM_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_PALM_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_WRIST_FB]:             /images/XR_BODY_JOINT_LEFT_HAND_WRIST_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_WRIST_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB]:  /images/XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB]:    /images/XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB]:      /images/XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB]:         /images/XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_THUMB_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB]:  /images/XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB]:    /images/XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB]:      /images/XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB]:         /images/XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_INDEX_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB]: /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB]:        /images/XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB]:       /images/XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB]:          /images/XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_RING_TIP_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB]: /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB]:   /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB]:     /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB]:        /images/XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB.jpg "The XR_BODY_JOINT_LEFT_HAND_LITTLE_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_PALM_FB]:             /images/XR_BODY_JOINT_RIGHT_HAND_PALM_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_PALM_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_WRIST_FB]:            /images/XR_BODY_JOINT_RIGHT_HAND_WRIST_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_WRIST_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB]:   /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB]:     /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB]:        /images/XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_THUMB_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB]:   /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB]:     /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB]:        /images/XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_INDEX_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB]:       /images/XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB]:      /images/XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB]:         /images/XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_RING_TIP_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB]:  /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB]: /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB]:    /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_FB bone joint."
[XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB]:       /images/XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB.jpg "The XR_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_FB bone joint."

When a user enables eye tracking for your app, your app is granted access to real time abstracted gaze data, which is user data under the [Developer Data Use Policy][dup]. You are expressly forbidden from using this data for [Data Use Prohibited Practices][dup-prohibited] in accordance with the Developer Data Use Policy. The eye tracking feature is powered by our Eye Tracking API technology.

## Known issues
- None

## Integrate eye tracking

After completing this section, the developer should be able to:

1. Set up a new project for eye tracking.

2. Enable a character to support eye tracking.

**Note**: Before following these steps, check the prerequisites in the [Movement SDK Getting Started](/documentation/unity/move-unity-getting-started/).

### Set up a project that supports eye tracking

After you have configured your project for VR, follow these steps.

1. Make sure you have an **OVRCameraRig** prefab in your scene. The prefab is located at `Packages/com.meta.xr.sdk.core/Prefabs/OVRCameraRig.prefab`.
2. Select the **OVRCameraRig** object in the Hierarchy. In the Inspector, navigate to the **OVRManager** component.
3. Select **Target Devices**.
4. Scroll down to **Quest Features** > **General**.
5. If you want hand tracking, select **Controllers and Hands** for **Hand Tracking Support**.
6. Under **General**, set **Eye Tracking Support** to **Supported** or **Required**. Select **Supported** if eye tracking is optional for your app, or **Required** if your app cannot function without it. Click **General** if that view isn't showing.
7. Under **OVRManager**, select **Eye Tracking**  under  **Permissions Request On Startup**.
8. If your project depends on face tracking, eye tracking, or hand tracking, ensure that these are enabled on your HMD. This is typically part of the device setup, but you can verify or change the settings by clicking **Settings** > **Movement Tracking**.
10. Fix any issues diagnosed by the Project Setup Tool. On the menu in Unity, go to **Edit** > **Project Settings** > **Meta XR** > to access the Project Setup Tool.
11. Select your platform.
12. Select **Fix All** if there are any issues. For details, see [Use Project Setup Tool](/documentation/unity/unity-upst-overview/).

### Setting up a character for face tracking
1. Choose the GameObject that will represent your character’s eyeball.
2. Attach the [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) component to it.
3. Set the component’s reference frame, specify the eye (left or right), and set the confidence threshold.
4. Enable **Apply Rotation** to allow real-time rotation of the eye GameObject as it tracks the corresponding eye. Optionally, enable **Apply Position** to allow positional adjustments.

This component needs a reference frame in world space orientation to function correctly, typically aligned with the eye's forward direction, to calculate the eye GameObject's initial offset.

The key attributes include **Confidence Threshold** and **Tracking Mode**. If the eye tracking data falls below the set confidence threshold, the [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) will not apply the data to the GameObject.

Tracking modes include:
* **World Space**: Converts eye pose from tracking to world space.
* **Head Space**: Converts eye pose from tracking to local space relative to the VR camera rig.
* **Tracking Space**: Uses raw pose data from VR tracking space.

## FAQ

**Do I need to apply correctives for eye tracking?**
No, correctives are generally not necessary, although calibration through the Meta Quest Pro OS might be required for enhanced accuracy.

**How does this work with realistic or stylized characters?**
The [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) can animate any character's eye. Stylized characters with larger eyes may show more pronounced movements.

**Is Eye Tracking available on both Meta Quest Pro and Meta Quest?**
Currently, only the Quest Pro supports Eye Tracking.

**Can I use eye tracking to emphasize specific scene areas?**
Yes, using raycasting driven by the eye transform’s forward direction can highlight areas of interest.

**What if the user denies Eye Tracking permission?**
If permissions are not granted, the [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) will not control the eye.

**Does eye tracking provide confidence values?**
Yes, the [`OVREyeGaze`](/reference/unity/latest/class_o_v_r_eye_gaze/) includes a **Confidence** field that ranges from 0 to 1, with higher values indicating greater reliability.
# Move Face Tracking

**Documentation Index:** Learn about move face tracking in this documentation.

---

---
title: "Face Tracking for Movement SDK for Unity"
description: "Set up face tracking with FACS-based, ARKit, or Viseme blendshapes to animate character expressions in Unity."
last_updated: "2025-08-28"
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

The Face Tracking API allows developers to use abstracted facial expression data to enhance social presence. For example, face tracking can help make a character's facial expression look more natural during virtual interactions with other users. At a high level, creating a character with face tracking consists of creating the character to be represented with blendshapes that represent the facial expressions of your character, and adding the scripts to the character containing the blendshapes to read the API and map the expressions detected to the character blendshapes.

The Face Tracking API supports expressions based on the Facial Action Coding System (FACS) or the Oculus Viseme-based expressions. The FACS expressions represent the different 70 muscles that are used to animate the face. Visemes represent the shape of the mouth when producing phonemes (sounds) and are represented by 15 blendshapes. Both the traditional Lipsync library and the Face Tracking API described in this section use the same 15 Oculus Visemes.

On the Quest Pro, the facial movements detected by the headset sensors are converted to activations of the expressions of the FACS blendshapes, such as jaw drop and nose wrinkle.  On other Quest headsets, the audio stream is analyzed and translated into either FACS expressions or into Oculus Visemes relying on a machine learning model that has been trained on speech samples using a feature we call Audio To Expressions.

Within the face tracking implementation in Unity, these expressions are mapped into FACS-based blendshapes that an artist has created to represent the facial expressions of the character. In highly realistic cases and in the samples, there is one of these blendshapes representing each of the expressions detected by the sensors. However, it is important to realize that a human wearing the headset will typically trigger a multiple of these expressions at the same time. For instance, when you smile, you may see lip corner raisers and also other actions on the cheek or even the eye.  The API returns a weight corresponding to the strength of the expression (for example, barely raising an eyebrow or an extreme raise of an eyebrow). The list of expressions that are fired along with their weights are then used to activate the blendshapes.  Since these blendshape meshes can be deformed corresponding to the strength (or weight) of the expression, the combination of the different meshes combine to create the effect desired. There is no absolute requirement to match the number of FACS-based blendshapes and the number of expressions.  For instance, you could create a simple avatar with two blendshapes (neutral and smile). You could then map only a few of the expressions provided, such as lip corner raiser, to detect a smile and activate the smile blendshape.

The Face Tracking API provides FACS-based blendshapes that represent most of the face including nose, mouth, jaw, eyebrows, and areas close to the eye. This provides coverage for the facial movements that make up smiling, frowning, surprise, and other facial expressions. This allows developers to provide their users with characters that can range from high-quality 3D representations for realistic VR experiences, to extremely stylized ones for fantasy and science-fiction environments.

For highly realistic characters, it is especially important to use accurate photogrammetry that will generate face assets and provide each of the 70 FACS expressions or the 15 visemes that the Face Tracking API defines. These can be combined to a skinned mesh.

## Policies and disclaimers

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

When a user enables Natural Facial Expressions for your app, your app is granted access to real time abstracted facial expressions data which is user data under the [Developer Data Use Policy][dup]. This data is only permitted to be used for purposes outlined in the Developer Data Use Policy.  You are expressly forbidden from using this data for [Data Use Prohibited Practices][dup-prohibited]. The Natural Facial Expressions feature is powered by our Face Tracking API.

Your use of the Face Tracking API must at all times be consistent with the [Oculus SDK License Agreement][oc-sdk-license-agreement], the [Developer Data Use Policy][dup] and all applicable Oculus and Meta policies, terms, and conditions. Applicable privacy and data protection laws may cover your use of Movement, and all applicable privacy and data protection laws.

In particular, you must post and abide by a publicly available and easily accessible privacy policy that clearly explains your collection, use, retention, and processing of data through the Face Tracking API. You must ensure that a user is provided with clear and comprehensive information about, and consents to, your access to and use of abstracted facial expression data prior to collection, including as required by applicable privacy and data protection laws.

Please note that Meta reserves the right to monitor your use of the Face Tracking API to enforce compliance with our policies.

## Known issues

The following are known issues:
* Confidence values for Face Tracking API facial expressions are not populated.
* Audio to Expressions: Yawning may not result in the character opening their mouth as the models are trained mostly on verbal input.
* Audio to Expressions: When the app first launches, there may be up to 10 seconds until the voice is fully connected to drive the face animation. This problem has been observed after a reboot of the device.
* Similar to visual based face tracking, the Audio to Expressions model is trained on how users really move their lips.  For users expecting more exaggerated movements like provided with visemes, you can increase the multipliers in the retargeting configuration file.  See the JSON configuration section.

## Integrate face tracking

After completing this section, the developer should be able to:

1. Set up a new project for face tracking.

2. Enable a character using blendshapes corresponding directly to the Face Tracking API for face tracking.

3. Enable a character using ARKit blendshapes for face tracking.

4. Enable a character using Visemes for face tracking.

**Note**: Before following these steps, check the prerequisites in the [Movement SDK Getting Started](/documentation/unity/move-unity-getting-started/).

### Set up a project that supports face tracking

After configuring your project for VR, follow these steps.

1. Make sure you have an **OVRCameraRig** prefab in your scene. The prefab is located at `Packages/com.meta.xr.sdk.core/Prefabs/OVRCameraRig.prefab`.
1. Select the **OVRCameraRig** object in the **Hierarchy**. Then, locate the **OVRManager** component in the **Inspector**.
1. Expand the **Quest Features** item and click **General**.
1. If you want hand tracking, select **Controllers and Hands** for **Hand Tracking Support**.
1. Ensure **Face Tracking Support** is set to **Supported**.
1. Fix any issues diagnosed by the Project Setup Tool. On the menu in Unity, go to **Edit** > **Project Settings** > **Meta XR** to access the Project Setup Tool.
1. Select the **Android, Meta Quest** platform tab.
1. Click **Fix All** if there are any issues. For details, see [Use Project Setup Tool](/documentation/unity/unity-upst-overview/).

**Note**: If your project depends on Face Tracking, Eye Tracking, or Hand Tracking, ensure that these are enabled on your headset. This is typically part of the device setup, but you can verify or change the settings by clicking **Settings** > **Movement Tracking**.

### Setting up a character for FACS-based face tracking

**Note**:  Some of the scripts below are distributed in the sample and not in Meta XR All-in-One SDK. As such, it is necessary to download the [Oculus Samples GitHub repo](https://github.com/oculus-samples/Unity-Movement) to have access to these scripts. It is distributed as a package, so you can add it to an existing project.

- Step 1: Characters with FACS-based blendshapes. If your character has blendshapes that correspond to our face tracking API blendshapes (see [FACS-based blendshapes](/documentation/unity/move-face-tracking/#facs-based-blendshapes)) follow this step. Otherwise, skip to Step 2.
   - Step 1a: There is a helper function under **Game Object** > **Movement SDK** > **Face Tracking** > **Add A2E Face**. This should be used as a base setting, but it should be examined and modified based on the target character to make certain that blendshapes predictably work together.

- Step 2: Characters with ARKit Blendshapes. Right-click on your character's face skinned mesh that has blendshapes, then use the ARKit helper function under **Game Object** > **Movement SDK** > **Face Tracking** > **Add A2E ARKit Face** which establishes our default mappings for ARKit.

- Step 3: Make sure your character has `FaceDriver` and `FaceRetargeterComponent` components, and the `FaceRetargeterComponent` component references a `OVRWeightsProvider`. The `FaceDriver` should reference meshes that animate in response to face tracking.

- Step 4: Test your character. At this point your character should be ready to test by donning the headset with the character being used. The `FaceRetargeterComponent`'s “Retargeter Config” field references a JSON that can be modified to influence how the character animates.

**Note:** If you notice incorrect lighting on your character as it animates such as unusual creases that appear near the eyelids, please use the `RecalculateNormals` component.

To configure the `RecalculateNormals` component:
  1. Assign the character’s face skinned mesh renderer to the **Skinned Mesh Renderer** field.
      * You must use a compatible material based on `Movement/PBR (Specular)` or `Movement/PBR (Metallic)` on the skinned mesh renderer in order for normal recalculation to work.
  2. Set the **Recalculate Material Indices** array to the indices of the materials that normal recalculation needs to be run on. Indicate the index of the sub mesh that recalculation should operate on using the “Sub Mesh” array.
  3. Set the **Duplicate Layer** field to the layer that the character is located on.
  4. Set the **Hidden Mesh Layer Name** field to the layer name that isn’t rendered by the camera.
      * If no layer exists, head to **Edit** > **Project Settings** > **Tags and Layers**, expand the **Layers** section, and then create a new layer in an empty slot (for example, enter a name like `HiddenMesh` in one of the User Layer fields). Use this new layer name for the **Hidden Mesh Layer Name** field. The `RecalculateNormals` script will change the culling mask for all cameras in the scene to exclude the rendering of this layer.
  5. Enable the **Recalculate Independently** field.

#### Scripts and components

This section offers a brief description for the **OVR Face Expressions** component that defines the blendshapes drive face tracking.
#### OVR face expressions            {#ovrfaceexpressions}

The [`OVRFaceExpressions`][OVRFaceExpressions.cs] component queries and updates face expression data every frame. The `OVRFaceExpressions` component script defines an indexer for accessing face expression data and provides the field `ValidExpressions` to indicate whether or not expression data is valid. This component only works in apps that users granted face tracking permissions for.

#### FaceRetargeterComponent

Implements a retargeting  `WeightsProvider` to map source tracking weights to a set of target weights based on a JSON configuration file. Each item in this file defines a set of input drivers, and what combination of output weights this specific combination should map to. The `FaceRetargeterComponent` class then creates a mapping function implementing these. The intended consumer for these weights is a `FaceDriver` instance.

#### FaceDriver
Implements a rig concept based on a naming convention, and drives the deformation. Using a list of blendshapes extracted from a list of skinned meshes, it builds a `RigLogic` instance, which interprets each name in terms of direct driver signals, in-betweens and correctives, and assembles their activation functions. The signals from the associated `WeightsProvider` then drive the deformation.

#### WeightsProvider
An abstract class that provides weights values to consumers. Examples include `FaceRetargeterComponent` and `OVRWeightsProvider`.

#### JSON configuration

Each character's `FaceRetargeterComponent` references "Retargeter Config" field that can be used to tweak the character's performance by modifying the drivers (`FaceExpression`) that drive the targets (target FACS-based blendshapes) via weights. For instance, when setting up a character using **Game Object**->**Movement SDK**->**Face Tracking**->**Add A2E ARKit Face**, a default `arkit_retarget_a2e_v10.json` configuration is added. Each entry is this configuration has an `FaceExpression` name followed by a list of ARKit blendshape names that are driven by it. If you wish for the corresponding ARKit blendshape to react differently, you can increase the weight next to it. Optionally, you can change the FACS-based blendshapes modified by removing and adding entries. This workflow does not apply to Viseme-based blendshapes.

### Setting up a character for Viseme-based face tracking

This section describes configuring a character with [Viseme-compatible blendshapes](/documentation/unity/move-face-tracking/#viseme-blendshapes).

To use Visemes, follow the steps discussed [above](/documentation/unity/move-face-tracking/#set-up-a-project-that-supports-face-tracking), and then enable the following:

1. Select **OVRCameraRig** in the **Hierarchy**. Locate the **OVR Manager (Script)** item in the **Inspector**.
1. Expand **Quest Features**. Then, click **Experimental** and check the **Experimental Features Enabled** item.
1. Expand **Tracking** > **Face Tracking**. Then, check the **Visual**, **Audio**, and **Enable visemes** items.
1. Expand **Permission Requests On Startup**. Then, check the **Record Audio for audio base Face Tracking** item.

[`OVRFaceExpressions`][OVRFaceExpressions.cs] provides several properties and functions to access visemes, and you may use the component's `AreVisemesValid` to query viseme validity, and `GetViseme` to obtain the weight of a given `FaceViseme`. `TryGetFaceViseme` is similar to `GetViseme`, except it returns `false` if the provided `FaceViseme` is invalid.

For quicker Viseme integration, use the `VisemeDriver` component in the [Oculus Samples GitHub package](https://github.com/oculus-samples/Unity-Movement). To use it, follow these steps:

- Step 1: Add `VisemeDriver` as a component to each skinned mesh renderer that has viseme-compatible blendshapes.
- Step 2: Click on the "Auto Generate Mapping" button to associate the skinned mesh renderer blendshapes to visemes.
   - Step 2a: The "Clear Mapping" button can be used to clear these associations.
- Step 3: Check the mappings that have been generated on the `VisemeDriver` component and make changes if required.

If you notice unusual creases that appear near the eyelids, please use the `RecalculateNormals` component discussed above.

## FAQ

1. **Which headset models support face tracking?**<br/>
   Natural Facial Expressions, which estimates facial movements based on inward facing cameras, is only available on Meta Quest Pro headsets. However, Audio To Expressions is available on all Meta Quest 2, Meta Quest 3, and Meta Quest 3S devices with the same API.
2. **How do I adapt my existing blendshapes to the FACS blendshapes Face Tracking API provides?**<br/>
  If the existing blendshapes don’t match the naming convention of the expected blendshapes in [`OVRFace`](/reference/unity/latest/class_o_v_r_face), the blendshapes will need to be manually assigned. One may also inherit [`OVRCustomFace`](/reference/unity/latest/class_o_v_r_custom_face ) to create their own custom mapping. The blendshape visual reference can be found in the [Movement - Face BlendShapes](/documentation/unity/move-face-tracking/#facs-based-blendshapes) topic.
3. **Are tongue blendshapes supported?**<br/>
   Yes, a total seven tongue blendshapes are supported, including tongue out, among the FACS blendshapes provided.

## FACS-based blendshapes

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

|---------------------------------------------------------------------|
| **Blendshapes**                                                     |
|---------------------------------------------------------------------|
| `BROW_LOWERER_L` and `BROW_LOWERER_R` knit and lower the brow area and lower central forehead. |
| ![XR_Face_Expression_2_Brow_Lowerer_L] |
| ![XR_Face_Expression_2_Brow_Lowerer_R] |
|
| `CHEEK_PUFF_L` and `CHEEK_PUFF_R` fill the cheeks with air causing them to round and extend outward. |
| ![XR_Face_Expression_2_Cheek_Puff_L] |
| ![XR_Face_Expression_2_Cheek_Puff_R] |
|
| `CHEEK_RAISER_L` and `CHEEK_RAISER_R` tighten the outer rings of the eye orbit and squeeze the lateral eye corners. |
| ![XR_Face_Expression_2_Cheek_Raiser_L] |
| ![XR_Face_Expression_2_Cheek_Raiser_R] |
|
| `CHEEK_SUCK_L` and `CHEEK_SUCK_R` suck the cheeks inward and against the teeth to create a hollow effect in the cheeks. |
| ![XR_Face_Expression_2_Cheek_Suck_L] |
| ![XR_Face_Expression_2_Cheek_Suck_R] |
|
| `CHIN_RAISER_B` and `CHIN_RAISER_T` push the skin of the chin and the lower lip upward. When the lips are touching, the upward force from the lower lip pushes up the top lip as well. |
| ![XR_Face_Expression_2_Chin_Raiser_B] |
| ![XR_Face_Expression_2_Chin_Raiser_T] |
|
| `DIMPLER_L` and `DIMPLER_R` pinch the lip corners against the teeth, drawing them slightly backward and often upward in the process. |
| ![XR_Face_Expression_2_Dimpler_L] |
| ![XR_Face_Expression_2_Dimpler_R] |
|
| `EYES_CLOSED_L` and `EYES_CLOSED_R` lower the top eyelid to cover the eye. |
| ![XR_Face_Expression_2_Eyes_Closed_L] |
| ![XR_Face_Expression_2_Eyes_Closed_R] |
|
| `EYES_LOOK_DOWN_L` and `EYES_LOOK_DOWN_R` move the eyelid consistent with downward gaze. |
| ![XR_Face_Expression_2_Eyes_Look_Down_L] |
| ![XR_Face_Expression_2_Eyes_Look_Down_R] |
|
| `EYES_LOOK_LEFT_L` and `EYES_LOOK_LEFT_R` move the eyelid consistent with leftward gaze. |
| ![XR_Face_Expression_2_Eyes_Look_Left_L] |
| ![XR_Face_Expression_2_Eyes_Look_Left_R] |
|
| `EYES_LOOK_RIGHT_L` and `EYES_LOOK_RIGHT_R` move the eyelid consistent with rightward gaze. |
| ![XR_Face_Expression_2_Eyes_Look_Right_L] |
| ![XR_Face_Expression_2_Eyes_Look_Right_R] |
|
| `EYES_LOOK_UP_L` and `EYES_LOOK_UP_R` move the eyelid consistent with upward gaze. |
| ![XR_Face_Expression_2_Eyes_Look_Up_L] |
| ![XR_Face_Expression_2_Eyes_Look_Up_R] |
|
| `INNER_BROW_RAISER_L` and `INNER_BROW_RAISER_R` lift the medial brow and forehead area. |
| ![XR_Face_Expression_2_Inner_Brow_Raiser_L] |
| ![XR_Face_Expression_2_Inner_Brow_Raiser_R] |
|
| `JAW_DROP` moves the lower mandible downward and toward the neck. |
| ![XR_Face_Expression_2_Jaw_Drop] |
|
| `JAW_SIDEWAYS_LEFT` moves the lower mandible leftward. |
| ![XR_Face_Expression_2_Jaw_Sideways_Left] |
|
| `JAW_SIDEWAYS_RIGHT` moves the lower mandible rightward. |
| ![XR_Face_Expression_2_Jaw_Sideways_Right] |
|
| `JAW_THRUST` projects the lower mandible forward. |
| ![XR_Face_Expression_2_Jaw_Thrust] |
|
| `LID_TIGHTENER_L` and `LID_TIGHTENER_R` tighten the rings around the eyelids and push the lower eyelid skin toward the inner eye corners. |
| ![XR_Face_Expression_2_Lid_Tightener_L] |
| ![XR_Face_Expression_2_Lid_Tightener_R] |
|
| `LIP_CORNER_DEPRESSOR_L` and `LIP_CORNER_DEPRESSOR_R` draw the lip corners downward. |
| ![XR_Face_Expression_2_Lip_Corner_Depressor_L] |
| ![XR_Face_Expression_2_Lip_Corner_Depressor_R] |
|
| `LIP_CORNER_PULLER_L` and `LIP_CORNER_PULLER_R` draw the lip corners up, back, and laterally. |
| ![XR_Face_Expression_2_Lip_Corner_Puller_L] |
| ![XR_Face_Expression_2_Lip_Corner_Puller_R] |
|
| `LIP_FUNNELER_LB`, `LIP_FUNNELER_LT`, `LIP_FUNNELER_RB`, and `LIP_FUNNELER_RT` fan the lips outward in a forward projection, often rounding the mouth and separating the lips. |
| ![XR_Face_Expression_2_Lip_Funneler_LB] |
| ![XR_Face_Expression_2_Lip_Funneler_LT] |
| ![XR_Face_Expression_2_Lip_Funneler_RB] |
| ![XR_Face_Expression_2_Lip_Funneler_RT] |
|
| `LIP_PRESSOR_L` and `LIP_PRESSOR_R` press the upper and lower lips against one another. |
| ![XR_Face_Expression_2_Lip_Pressor_L] |
| ![XR_Face_Expression_2_Lip_Pressor_R] |
|
| `LIP_PUCKER_L` and `LIP_PUCKER_R` draw the lip corners medially causing the lips to protrude in the process. |
| ![XR_Face_Expression_2_Lip_Pucker_L] |
| ![XR_Face_Expression_2_Lip_Pucker_R] |
|
| `LIP_STRETCHER_L` and `LIP_STRETCHER_R` draw the lip corners laterally, stretching the lips and widening the jawline. |
| ![XR_Face_Expression_2_Lip_Stretcher_L] |
| ![XR_Face_Expression_2_Lip_Stretcher_R] |
|
| `LIP_SUCK_LB`, `LIP_SUCK_LT`, `LIP_SUCK_RB`, and `LIP_SUCK_RT` suck the lips toward the inside of the mouth. |
| ![XR_Face_Expression_2_Lip_Suck_LB] |
| ![XR_Face_Expression_2_Lip_Suck_LT] |
| ![XR_Face_Expression_2_Lip_Suck_RB] |
| ![XR_Face_Expression_2_Lip_Suck_RT] |
|
| `LIP_TIGHTENER_L` and `LIP_TIGHTENER_R` narrow or constrict each lip on a horizontal plane. |
| ![XR_Face_Expression_2_Lip_Tightener_L] |
| ![XR_Face_Expression_2_Lip_Tightener_R] |
|
| `LIPS_TOWARD` forces contact between top and bottom lips to keep the mouth closed regardless of the position of the jaw. |
| ![XR_Face_Expression_2_Lips_Toward] |
|
| `LOWER_LIP_DEPRESSOR_L` and `LOWER_LIP_DEPRESSOR_R` draw the lower lip downward and slightly laterally. |
| ![XR_Face_Expression_2_Lower_Lip_Depressor_L] |
| ![XR_Face_Expression_2_Lower_Lip_Depressor_R] |
|
| `MOUTH_LEFT` pulls the left lip corner leftward and pushes the right side of the mouth toward the left lip corner. |
| ![XR_Face_Expression_2_Mouth_Left] |
|
| `MOUTH_RIGHT` pulls the right lip corner rightward and pushes the left side of the mouth toward the right lip corner. |
| ![XR_Face_Expression_2_Mouth_Right] |
|
| `NOSE_WRINKLER_L` and `NOSE_WRINKLER_R` lift the sides of the nose, nostrils, and central upper lip area. Often pairs with brow lowering muscles to lower the medial brow tips. |
| ![XR_Face_Expression_2_Nose_Wrinkler_L] |
| ![XR_Face_Expression_2_Nose_Wrinkler_R] |
|
| `OUTER_BROW_RAISER_L` and `OUTER_BROW_RAISER_R` lift the lateral brows and forehead areas. |
| ![XR_Face_Expression_2_Outer_Brow_Raiser_L] |
| ![XR_Face_Expression_2_Outer_Brow_Raiser_R] |
|
| `UPPER_LID_RAISER_L` and `UPPER_LID_RAISER_R` pull the top eyelid up and back to widen eyes. |
| ![XR_Face_Expression_2_Upper_Lid_Raiser_L] |
| ![XR_Face_Expression_2_Upper_Lid_Raiser_R] |
|
| `UPPER_LIP_RAISER_L` and `UPPER_LIP_RAISER_R` lift the top lip (in a more lateral manner than nose wrinkler). |
| ![XR_Face_Expression_2_Upper_Lip_Raiser_L] |
| ![XR_Face_Expression_2_Upper_Lip_Raiser_R] |
|
| `TONGUE_TIP_INTERDENTAL` raises the tip of the tongue to touch the top teeth like with the viseme "TH". The tongue is visible and slightly sticks out past the teeth line. |
| ![XR_Face_Expression_2_Tongue_Tip_Interdental] |
|
| `TONGUE_TIP_ALVEOLAR` raises the tip of the tongue to touch the back of the top teeth like in the viseme "NN". |
| ![XR_Face_Expression_2_Tongue_Tip_Alveolar] |
|
| `TONGUE_FRONT_DORSAL_PALATE` presses the front part of the tongue against the palate like in the viseme "CH". |
| ![XR_Face_Expression_2_Tongue_Front_Dorsal_Palate] |
|
| `TONGUE_MID_DORSAL_PALATE` presses the middle of the tongue against the palate like in the viseme "DD". |
| ![XR_Face_Expression_2_Tongue_Mid_Dorsal_Palate] |
|
| `TONGUE_BACK_DORSAL_VELAR` presses the back of the tongue against the palate like in the viseme "KK". |
| ![XR_Face_Expression_2_Tongue_Back_Dorsal_Velar] |
|
| `TONGUE_OUT` sticks the tongue out. |
| ![XR_Face_Expression_2_Tongue_Out] |
|
| `TONGUE_RETREAT` pulls the tongue back in the throat and makes the tongue stay down like in the viseme "AA". |
| ![XR_Face_Expression_2_Tongue_Retreat] |

## Viseme blendshapes

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

|------------------------------------------------------------------------------------|
| **Viseme** | **Phonemes** | **Examples** | **Mild** | **Emphasized** | **Rotated** |
|------------------------------------------------------------------------------------|
| SIL | neutral | | ![XR_META_Face_Tracking_Viseme_SIL] | None | ![XR_META_Face_Tracking_Viseme_SIL_rot] |
| PP | p, b, m | put, bat, mat | ![XR_META_Face_Tracking_Viseme_PP] | ![XR_META_Face_Tracking_Viseme_PP_emp] | ![XR_META_Face_Tracking_Viseme_PP_rot] |
| FF | f, v | fat, vat | ![XR_META_Face_Tracking_Viseme_FF] | ![XR_META_Face_Tracking_Viseme_FF_emp] | ![XR_META_Face_Tracking_Viseme_FF_rot] |
| TH | th | think, that | ![XR_META_Face_Tracking_Viseme_TH] | ![XR_META_Face_Tracking_Viseme_TH_emp] | ![XR_META_Face_Tracking_Viseme_TH_rot] |
| DD | t, d | tip, doll | ![XR_META_Face_Tracking_Viseme_DD] | ![XR_META_Face_Tracking_Viseme_DD_emp] | ![XR_META_Face_Tracking_Viseme_DD_rot] |
| KK | k, g | call, gas | ![XR_META_Face_Tracking_Viseme_KK] | ![XR_META_Face_Tracking_Viseme_KK_emp] | ![XR_META_Face_Tracking_Viseme_KK_rot] |
| CH | tS, dZ, S | chair, join, she | ![XR_META_Face_Tracking_Viseme_CH] | ![XR_META_Face_Tracking_Viseme_CH_emp] | ![XR_META_Face_Tracking_Viseme_CH_rot] |
| SS | s, z | sir, zeal | ![XR_META_Face_Tracking_Viseme_SS] | ![XR_META_Face_Tracking_Viseme_SS_emp] | ![XR_META_Face_Tracking_Viseme_SS_rot] |
| NN | n, l | lot, not | ![XR_META_Face_Tracking_Viseme_NN] | ![XR_META_Face_Tracking_Viseme_NN_emp] | ![XR_META_Face_Tracking_Viseme_NN_rot] |
| RR | r | red | ![XR_META_Face_Tracking_Viseme_RR] | ![XR_META_Face_Tracking_Viseme_RR_emp] | ![XR_META_Face_Tracking_Viseme_RR_rot] |
| AA | A: | car | ![XR_META_Face_Tracking_Viseme_AA] | ![XR_META_Face_Tracking_Viseme_AA_emp] | ![XR_META_Face_Tracking_Viseme_AA_rot] |
| E | e | bed | ![XR_META_Face_Tracking_Viseme_E] | ![XR_META_Face_Tracking_Viseme_E_emp] | ![XR_META_Face_Tracking_Viseme_E_rot] |
| IH | ih | tip | ![XR_META_Face_Tracking_Viseme_IH] | ![XR_META_Face_Tracking_Viseme_IH_emp] | ![XR_META_Face_Tracking_Viseme_IH_rot] |
| OH | oh | toe | ![XR_META_Face_Tracking_Viseme_OH] | ![XR_META_Face_Tracking_Viseme_OH_emp] | ![XR_META_Face_Tracking_Viseme_OH_rot] |
| OU | ou | book | ![XR_META_Face_Tracking_Viseme_OU] | ![XR_META_Face_Tracking_Viseme_OU_emp] | ![XR_META_Face_Tracking_Viseme_OU_rot] |
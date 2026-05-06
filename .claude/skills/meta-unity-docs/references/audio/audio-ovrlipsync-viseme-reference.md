# Audio Ovrlipsync Viseme Reference

**Documentation Index:** Learn about audio ovrlipsync viseme reference in this documentation.

---

---
title: "Viseme Reference"
description: "A complete table of visemes detected by Oculus Lipsync, with reference images."
---

<oc-devui-note type="warning" heading="End-of-Life Notice for Oculus Spatializer Plugin">
<p>The Oculus Spatializer Plugin has been replaced by the Meta XR Audio SDK and is now in end-of-life stage. It will not receive any further support beyond v47. We strongly discourage its use. Please navigate to the Meta XR Audio SDK documentation for your specific engine:

<br>- <a href="/documentation/unity/meta-xr-audio-sdk-unity-intro/">Meta XR Audio SDK for Unity Native</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unity</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unity</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-unreal-intro/">Meta XR Audio SDK for Unreal Native</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unreal</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unreal</a>
</p>

<p><strong>This documentation is no longer being updated and is subject for removal.</strong></p>
</oc-devui-note>

Oculus Lipsync maps human speech to a set of mouth shapes, called "visemes", which are a visual analog to phonemes. Each viseme depicts the mouth shape for a specific set of phonemes. Over time these visemes are interpolated to simulate natural mouth motion. Below we give the reference images we used to create our own demo shapes. For each row we give the viseme name, example phonemes that map to that viseme, example words, and images showing both mild and emphasized production of that viseme. We hope that you will find these useful in creating your own models. For more information on these 15 visemes and how they were selected, please read the following documentation: [Viseme MPEG-4 Standard](https://www.visagetechnologies.com/uploads/2012/08/MPEG-4FBAOverview.pdf)

## Animated example

The animation that follows shows the visemes from the reference image section.

<image handle="GN3_OAI0UnGbvU4BAAAAAABsqJAgbj0JAAAC" style="width:100%;max-width:300pt" title="Demo lips geometry showing each viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-1.gif"/>

## Reference Images

You can click each image to view in larger size. Only a subset of phonemes are shown for each viseme.

<table>
  <thead>
    <tr>
      <th>Viseme Name</th>
      <th>Phonemes</th>
      <th>Examples</th>
      <th>Mild Production</th>
      <th>Emphasized Production</th>
      <th>3/4 Rotation</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>
        sil
      </td>
      <td>
        neutral
      </td>
      <td>
        (none - silence)
      </td>
      <td>
        <file-link handle="GMNZVQJaoPGQAfQAAAAAAAAxMzoObj0JAAAB"><image style="width: 175px;" handle="GMNZVQJaoPGQAfQAAAAAAAAxMzoObj0JAAAB" title="sil viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-2.jpg"/></file-link>
      </td>
      <td>
      None
      </td>
      <td>
        <file-link handle="GPzDVAJfxE-A0-sAAAAAAAABPTdhbj0JAAAB"><image style="width: 175px;" handle="GPzDVAJfxE-A0-sAAAAAAAABPTdhbj0JAAAB" title="sil viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-3.jpg" /></file-link>
      </td>
    </tr>
    <tr>
      <td>
        PP
      </td>
      <td>
        p, b, m
      </td>
      <td>
        put, bat, mat
      </td>
      <td>
        <file-link handle="GGd0VgJh-6Fy9sABAAAAAAC5knt8bj0JAAAB"><image style="width: 175px;" handle="GGd0VgJh-6Fy9sABAAAAAAC5knt8bj0JAAAB" title="Mild PP viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-4.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GB1jVwIRtI3Af98BAAAAAACz4q8gbj0JAAAB"><image style="width: 175px;" handle="GB1jVwIRtI3Af98BAAAAAACz4q8gbj0JAAAB" title="Emphasized PP viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-5.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GMHDVgL0B6GqD6YBAAAAAACIVgRCbj0JAAAB"><image style="width: 175px;" handle="GMHDVgL0B6GqD6YBAAAAAACIVgRCbj0JAAAB" title="PP viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-6.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        FF
      </td>
      <td>
        f, v
      </td>
      <td>
        fat, vat
      </td>
      <td>
        <file-link handle="GNyhVgKoJMPr74cCAAAAAADK2I4tbj0JAAAB"><image style="width: 175px;" handle="GNyhVgKoJMPr74cCAAAAAADK2I4tbj0JAAAB" title="Mild FF viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-7.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GAsWVgKjXXMmS-8AAAAAAAAZbiUvbj0JAAAB"><image style="width: 175px;" handle="GAsWVgKjXXMmS-8AAAAAAAAZbiUvbj0JAAAB" title="Emphasized FF viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-8.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GBo5PgIKSprTctEHAAAAAAAad4tkbj0JAAAB"><image style="width: 175px;" handle="GBo5PgIKSprTctEHAAAAAAAad4tkbj0JAAAB" title="FF viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-9.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        TH
      </td>
      <td>
        th
      </td>
      <td>
        think, that
      </td>
      <td>
        <file-link handle="GBNlVQLq58gEFcsAAAAAAABywSlWbj0JAAAB"><image style="width: 175px;" handle="GBNlVQLq58gEFcsAAAAAAABywSlWbj0JAAAB" title="Mild TH viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-10.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GOVhVwJxuBzjqCMBAAAAAADJ-wAvbj0JAAAB"><image style="width: 175px;" handle="GOVhVwJxuBzjqCMBAAAAAADJ-wAvbj0JAAAB" title="Emphasized TH viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-11.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GKjuVgIV5DUEuikBAAAAAAD-g9xsbj0JAAAB"><image style="width: 175px;" handle="GKjuVgIV5DUEuikBAAAAAAD-g9xsbj0JAAAB" title="TH viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-12.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        DD
      </td>
      <td>
        t, d
      </td>
      <td>
        tip, doll
      </td>
      <td>
        <file-link handle="GHW8VAINzh-AVV8CAAAAAADPfeYvbj0JAAAB"><image style="width: 175px;" handle="GHW8VAINzh-AVV8CAAAAAADPfeYvbj0JAAAB" title="Mild PDDP viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-13.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GKnlVQKxqoILcbgAAAAAAACErtsObj0JAAAB"><image style="width: 175px;" handle="GKnlVQKxqoILcbgAAAAAAACErtsObj0JAAAB" title="Emphasized DD viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-14.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GJYwVwJvLzjpz-sAAAAAAAAzGTkAbj0JAAAB"><image style="width: 175px;" handle="GJYwVwJvLzjpz-sAAAAAAAAzGTkAbj0JAAAB" title="DD viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-15.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        kk
      </td>
      <td>
        k, g
      </td>
      <td>
        call, gas
      </td>
      <td>
        <file-link handle="GGbsVgIGRhPAZkMDAAAAAAC8SspJbj0JAAAB"><image style="width: 175px;" handle="GGbsVgIGRhPAZkMDAAAAAAC8SspJbj0JAAAB" title="Mild kk viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-16.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GC5zVgKjw9uzdtYBAAAAAACWOmBUbj0JAAAB"><image style="width: 175px;" handle="GC5zVgKjw9uzdtYBAAAAAACWOmBUbj0JAAAB" title="Emphasized kk viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-17.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GJtfVgKV6raChGECAAAAAADSvZp0bj0JAAAB"><image style="width: 175px;" handle="GJtfVgKV6raChGECAAAAAADSvZp0bj0JAAAB" title="kk viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-18.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        CH
      </td>
      <td>
        tS, dZ, S
      </td>
      <td>
        chair, join, she
      </td>
      <td>
        <file-link handle="GLz3VQLqG_0kYOgHAAAAAACEhDNObj0JAAAB"><image style="width: 175px;" handle="GLz3VQLqG_0kYOgHAAAAAACEhDNObj0JAAAB" title="Mild CH viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-19.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GEt0VgLWKla9ipABAAAAAAB_kXIbbj0JAAAB"><image style="width: 175px;" handle="GEt0VgLWKla9ipABAAAAAAB_kXIbbj0JAAAB" title="Emphasized CH viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-20.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GHluVwLoYmdQ7eEAAAAAAACld3hVbj0JAAAB"><image style="width: 175px;" handle="GHluVwLoYmdQ7eEAAAAAAACld3hVbj0JAAAB" title="CH viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-21.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        SS
      </td>
      <td>
        s, z
      </td>
      <td>
        sir, zeal
      </td>
      <td>
        <file-link handle="GAjiVQJwZg2-cPIBAAAAAACF2EMhbj0JAAAB"><image style="width: 175px;" handle="GAjiVQJwZg2-cPIBAAAAAACF2EMhbj0JAAAB" title="Mild SS viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-22.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GAGyVwLaJfUm20EIAAAAAABi8GN-bj0JAAAB"><image style="width: 175px;" handle="GAGyVwLaJfUm20EIAAAAAABi8GN-bj0JAAAB" title="Emphasized SS viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-23.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GKbRVgLNhribkQAHAAAAAACIyIRGbj0JAAAB"><image style="width: 175px;" handle="GKbRVgLNhribkQAHAAAAAACIyIRGbj0JAAAB" title="SS viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-24.jpg"/></file-link>          
      </td>
    </tr>

    <tr>
      <td>
        nn
      </td>
      <td>
        n, l
      </td>
      <td>
        lot, not
      </td>
      <td>
        <file-link handle="GF-QVQIbsjqsV4IGAAAAAABSLVcMbj0JAAAB"><image style="width: 175px;" handle="GF-QVQIbsjqsV4IGAAAAAABSLVcMbj0JAAAB" title="Mild nn viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-25.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GDrFVQL7rhlL28sAAAAAAAA1mwUQbj0JAAAB"><image style="width: 175px;" handle="GDrFVQL7rhlL28sAAAAAAAA1mwUQbj0JAAAB" title="Emphasized nn viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-26.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GJcNVQIfgKyMfs4AAAAAAAA6hnYHbj0JAAAB"><image style="width: 175px;" handle="GJcNVQIfgKyMfs4AAAAAAAA6hnYHbj0JAAAB" title="nn viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-27.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        RR
      </td>
      <td>
        r
      </td>
      <td>
        red
      </td>
      <td>
        <file-link handle="GH7uVgKwQk4HT2wHAAAAAAAQ4nkVbj0JAAAB"><image style="width: 175px;" handle="GH7uVgKwQk4HT2wHAAAAAAAQ4nkVbj0JAAAB" title="Mild RR viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-28.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GFN9VQJOq4rV0cEHAAAAAABxScd8bj0JAAAB"><image style="width: 175px;" handle="GFN9VQJOq4rV0cEHAAAAAABxScd8bj0JAAAB" title="Emphasized RR viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-29.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GLnbVwJb30gKw4QCAAAAAAAMOxlsbj0JAAAB"><image style="width: 175px;" handle="GLnbVwJb30gKw4QCAAAAAAAMOxlsbj0JAAAB" title="RR viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-30.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        aa
      </td>
      <td>
        A:
      </td>
      <td>
        car
      </td>
      <td>
        <file-link handle="GEkzVQJNZUv5v9IHAAAAAADEAEBxbj0JAAAB"><image style="width: 175px;" handle="GEkzVQJNZUv5v9IHAAAAAADEAEBxbj0JAAAB" title="Mild aa viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-31.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GKMvVQI1_Xd5hWMCAAAAAADV5vlvbj0JAAAB"><image style="width: 175px;" handle="GKMvVQI1_Xd5hWMCAAAAAADV5vlvbj0JAAAB" title="Emphasized aa viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-32.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GNWJVgJtP1XnaRUBAAAAAAAlT7ktbj0JAAAB"><image style="width: 175px;" handle="GNWJVgJtP1XnaRUBAAAAAAAlT7ktbj0JAAAB" title="aa viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-33.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        E
      </td>
      <td>
        e
      </td>
      <td>
        bed
      </td>
      <td>
        <file-link handle="GOV9VQIitb4jvtgBAAAAAADIIp0ibj0JAAAB"><image style="width: 175px;" handle="GOV9VQIitb4jvtgBAAAAAADIIp0ibj0JAAAB" title="Mild E viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-34.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GM8XWALxYWuCSDEGAAAAAAAqAcwObj0JAAAB"><image style="width: 175px;" handle="GM8XWALxYWuCSDEGAAAAAAAqAcwObj0JAAAB" title="Emphasized E viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-35.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GIDDVwK1WV1DfrkGAAAAAADJyalTbj0JAAAB"><image style="width: 175px;" handle="GIDDVwK1WV1DfrkGAAAAAADJyalTbj0JAAAB" title="E viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-36.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        I
      </td>
      <td>
        ih
      </td>
      <td>
        tip
      </td>
      <td>
        <file-link handle="GCUZVQL8LcWCZhgBAAAAAAAOkIF7bj0JAAAB"><image style="width: 175px;" handle="GCUZVQL8LcWCZhgBAAAAAAAOkIF7bj0JAAAB" title="Mild I viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-37.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GEohVwLF5hpBPtsAAAAAAADO3ysDbj0JAAAB"><image style="width: 175px;" handle="GEohVwLF5hpBPtsAAAAAAADO3ysDbj0JAAAB" title="Emphasized I viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-38.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GHt0VgI1DLdJD8oDAAAAAAAAEL5Ibj0JAAAB"><image style="width: 175px;" handle="GHt0VgI1DLdJD8oDAAAAAAAAEL5Ibj0JAAAB" title="I viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-39.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        O
      </td>
      <td>
        oh
      </td>
      <td>
        toe
      </td>
      <td>
        <file-link handle="GL5yVgJD2fZ75b0FAAAAAADeNlFebj0JAAAB"><image style="width: 175px;" handle="GL5yVgJD2fZ75b0FAAAAAADeNlFebj0JAAAB" title="Mild O viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-40.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GIIVVgIA4EzqsyIBAAAAAAC1rvp9bj0JAAAB"><image style="width: 175px;" handle="GIIVVgIA4EzqsyIBAAAAAAC1rvp9bj0JAAAB" title="Emphasized O viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-41.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GMaKVAI7nxtVx1gHAAAAAAA_OuBBbj0JAAAB"><image style="width: 175px;" handle="GMaKVAI7nxtVx1gHAAAAAAA_OuBBbj0JAAAB" title="O viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-42.jpg"/></file-link>          
      </td>
    </tr>
    <tr>
      <td>
        U
      </td>
      <td>
        ou
      </td>
      <td>
        book
      </td>
      <td>
        <file-link handle="GKiqVQJlfViHiiYDAAAAAACwRqEJbj0JAAAB"><image style="width: 175px;" handle="GKiqVQJlfViHiiYDAAAAAACwRqEJbj0JAAAB" title="Mild U viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-43.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GHlQVwIkZcad_r8AAAAAAAA2zC0mbj0JAAAB"><image style="width: 175px;" handle="GHlQVwIkZcad_r8AAAAAAAA2zC0mbj0JAAAB" title="Emphasized U viseme" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-44.jpg"/></file-link>
      </td>
      <td>
        <file-link handle="GLflVQLwi_A5FcMAAAAAAABx-BBDbj0JAAAB"><image style="width: 175px;" handle="GLflVQLwi_A5FcMAAAAAAABx-BBDbj0JAAAB" title="U viseme 3/4 rotation" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-viseme-reference-45.jpg"/></file-link>          
      </td>
    </tr>
  </tbody>
 </table>
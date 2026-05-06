# Ps User Notifications

**Documentation Index:** Learn about ps user notifications in this documentation.

---

---
title: "User Notifications Overview"
description: "Send targeted notifications to users of your Meta Quest app using the User Notifications feature."
last_updated: "2025-11-04"
---

User notifications are short, free-form notifications that you can send to people in VR and on the Meta Horizon mobile app.

To view the user notifications dashboard, navigate to the **Engagement** > **User Notifications** tab on the [Meta Horizon Developer Dashboard](/manage).

You can send notifications either from a specific app to users who have entitled that app, or from your organization profile to your followers.

**Single-send notifications**: They are sent once to an entire audience that you define. The entire process of creating these notifications is completed on the user notifications dashboard. You might use single-send notifications in the following ways:
- Let your audience know about updates to your app.
- Share promotions in the Meta Horizon Store.
- Request an app review and raise awareness to upcoming in-app experiences.
- Notify your organization's followers about new releases, updates, or sales across your portfolio.

**Event-based notifications**: They are sent based on a user action or another event that you define in your code. These notifications encourage specific engagement based on activity. The creation of these notifications varies depending on the delivery surface. The event-based workflows for each surface are as follows:

- **Mobile**:
    1. Create and submit the notification for review on the user notifications dashboard.
    2. Add the necessary code to your app that will trigger the notification.
- **Headset**: Create and submit the notification for review on the user notifications dashboard.

You might use event-based notifications in the following ways:
- Let a user know that a friend has sent in-app currency.
- Celebrate a user achievement. For example, when a user completes a level in your game, you can encourage them to continue to the next level through a notification.

We'll cover details on implementation for each of these in [Create User Notifications](/documentation/unity/ps-user-notifications-create).

To select the appropriate notification type for your use case, refer to the table below for a detailed comparison.

<horizon-os-dev-center-docs>
<table>
  <thead>
      <tr>
        <th>
          <text type="body2-emphasized" color="secondary">
            Notification Type
         </text>
        </th>
        <th>
          <text type="body2-emphasized">
            Single send
          </text>
        </th>
        <th>
          <text type="body2-emphasized">
            Event-based
          </text>
        </th>
      </tr>
    </thead>
    <tbody>
     <tr>
       <td>
          <text type="body2-emphasized" color="secondary">
            Surface
         </text>
       </td>
       <td>
          <text>
            Mobile, Headset
         </text>
        </td>
       	<td>
          <text>
            Mobile, Headset
         </text>
        </td>
     </tr>
     <tr>
      <td colspan="3">
       <text type="body2-emphasized" color="secondary">
         Required Integrations
       </text>
      </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
            	Meta Horizon Developer Dashboard
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
         <list>
           <text type="body2-emphasized" color="secondary">
            Your App Code
         	</text>
         </list>
       </td>
       <td>
          <text>
            ❌
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <text type="body2-emphasized" color="secondary">
            Audience
         </text>
       </td>
       <td>
         <list>
           <list-item><text>All entitled users</text></list-item>
           <list-item><text>Only active/inactive entitled users</text></list-item>
         </list>
        </td>
       	<td>
          <text>
            Any entitled user who has app installed and hits the event trigger
         </text>
        </td>
     </tr>
     <tr>
      <td colspan="3">
       <text type="body2-emphasized" color="secondary">
         Notification Delivery Channel
       </text>
      </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              VR notification feed
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
      <tr>
       <td>
         <list>
            <text type="body2-emphasized" color="secondary">
              VR notification push
         		</text>
         </list>
       </td>
       <td>
          <text>
            ❌
         </text>
        </td>
       	<td>
          <text>
            ❌
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Mobile notification feed
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Mobile notification push
         		</text>
         </list>
       </td>
       <td>
          <text>
            ❌
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
      <td colspan="3">
       <text type="body2-emphasized" color="secondary">
         Destinations
       </text>
      </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              PDP
         	</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Add-on
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Post
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Event
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Ratings and reviews
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Launch app
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Launch app destinations
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td colspan="3">
       <text type="body2-emphasized" color="secondary">
         Supported App Types
       </text>
      </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              2D
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Unity
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
     <tr>
       <td>
          <list>
            <text type="body2-emphasized" color="secondary">
              Unreal
         		</text>
         </list>
       </td>
       <td>
          <text>
            ✅
         </text>
        </td>
       	<td>
          <text>
            ✅
         </text>
        </td>
     </tr>
   </tbody>
 </table>
</horizon-os-dev-center-docs>

## User Notifications Dashboard

The user notifications dashboard is on the [Meta Horizon Developer Dashboard](/manage) under **Engagement** > **User Notifications**. The dashboard has two tabs:

* **Apps**: Shows notifications from all of your applications. You can filter by specific apps using the selector.
* **Followers**: Shows all notifications sent to your organization's followers.

You'll find all of your notifications appear here, along with the following:

* **Name**: The notification name.
* **Type**: Single send or event-based.
* **Status**: The current status of your single send notification or event-based notification delivery.
    * **Approved**: When a notification is approved and will run on the start date of its schedule.
    * **Pending**: When a notification isn't running yet because it's in review.
    * **Rejected**: When a notification can't run because there's a problem that needs attention.
    * **Canceled**: If you have canceled a notification and it will not run. Canceling a notification cannot be undone.
    * **Completed**: When a notification has run through the end of its schedule.
    * **Draft**: When a notification has been saved as a draft.
* **Created**: The date your notification was created.
* **Reach**: The number of people exposed to a notification at least once during a given period.
* **Clickthrough Rate**: The number of times a notification is clicked compared to the number of times it is displayed.
* **Unsubscribes**: The number of people who unsubscribed from the notification.
* **Three dot menu**: A menu with more admin options for each user notification.
    * **Send Preview**: Sends a preview of the notification to you on your Meta Horizon mobile app and in your headset.
    * **Duplicate**: Allows you to make a copy of your notification.
    * **View Request ID** (Event-based only): Shows the request ID needed to be implemented in your app code to allow the notification to function.
    * **Edit**: Allows you to edit the notification. Edit is only available when the notification status is Rejected or Draft state.
    * **View Details**: Allows you to view the notification that is in Pending state.
    * **Cancel**: Blocks this notification from going out to users. This cannot be undone.

**Note**: Follower notifications are limited to a maximum of 1 notification sent per day from your organization. Follower notifications are sent deterministically to all followers and appear in the notification feed only.

## Text Guidelines

Follow these guidelines to create a great experience and help your notification's chance of getting approved.

### Clarity

#### Do

Be clear, specific, relevant and timely. Put names and key information first and any calls to action at the end.

> _The Town Tournament begins today. Join in!_

Set expectations for what happens when the user taps.

> _Did you miss the town hall? Catch a virtual replay._

#### Don't

Use jargon or outdated information. Put names or key information at the end.

> _A tournament happened last week._

Be ambiguous or misleading when setting expectations.

> _Did you miss the town hall?_

### Emoji

Be thoughtful about an emoji's possible interpretations and cultural implications in the context of your text. Note that an emoji may be counted in the system as more than 1 character.

#### Do

Use a single emoji that aligns with the tone and theme of your message. Generally, an emoji is best placed at the end of the message.

> _You received some tokens_  🎉

Choose an emoji that is available for use in notifications:

| Category | Available emojis |
| -- | -- |
| Faces, clothing | 😃 😄 😎 🥳 🤯 👽 🎃 👟 👑 |
| Plants, animals | 🐶 🐱 🐭 🐹 🐰 🦊  🐼 🐯 🦁 🐒  🐳 🐠 🌲 🌴 🌿 🍀 🍁 🌺 🌙 🪐 ⭐️ ✨ 💥 ☀️❄️ |
| Food, activities | 🍎 🍿 ⚽️ 🏀 🏈 ⚾️ 🎾 🏐 ⛳️ 🏹 🥊 🏆 🥇 🥈 🥉 🏅 🎟 🎸 🎲 |
| Travel, places | 🚗 ✈️ 🚀 ⛱ ⛺️ |
| Objects, symbols | 💿 💎 🔮 🔑 🎁 🎉 🔓 |

#### Don't

Don't use more than one emoji, because they increase cognitive load.

> _You received some tokens_ 🏅🚀🎉

### Grammar

#### Do

Use correct punctuation and sentence case, capitalizing only the first word and proper nouns:

> _You received some tokens._

#### Don't

Don't use title case, all caps (except in official names), or multiple punctuation marks:

> _You received SOME TOKENS!!_

### Profanity

Avoid expletives and other vulgar language.

See the Meta Quest [Content Guidelines](/policy/content-guidelines/) for content that we don't allow on our platform.
# Voice Sdk Intent

**Documentation Index:** Learn about voice sdk intent in this documentation.

---

---
title: "Built-in Intents"
description: "Reference the pre-trained intent categories available through Wit.ai, including media, alarms, and calendar."
---

Currently, built-in intents are only available in English.

<br/>

## Common Intents

| Intent | Meaning | Sample utterances |
|-|-|-|
| `wit$cancel` | Cancels an action or selection | “Cancel that” |
||| “I don’t want that” |
| `wit$confirmation` | Confirms or agrees with information | “That’s right” |
||| “I agree” |
| `wit$go_back` | Indicates an action or individual should go back or reverse | “Go back” |
||| “Take me back” |
| `wit$go_forward` | Indicates that an action or individual should move forward | “Go forward” |
||| “Move ahead” |
| `wit$negation` | Denies or disagrees with information | “That’s wrong” |
||| “Nope” |
| `wit$nevermind` | Discards the voice activation.  | “Nevermind” |
| `wit$open_resource` | Opens an app | “Open my app” |
| `wit$repeat_response` | The statement was unclear; please repeat the response | “I didn’t understand that” |
||| “Say that again” |
| `wit$select_item` | Indicates a selection | “The first item” |
||| “Select ‘all of the above’” |
| `wit$share` | Shares this with another individual | “Send this to George” |
||| “Share this with the group” |

<br/>

## Alarms and Timer Intents

| Intent | Meaning | Sample utterances |
|-|-|-|
| `wit$add_time_timer` | Adds time to a timer. | “Add 5 minutes to the time” |
||| “Give me 10 more minutes” |
| `wit$create_alarm` | Sets an alarm. | “Set an alarm for 7am tomorrow morning” |
||| “I want a 9:45 pm alarm” |
| `wit$create_timer` | Creates a new timer. | “Set a timer for 10 minutes” |
||| “I need a 15 minute count-down” |
| `wit$delete_alarm` | Deletes an alarm. | “Delete the alarm” |
||| “Cancel the 9am alarm” |
| `wit$delete_timer` | Deletes the timer. | “Delete the timer” |
||| “Get rid of this timer” |
| `wit$get_alarms` | Gets the alarms currently set. | “What alarms do I have?” |
||| “Do I have an alarm set?” |
| `wit$get_timer` |Asks how much time is left on a timer. |“Show me the timer” |
||| “How long do I have left?” |
| `wit$pause_timer` | Pauses the timer. | “Pause the timer” |
||| “Pause” |
| `wit$resume_timer` | Resumes the timer. | “Resume the timer” |
||| “Start it again” |
| `wit$silence_alarm` | Stops the currently set alarm. | “Stop the alarm” |
||| “Turn it off” |
| `wit$snooze_alarm` | Snoozes an alarm. | “Snooze” |
||| “Pause the alarm” |
| `wit$subtract_time_timer` | Reduces time from the timer. | “Subtract 5 minutes from the timer” |
||| “Take an hour off the timer” |

<br/>

## Calendar and Weather Intents

| Intent | Meaning | Sample utterances |
|-|-|-|
| `wit$check_weather_condition` | Asks about the current weather. | “Is it raining?” |
||| “What’s the weather like?” |
| `wit$get_date` | Returns the current date. | “What's the date?” |
||| “What day is it?” |
| `wit$get_sunrise` | Returns the time of the sunrise. | “When's sunrise?” |
||| “What time does dawn happen?” |
| `wit$get_sunset` | Returns the time of the sunset. | “When's the sunset today?” |
||| “What time does the sun go down?” |
| `wit$get_temperature` | Asks for the current temperature. | “What is the temperature today?” |
||| “How hot is it?” |
| `wit$get_time` | Asks for the current time. | “What's the time?” |
||| “What time is it?” |
| `wit$get_time_until_date` | Asks how many days till a certain date. | “How many days until Christmas?” |
||| “How long until payday?” |
| `wit$get_weather` | Asks for the current weather. | “What's the weather like today?” |
||| “What’s it like outside?” |

<br/>

## Media Intents

| Intent | Meaning | Sample utterances |
|-|-|-|
| `wit$add_to_playlist` | Adds a song to a playlist. | “Add this song to my playlist” |
||| “Play this song on my list” |
| `wit$create_playlist` | Creates a new playlist and adds the current song. | “Create a new playlist” |
||| “Add this song to a new playlist” |
| `wit$decrease_volume` | Decreases the volume of the selected media. | “Turn it down” |
||| “Lower the volume” |
| `wit$delete_playlist` | Deletes the current playlist. | “Delete this playlist” |
||| “Get rid of this list” |
| `wit$fast_forward_track` | Fast forwards in the song. | “Fast forward this song” |
||| “Skip to later in this song” |
| `wit$get_track_info` | Gets information about the current song. |“What's the name of this song?” |
||| “Who sings this?” |
| `wit$increase_volume` | Increases the volume of the selected media. | “Make the podcast louder” |
||| “Turn it up” |
| `wit$pause` | Pauses the media (song, video, podcast). | “Pause” |
| `wit$play` | Plays the media (song, video, podcast). | “Start playing” |
| `wit$play_podcast` | Plays the particular podcast. | “Play this podcast” |
||| “Start the first episode” |
| `wit$previous_track` | Replays the previous song. | “Play the last song” |
||| “Give me that one again” |
| `wit$remove_from_playlist` | Deletes a song from the playlist. | “Remove this song” |
||| “I don’t want to hear this again” |
| `wit$replay_track` | Repeats the current song. | “Replay this song” |
||| “Give me that one again” |
| `wit$resume` | Resumes the media (song, video, podcast). | “Please resume” |
| `wit$rewind_track | Rewinds the current song by 10 seconds. | “Rewind the song” |
||| “Back that up” |
| `wit$shuffle_playlist` | Plays the playlist in random order. | “Shuffle the playlist” |
||| “Play these songs randomly” |
| `wit$skip_track` | Skips the song currently playing. | “Skip this song” |
||| “Next song, please” |
| `wit$stop` | Stops the media (song, video, podcast). | “Please stop” |
| `wit$unshuffle_playlist` | Plays the playlist according to the playlist order. | “Stop the shuffle” |
||| “Play the songs in order” |

<br/>

## Music Intents

| Intent | Meaning | Sample utterances |
|-|-|-|
| `wit$dislike_music` | Lets the music service know the user doesn’t like the current song. |“I don't like this song” |
||| “This song sucks” |
| `wit$like_music` | Lets the music service know the user likes a song and it’ll get saved into the favorites. | “I like this song” |
||| “This song is great” |
| `wit$loop_music` | Plays the song repeatedly. | “Repeat this song” |
||| “Play this over again” |
| `wit$pause_music` | Pauses the song current playing. | “Pause this song” |
| `wit$play_music` | Plays music or media content. | “Play Christmas music” |
||| “Give me some blues” |
| `wit$resume_music` | Resumes a song that was paused. | “Resume the song” |
||| “Start it again” |
| `wit$stop_music` | Stops the music or media content that's currently playing. | “Stop the music” |
||| “Stop the video” |
| `wit$unloop_music` | Stops repeating a song. | “Stop the loop on this song” |
||| “Stop repeating” |
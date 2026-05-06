# Voice Sdk Entities

**Documentation Index:** Learn about voice sdk entities in this documentation.

---

---
title: "Built-In Entities"
description: "Reference the pre-trained entity types available in Voice SDK built-in natural language processing."
---

Support for built-in entities varies per language.

| Entity | Meaning | Entity Captured (in bold) |
|-|-|-|
| `wit$amount_of_money` | Captures an amount of money such as $20 or 30 euros.  | “Book a **30 Euro** train ticket.” |
||| For “I want to spend between 10 and 20 dollars.” the entity captures **$10 to $20**. |
||| For “In my pocket, I have at least 10 Euros,” the entity captures **from 10 Euros**. |
| `wit$datetime` | Captures and resolves dates and time, like tomorrow at 6pm. The API returns a grain field that indicates the granularity of the reading. For entities that are intervals, the maximum length of time will be captured. If a user says “this weekend,” for instance, the entity will capture the datetime from Friday 18:00:00 to Monday 0:00:00. For instance, the datetime might be: “from 2020-06-19T18:00:00.000-07:00 to 2020-06-22T00:00:00.000-07:00.”  | For “I was born at 9:27pm on December 4th, 1990,” the entity captures **1990-12-04T21:27:00.000-08:00** (in the form yyyy-mm-ddThh:mm:ss.000±UTC). |
||| For “I won’t eat meat between World Vegetarian Day and World Vegan Day,” the entity captures **2020-10-01T00:00:00.000-07:00 to 2020-11-02T00:00:00.000-07:00**.  |
| `wit$distance` | Captures a distance in miles or kilometers such as 5 km, 5 miles, or 12 m.  | “I ran for **5 miles**.” |
||| For “It’s around 8-10 km,” the entity captures **8 kilometers to 10 kilometers**. |
| `wit$duration` | Captures a duration such as 30 min, 2 hours or 15 seconds and normalizes the value in seconds | “I started a timer for **45 seconds**.” |
||| For “I was gone for 10 hours, six minutes, and 2 secs,” the entity captures **36362 seconds**. |
| `wit$email` | Captures an email but does not attempt to check the validity of the email address.  | “I can be reached at **contact@wit.ai**.” |
| `wit$location` | Captures free text that indicates a typical location such as the city or address. | For “Find a house in Palo Alto, CA,” the entity captures the city, coordinates, and timezone of Palo Alto. |
| `wit$phone_number` | Captures telephone numbers as presented in the utterance but does not try to check the validity of the number.  | “Please call me at **+1 (123) 456-7890**.” |
| `wit$quantity` | Captures the quantity of an item, as with the ingredients in a recipe, or the quantity of food in a health tracking app. It returns a numerical value, the applicable unit, and a product. Each field is optional. | For “How many ounces are there in two cups of flour?” the entity captures **two cups, flour**. |
||| For “How many ounces are there in five cups?” the entity captures **five cups**. |
||| For “I need at least 4 oz. of chocolate,” the entity captures **from 4 ounces, chocolate**. |
| `wit$temperature` | Captures the temperature in either degrees Celsius or Fahrenheit. | For “Set the temperature to 70° F,” this entity captures **70° Fahrenheit**. |
||| For “The current temperature is more than 24° Celsius,” this entity captures **from 24° Celsius**. |
| `wit$url` | Captures a URL, but does not try to check the validity of the link. Any standard link format will be captured, such as: http://www.foo.com, www.foo.com:8080/path, https://myserver?foo=bla, foo.com/info, bla.com/path/path?ext=%23&foo=bla, or localhost. | “Go to **www.wit.ai**.” |
| `wit$volume` | Captures measures of volume in an utterance, such as 250 ml, 3 gal. | “Please fill up the **five-gallon** bucket.”  |
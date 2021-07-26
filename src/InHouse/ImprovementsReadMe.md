## Improvements on V1 InHouse API

Tell us what you think about the data format. Is the current JSON structure the
best way to store that kind of data or can you come up with a better version? There
are no right answers here  Please write your thoughts to readme.md.

I think the Rest standard way to do this to bring the Key part into value,
let the key reamians as Contract

Lets take this
```
{
  "monday": [
  ],
  "tuesday": [
    {
      "type": "open",
      "value": 36000
    },
    {
      "type": "close",
      "value": 64800
    }
  ],
}
```

here monday is the Domain Value for the the Data , this means that we 
need repeat the Model for all 7 days, I know there is only 7 days possible.
But to make our model cleaner we could do something like this
```
{
    workingDaySchedules:[
     {
        "DayOfWeek": "Monday",
        "Schedules":[
        ]
     },
    {
        "DayOfWeek": "Tuesday",
        "Schedules":[
            {
              "type": "open",
              "value": 36000
            },
            {
              "type": "close",
              "value": 64800
            }
        ]
     },   
    ]
}
```

So what I am doing I am using AutoMapper to convert V1 payload to above payload internally
then process events, my API Contract becomes

from this 
```
public class RestaurantListingWorkingHoursRequest
    {
        [JsonProperty("monday")]
        public List<WorkingDay> Monday { get; set; }

        [JsonProperty("tuesday")]
        public List<WorkingDay> Tuesday { get; set; }

        ...
    }

    public class WorkingDay
    {
        [JsonProperty("type")]
        public WorkingDayType Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

```

to

```
    public class RestaurantWorkingHoursPayload
    {

        public RestaurantWorkingHoursPayload()
        {
            WorkingDaySchedules = new List<WorkingDaySchedule>();
        }
        public List<WorkingDaySchedule> WorkingDaySchedules { get; set; }
    }

    public class WorkingDaySchedule
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<WorkingDay> Schedules { get; set; }
    }

```


As part of V2, I can take RestaurantWorkingHoursPayload directly and call my service
and returns the same response on the V2 endpoint , but which means it supports both version.
I didb't impleement it now. its straight forward it passing V2 payload to V2 controller
and call the service directly it doesn't need the mapper in this case


Let me know I can add this in V2 as new endpoint after submitting the Assignment

we can adapt FeatureToogle to make this change and deploy V2


	
# GreatGalaxy
The amazing great galaxy delivery solution

# Introduction
Our team of talented software engineers (well software engineer) started of planning out the system, and decided to go for an event driven design, with a REST Api, and messaging to handle events.

LiteDB was chosen as the database, because it is a NonSql embedded database, that would not require any installation to run. MassTransit in memory is used for messaging again to avoid any complications with running the project.

While the delivery team had the best of intentions, a number of shortcuts had to be taken in the interest of time.

What is missing you might ask, well unfortunately a lot, in fact it would be a miracle if this actually works (I should probably not admit that here) 
-	The system is not very robust, for example there are no retry policies in place and not a lot of defensive coding
-	Any form of logging
-	Security
-	Error handling (especially for the REST Api and Events)
-	Talking about the Api, the design needs to be revisited
-	Separation of concerns, the services should not share a db, and should be communicating via api’s and messages

### Assumptions
-	Everyone speaks English
-	Times can be expressed in UTC
-	All addresses can be broken up into 
    -	Address Line 1
    -	Address Line 2
    -	Address Line 3
    -	GPS Coordinates
    -	Celestial body
    -	A string indicating the position of the celestial  body
-	Planning, who need planning your package will get picked up when we are ready, and it will get there when it gets there ;)
-	The logic to determine the availability of drivers and vehicles is outside the scope of this project
-	Once a Delivery has been created, the driver and vehicle will not be changed. This is very unrealistic in a real world scenario
-	Managing the cargo is outside the scope of this project

### Testing
As for testing, you trust us right ;) Okay that was worth a try.

There is a project under the src directory named `GreatGalaxy.slnx`

To set up some test data you can run `TripReportFullTest` > `DoIt` under the `GreatGalaxy.Integration.Tests`. This test will create everything required for a delivery, fire off a couple of events and produce a trip report. The example report will be output to the test output.

The directory `c:\Temp` must exist for the db file to be created

There are 3 web projects, each of which have swagger enabled, so you can add /swagger to the end of the url to see the Api definition, and try it out:
-	`GreatGalaxy.Administration`
    -	This service can be used to create drivers and vehicles
-	`GreatGalaxy.Dispatch`
    - Used to set up routes and deliveries
    - Once a delivery has been created, all updates will happen via events. A endpoint has been created to make it easier to submit events (`/deliveries/{deliveryId}/events`), which will fire off a Mass transit message
        - Event Types:
            - VehicleDeparture = 1,
            - CheckpointReached = 2,
            - DestinationReached = 3,
            - Information = 4,
            - Disruption = 5,
            - Disaster = 6,
            - CompleteAndUtterFailure = 7
-	`GreatGalaxy.Reporting`
    - Generates the trip report
    - The report is currently generated at runtime, but it might be better to generate and save the report once the trip has been completed. This could be triggered by a message.

There are integration tests for all of the Api's, but unit tests are sadly a bit sparse, there are a couple of examples under `GreatGalaxy.Dispatch.Test.Unit`.

Thank you for the oppertunity to present you with my code :)


Example report output (from integration test): 

 ```{
  "Id": 22,
  "Route": {
    "Id": 21,
    "Origen": {
      "Id": 1620276216,
      "Line1": "123 Main St",
      "Line2": "Apt 4B",
      "Line3": "Zork",
      "CelestialBody": "Mars",
      "Latitude": 40.7128,
      "Longitude": -74.006,
      "CelestialBodyPositionInSpace": "0,0,0"
    },
    "Checkpoints": [
      {
        "Location": {
          "Id": 1385588778,
          "Line1": "Martian imigration center",
          "Line2": null,
          "Line3": null,
          "CelestialBody": "Phobos",
          "Latitude": 39.9526,
          "Longitude": -75.1652,
          "CelestialBodyPositionInSpace": "0,0,0"
        },
        "Arrived": "2026-02-16T07:50:27.378+13:00",
        "Events": [
          {
            "EventId": "7b368694-f20f-425a-a62d-f1fd8cc322bf",
            "EventType": "CheckpointReached",
            "Timestamp": "2026-02-16T07:50:27.378+13:00",
            "Duration": "00:00:01",
            "RelatedCheckpoint": 1385588778,
            "Description": "Got to the first checkpoint with no issues"
          }
        ]
      },
      {
        "Location": {
          "Id": 813126875,
          "Line1": "Way Station 1",
          "Line2": null,
          "Line3": null,
          "CelestialBody": "Moon",
          "Latitude": 39.9526,
          "Longitude": -75.1652,
          "CelestialBodyPositionInSpace": "0,0,0"
        },
        "Arrived": "2026-02-16T07:50:27.388+13:00",
        "Events": [
          {
            "EventId": "2006c56c-f153-4bf1-9ace-68a08e41ecc9",
            "EventType": "CheckpointReached",
            "Timestamp": "2026-02-16T07:50:27.388+13:00",
            "Duration": "00:00:01",
            "RelatedCheckpoint": 813126875,
            "Description": "Finally reached the moon, will need to spend some time on repairs"
          }
        ]
      }
    ],
    "Destination": {
      "Id": 431066508,
      "Line1": "Level/260 Oteha Valley Road",
      "Line2": "Albany",
      "Line3": "Auckland 0632",
      "CelestialBody": "Earth",
      "Latitude": -36.722418671875594,
      "Longitude": 174.70709234000125,
      "CelestialBodyPositionInSpace": "0,0,0"
    }
  },
  "Departed": "2026-02-16T07:50:27.178+13:00",
  "Arrived": "2026-02-16T07:50:27.389+13:00",
  "Driver": {
    "Id": 19,
    "Name": "Zyrrik Fluxrunner"
  },
  "Vehicle": {
    "Id": 20,
    "Type": {
      "Make": null,
      "Model": null
    },
    "Description": "Top of the fleet"
  },
  "Events": [
    {
      "EventId": "23468308-442a-4475-8794-8a5c2e43b9c0",
      "EventType": "VehicleDeparture",
      "Timestamp": "2026-02-16T07:50:27.178+13:00",
      "Duration": "00:00:01",
      "RelatedCheckpoint": null,
      "Description": "And we are off on a great adventure!"
    },
    {
      "EventId": "7b368694-f20f-425a-a62d-f1fd8cc322bf",
      "EventType": "CheckpointReached",
      "Timestamp": "2026-02-16T07:50:27.378+13:00",
      "Duration": "00:00:01",
      "RelatedCheckpoint": 1385588778,
      "Description": "Got to the first checkpoint with no issues"
    },
    {
      "EventId": "1bd4c276-981f-4932-aa6d-fe96dffdb5ac",
      "EventType": "Disaster",
      "Timestamp": "2026-02-16T07:50:27.384+13:00",
      "Duration": "00:00:01",
      "RelatedCheckpoint": null,
      "Description": "Space priates! They got half of the cargo, but we are back on route."
    },
    {
      "EventId": "2006c56c-f153-4bf1-9ace-68a08e41ecc9",
      "EventType": "CheckpointReached",
      "Timestamp": "2026-02-16T07:50:27.388+13:00",
      "Duration": "00:00:01",
      "RelatedCheckpoint": 813126875,
      "Description": "Finally reached the moon, will need to spend some time on repairs"
    },
    {
      "EventId": "d606f33b-8a6b-4060-8a21-8a2157635f34",
      "EventType": "DestinationReached",
      "Timestamp": "2026-02-16T07:50:27.389+13:00",
      "Duration": "00:00:01",
      "RelatedCheckpoint": null,
      "Description": "Mission successfull, well sort of..."
    }
  ],
  "Status": 3
}
```
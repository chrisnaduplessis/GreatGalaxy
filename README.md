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

To set up some test data you can run `TripReportFullTest` > `DoIt` under the `GreatGalaxy.Integration.Tests`. This test will create everything required for a delivery, fire off a couple of events and produce a trip report.

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

There are integration tests for all of the Api's, but unit tests are sadly a bit sparse, there are a couple of examples under `GreatGalaxy.Dispatch.Test.Unit`.

Thank you for the oppertunity to present you with my code :)

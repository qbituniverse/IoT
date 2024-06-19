# Traffic Lights

## TrafficLights.Domain

TBC

## TrafficLights.Api

### Configuration

|Key|Value|Description|
|-----|-----|-----|
|Environment|Development|Dev machine, no Pi|
||Test|Pi in test mode|
||Production|Pi in production mode|
|Repository.Type|Default|Mock, hard-coded in-memory data, no persistent storage|
||MySql|SqLite database backend<br>_NOTE: Works on AMD and ARM architectures_|
||MongoDb|MongoDb database backend<br>_NOTE: Not available for ARM architecture_|
|Repository.MySql.Url|Connection string|Connection details to MySql|
|Repository.MongoDb.Url|Connection string|Connection details to MongoDb|
|TrafficControl.Type|Default|Mock, hard-coded Traffic Control, not requiring Pi|
||Pi|Traffic Control running on Pi GPIO|

### Ports

|Machine|Environment|Port|
|-----|-----|-----|
|PC|Development|5010|
||Test|5011|
||Production|5012|
|Docker|Development|8010|
||Test|8011|
||Production|8012|

## TrafficLights.Console

### Configuration

|Key|Value|Description|
|-----|-----|-----|
|Environment|Development|Dev machine, no Pi|
||Test|Pi in test mode|
||Production|Pi in production mode|
|Repository.Type|Default|Mock, hard-coded in-memory data, no persistent storage|
||MySql|SqLite database backend<br>_NOTE: Works on AMD and ARM architectures_|
||MongoDb|MongoDb database backend<br>_NOTE: Not available for ARM architecture_|
|Repository.MySql.Url|Connection string|Connection details to MySql|
|Repository.MongoDb.Url|Connection string|Connection details to MongoDb|
|TrafficControl.Type|Default|Mock, hard-coded Traffic Control, not requiring Pi|
||Pi|Traffic Control running on Pi GPIO|
||Api|Traffic Control running through Traffic Lights Api|
|TrafficControl.Api.Url|Connection string|Url to the Traffic Lights Api<br>_NOTE: Used when TrafficControl.Type = Api_|
|TrafficSensor.Type|Default|Mock, hard-coded Traffic Sensor, not requiring Pi|
||Pi|Traffic Sensor running on Pi GPIO|
|Run|RunDefault|Run command prompt to test if it works|
||RunTrafficTimer|Run traffic lights alternate at a predefined time intervals|
||RunTrafficSensor|Use infrared sensor to control the traffic lights|

## TrafficLights.Web

### Configuration

|Key|Value|Description|
|-----|-----|-----|
|Environment|Development|Dev machine, no Pi|
||Test|Pi in test mode|
||Production|Pi in production mode|
|Repository.Type|Default|Mock, hard-coded in-memory data, no persistent storage|
||MySql|SqLite database backend<br>_NOTE: Works on AMD and ARM architectures_|
||MongoDb|MongoDb database backend<br>_NOTE: Not available for ARM architecture_|
|Repository.MySql.Url|Connection string|Connection details to MySql|
|Repository.MongoDb.Url|Connection string|Connection details to MongoDb|
|Api|Connection string|Url to the Traffic Lights Api|

### Ports

|Machine|Environment|Port|
|-----|-----|-----|
|PC|Development|5020|
||Test|5021|
||Production|5022|
|Docker|Development|8020|
||Test|8021|
||Production|8022|



# Misc

```json
"Serilog": {
  "WriteTo": [
    {
      "Name": "MongoDBBson",
      "Args": {
        "databaseUrl": "",
        "collectionName": "ApiLogs",
        "cappedMaxSizeMb": "1024",
        "cappedMaxDocuments": "50000",
        "rollingInterval": "Day",
        "restrictedToMinimumLevel": "Warning"
      }
    },
    {
      "Name": "Console",
      "Args": {
        "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
        "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
        "restrictedToMinimumLevel": "Information"
      }
    }
  ]
}
```
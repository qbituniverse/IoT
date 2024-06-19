namespace TrafficLights.Web.Models;

public class WebConfiguration
{
    public string? Environment { get; set; }

    public Repository? Repository { get; set; }

    public string? Api { get; set; }
}

public class Repository
{
    public string? Type { get; set; }

    public MySql? MySql { get; set; }

    public MongoDb? MongoDb { get; set; }
}

public class MySql
{
    public string? Url { get; set; }
}

public class MongoDb
{
    public string? Url { get; set; }
}
# Great.Configs
A Simple, Precise, and Pretty way to manage application configuration values.

## Rationale
Default .NET configuration files and settings files leave a lot to be desired. Most importantly, they are incredibly verbose, and ugly.

### app.config is weak and lame
The standard way to add configuration to a .NET application is to add keys and values to appSettings, as shown below
```xml
<?xml version="1.0" encoding="utf-8" ?>  
<configuration>  
  ...
  <appSettings>  
    <add key="EnableTestMode" value="true"/>  
    <add key="WorkerThreads" value="10"/>  
  </appSettings>  
</configuration>  
```
This sucks for two reasons:
* It's limited to just keys and values... anything more complex requires additional parsing
* It only stores strings! So, if I want WorkerThreads, I'll have to Int.Parse() the value every time, or write a class to wrap it

### Settings File is really cool and useful, but the XML it uses is UGLY
Adding a Settings file to a solution is cool, and really easy. It does almost everything I'd ever want it to, but has one major flaw: *The XML is atrocious!*. Just look:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <sectionGroup name="applicationSettings" type="System.Configuration.ApplicationSettingsGroup, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
      <section name="MySettings" type="System.Configuration.ClientSettingsSection, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
    </sectionGroup>
  </configSections>
  <applicationSettings>
    <MySettings>
      <setting name="EnableTestMode" serializeAs="Bool">
        <value>True</value>
      </setting>
      <setting name="WorkerThreads" serializeAs="Int">
        <value>10</value>
      </setting>
    </MySettings>
  </applicationSettings>
</configuration>
```
This sucks for another two reasons:
* It pollutes the app.config configSections node... eww
* The setting and value serialization is gross - why should my config file have so much text in it, including "serializeAs" attributes and "value" nodes?

### Great.Configs is cooler :)
The default output for a Great.Config file, representing the same data:
```xml
<?xml version="1.0" encoding="utf-8"?>
<TestConfig>
  <EnableTestMode>true</EnableTestMode>
  <WorkerThreads>400</WorkerThreads>
</TestConfig>
```

## Great.Configs to the rescue!
This library serves as a simple, easy way to persist configuration values in an easy to read, easy to edit XML file. Thats all it does. But it does it! 
Quick start example:
```csharp
using Great.Configs;

public class Program
{
    TestConfig Config = ConfigManager.LoadConfig<TestConfig>("TestConfig.xml");

    static void Main()
    {
        if(Config.EnableTestMode){
            // do something here! Whatever it is, its not worrying about your config!
        }
    }
}
```


### 1. Install Great.Configs via Nuget
Install via NuGet
```
Install-Package Great.Configs
```

### 2. Create a POCO class to represent your configuration
Be sure to extend 'ConfigFile'! Implement your values as properties (with {get;set;}) and include any defaults...
```csharp
public class TestConfig : XmlConfigFile
{
    public bool EnableTestMode { get; set; } = false;
    public int WorkerThreads { get; set; } = 10;
}
```

### 3. Load your config!
```csharp
using Great.Configs;

public class Program
{
    static void Main()
    {
        // no parameters means we just get the default class with default values
        TestConfig Config = ConfigManager.LoadConfig<TestConfig>();

        // pass a file path to load from an XML file. By default, exceptions will be 
        // swallowed and the returned config object will have .IsLoaded set to false
        // and .LoadException set to the exception
        TestConfig Config = ConfigManager.LoadConfig<TestConfig>("TestConfig.xml");

        // to allow throwing of exceptions, pass throwExceptions as true
        try {
            TestConfig Config = ConfigManager.LoadConfig<TestConfig>("TestConfig.xml", true);
        } catch(Exception ex) {
            // exception will be available here!
        }
    }
}
```

### 4. USE your config!
```csharp
// to read a config value
Config.EnableTestMode; // bool!
Config.WorkerThreads; // int!
```

## Change Log

### v1.0.0
* Initial Version... basic functionality. Submit a PR or issue if you find anything :)
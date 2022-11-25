# gcplength-dotnet

## Introduction

This project is a .NET Class Library implemention of GS1 Company Prefix Length detection, by using Codes like GCP, GTIN, etc.

## Namespace

```benjaminshi.gs1```

## Class

```C#
class GCPLength
{
    public static bool Download();

    public static bool Refresh();

    public static bool Exists(string prefix);

    public static int Find(string prefix);

    public static int Find(string prefix, out string realPrefix);    
}
```

### Method ```GCPLength.Download()```

This method is used to re-download the GCP Length file from GS1 Global Website (www.gs1.org).

#### Usage

```GCPLength.Download();```

#### Parameters

None

## Windows Test App

[winApp](https://github.com/benjamin-shi/gcplength-dotnet/tree/master/winApp)

## ASP.Net Web API Project

[gcplength-saas-dotnet](https://github.com/benjamin-shi/gcplength-dotnet/tree/master/gcplength-saas-dotnet)

## Contacts

Yu Shi (Benjamin)
**Email:** shiyubnu@gmail.com

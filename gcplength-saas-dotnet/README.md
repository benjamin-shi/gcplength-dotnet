# gcplength-saas-dotnet

## Introduction

This project is a ASP.NET Core Web API Docker Image implemention of GS1 Company Prefix Length detection, by using Codes like GCP, GTIN, etc.

## Dependence

[gcplength-dotnet](https://github.com/benjamin-shi/gcplength-dotnet/tree/master/gcplength-dotnet)

## api/gclength

### a) Http Get

#### Usage

> https://server/api/gcplength?code=\<code\>

#### Return

```JSON
{
    "isOK" : true|false,
    "status" : "OK"|"Error",
    "message" : "\<error message\>",
    "errors" : null | ["errors"],
    "data" : {
        "GCP" : "<Actual Code in the GCP Length Table>",
        "Length" : <GCP Length>
    } | null
}
```

### b) Http Post

#### Usage

> https://server/api/gcplength?code

#### POST Body

```JSON
{
    "code" : "<code>"
}
```

#### Return

```JSON
{
    "isOK" : true|false,
    "status" : "OK"|"Error",
    "message" : "\<error message\>",
    "errors" : null | ["errors"],
    "data" : {
        "GCP" : "<Actual Code in the GCP Length Table>",
        "Length" : <GCP Length>
    } | null
}
```

## Contacts

Yu Shi (Benjamin)
**Email:** shiyubnu@gmail.com

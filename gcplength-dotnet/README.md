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

### Method ```Download()```

This method is used to re-download the GCP Length file from GS1 Global Website (www.gs1.org).

#### Usage

```GCPLength.Download();```

#### Parameters

N/A

#### Return

```bool```, true if successed, or false if failed.

### Method ```Refresh()```

Refresh/reload the data from the GCP Length Table (JSON Version).

#### Usage

```GCPLength.Refresh();```

#### Parameters

N/A

#### Return

```bool```, true if successed, or false if failed.

### Method ```Exists(string prefix)```

Check whether a prefix existed in the GCP Length Table

#### Usage

```GCPLength.Exists(string prefix);```

#### Parameters

Name     | Type     | Note
---------|----------|---------
 prefix  | string   | The code used to be detected

#### Return

```bool```, whether the "prefix" can be found in the GCP Length Table.

### Method ```Find(string prefix)```

GS1 Company Prefix Length detection

#### Usage

```GCPLength.Find(string prefix);```

#### Parameters

Name     | Type     | Note
---------|----------|---------
 prefix  | string   | The code used to be detected

#### Return

```int```, GS1 Company Prefix Length, or 0 if cannot find a GCP Length based on the "prefix".

### Method ```Find(string prefix, out string realPrefix)```

GS1 Company Prefix Length detection

#### Usage

```GCPLength.Find(string prefix, out string realPrefix);```

#### Parameters

Name         | Type     | Note
-------------|----------|---------
 prefix      | string   | The code used to be detected
 realPrefix  | string   | Output actual prefix when found, or "" if cannot find a GCP Length based on the "prefix"

#### Return

```int```, GS1 Company Prefix Length, or 0 if cannot find a GCP Length based on the "prefix".

## Contacts

Yu Shi (Benjamin)
**Email:** shiyubnu@gmail.com

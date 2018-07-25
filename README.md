# UtilitySharp

A jack of all trades and master of none. UtilitySharp provides helper classes for things such as strings, and numbers so you don’t have to reinvent the wheel.
It currently only has UtilitySharp.StringHelper.CleanAndConvert() which cleans a string and returns the desired type.

UtilitySharp is on NuGet and can be found [here](https://www.nuget.org/packages/UtilitySharp/).

## Installing

Type the following into the NuGet Package Manager:

```
Install-Package UtilitySharp
```

## Usage

Some examples of its usage.

### Basic

```
int n = UtilitySharp.StringHelper.CleanAndConvert<int>("My number is 5."); // Output: 5
```

### Regex & DateTime

Note: If you need help with Regex, check out [regexr](https://regexr.com/).

```
DateTime dt = UtilitySharp.StringHelper.CleanAndConvert<DateTime>("The date is 24/07/2018 01:26!", "([0-9]{2}/[0-9]{2}/[0-9]{4}\\s[0-9]{2}:[0-9]{2})", UtilitySharp.Enums.RegexAction.Select, "dd/MM/yyyy HH:mm"); // Output: 24/07/2018 01:26:00 AM
```
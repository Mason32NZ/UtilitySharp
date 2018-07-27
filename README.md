# UtilitySharp

A jack of all trades and master of none. UtilitySharp provides helper classes for things such as strings, and numbers so you don’t have to reinvent the wheel.

UtilitySharp is on NuGet and can be found [here](https://www.nuget.org/packages/UtilitySharp/).

## Installing

Type the following into the NuGet Package Manager:

```
Install-Package UtilitySharp
```

## Usage

Some examples of its usage.

### StringHelper.CleanAndConvert()

```
int n = StringHelper.CleanAndConvert<int>("My number is 5."); // Output: 5
DateTime dt = StringHelper.CleanAndConvert<DateTime>("The date is 24/07/2018 01:26!", "dd/MM/yyyy HH:mm"); // Output: 24/07/2018 01:26:00 AM
```

More examples of other methods to come!

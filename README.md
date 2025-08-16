# Dataverse Functions As Custom APIs
A set of [Dataverse Functions](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/functions?view=dataverse-latest) wrapped as Custom APIs, the main purpose is to call the functions inside a Power Automate Cloud flow.

Available functions:
- [CalculateRollupField](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/calculaterollupfield?view=dataverse-latest)
- [FormatAddress](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/formataddress?view=dataverse-latest)
- [GetAllTimeZonesWithDisplayName](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/getalltimezoneswithdisplayname?view=dataverse-latest)
- [GetTimeZoneCodeByLocalizedName](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/gettimezonecodebylocalizedname?view=dataverse-latest)
- [LocalTimeFromUtcTime](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/localtimefromutctime?view=dataverse-latest)
- [RetrieveAvailableLanguages](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/retrieveavailablelanguages?view=dataverse-latest)
- [RetrieveCurrentOrganization](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/retrievecurrentorganization?view=dataverse-latest)
- [RetrieveExchangeRate](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/retrieveexchangerate?view=dataverse-latest)
- [RetrieveTotalRecordCount](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/retrievetotalrecordcount?view=dataverse-latest)
- [RetrieveVersion](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/retrieveversion?view=dataverse-latest)
- [WhoAmI](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/whoami?view=dataverse-latest)

This repository it is also a good exercise on how to handle Custom APIs types and call Dataverse Functions.

Notes:
- The RetrieveAvailableLanguages returns a String array instead of an Int array (as the Int array type is not available as Custom API type)
- the GetTimeZoneCodeByLocalizedName requires a parameter called "LocalizedStandardName", the string refers to a notation used by SQL Server, you can find a list of values [here](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-configure-time-zone-2019?view=sql-server-ver17#sql-server-2019-cu-20-and-later-versions) ("Windows time zone" column)
- Enum types have been mapped as Int (for example RetrieveCurrentOrganization Function requires an enum input of type [EndpointAccessType](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/endpointaccesstype?view=dataverse-latest))
- Some Dataverse Functions return a complex type, in this case the values are returned as JSON (for example "Endpoints" of RetrieveCurrentOrganization and EntityRecordCountCollection of RetrieveTotalRecordCount
- The Dataverse Function RetrieveTotalRecordCount is bugged, has been added because it is a good example of Custom API types

# Conference Api

This is a sample api that demonstrates an effective solution to storing and retreiving conference times, which need to consider timezones. It addresses some of the issues described in this [this blog post](https://codeblog.jonskeet.uk/2019/03/27/storing-utc-is-not-a-silver-bullet), which explains why simply storing DateTimes as UTC or even as a DateTimeOffset and then converting to local time as required can have problems for future dates, such as a conference date, because timezone offsets can be revised. Instead, it's safer to store the original local time along with its timezone and convert to UTC on the fly using the latest timezone definitions.

## Prerequisites

This api uses CosmosDB as a data store and so requires the CosmosDB emulator running locally. For instructions on setting up and running the emulator see [here](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator).


## Running the api

```sh
dotnet run
```

## Testing the api

There is an http test file `api-test.http` included in the root of the project which can be used with the Visual Studio Code [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) to test the api.

# Personal Site

.NET Core web application using Contentful to set up a personal website

## Getting Started

You will need a [Contentful](https://www.contentful.com/) account and have configured api/management keys.

To run the web project or the utility project you will need to create an `appsettings.local.json` file in the root of the project with the following structure:

```json
{
  "ContentfulOptions": {
    "DeliveryApiKey": "YOUR_DELIVERY_API_KEY",
    "PreviewApiKey": "YOUR_PREVIEW_API_KEY",
    "Environment": "environment-you-want-to-use",
    "ManagementApiKey": "YOUR_MANAGEMENT_API_KEY",
    "SpaceId": "YOUR_SPACE_ID"
  }
}
```
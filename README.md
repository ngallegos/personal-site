# Personal Site

.NET Core web application using Contentful and Tailwind to set up a personal website


## Getting Started

You will need a [Contentful](https://www.contentful.com/) account (free or paid) 
and have configured api/management keys.

The utility project will first need to be run to create the required content types in Contentful.

To run the web project or the utility project you will need to create an `appsettings.local.json` 
file in the root of the project with the following structure:

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

## Shout Outs

The resume is heavily influenced by [this excellent template](https://github.com/Thomashighbaugh/resume) 
by Thomas Highbaugh - thank you for sharing your work!
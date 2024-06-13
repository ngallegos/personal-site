# Personal Site

Static React web application using Contentful and Tailwind to set up a personal website hosted on AWS 


## Getting Started

In addition to the [Contentful setup](../README.md#getting-started) you'll need Node 20 installed.

### React Project
In the `app` folder, run

```bash
npm install
```
Add a `.env` file to the root of the `app` folder with the following content: 
```.env
REACT_APP_API_GATEWAY_URL="https://your-api-gateway-url"
REACT_APP_CONTENT_DOMAIN="your-domain-configured-in-contentful.com"
```

### AWS Lambda/Api Gateway Backend
As of now, the lambda/api gateway is set up manually (IaC setup in development). There are plenty of resources to help you, but I generally set things up by starting with
[this blog post](https://aws.amazon.com/blogs/developer/net-lambda-annotations-framework/) and the [lambda annotations github repo](https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.Annotations/README.md)

The lambda code is in the `/dotnet/PersonalSite.AwsLambda` project, as it shares the contentful setup with the dotnet site.

To create a zip file to upload to lambda, make sure the dotnet lambda tools are installed and from the `/dotnet/PersonalSite.AwsLambda` directory, run:

```bash
dotnet lambda package -farch arm64
```
This builds the lambda function for the arm64 architecture. If you use x86 in your lambda setup, then you can omit the architecture flag.

Last note - the lambda functions are written to work with api gateway setup to proxy the request to the lambda function, so when you set up your api resources, make sure the proxy option is selected.

## Infrastructure
Refer to the [react terraform instructions](../infrastructure/react-s3/README.md) for infrastructure setup
# Terraform Setup
Right now this only sets up the client side of things. You'll get a full environment, but will still need to set up the backend and upload your static build files.  These are placed in the `build` folder after running:

```bash
npm run build
```

To set up the S3 and Cloudfront with your domain, you'll need to have created a hosted zone in Route53, set up your aws cli profile and installed terraform.

Create a `terraform.tfvars` file in the root of the `/infrastructure/react-s3` directory with the following content:

```terraform
region                = "your-region-of-choice"
env                   = "env-name"
site_name             = "aws-cli-profile-name"
hosted_zone_id        = "route53-hosted-zone-id"
```

Run the following commands:

```bash
terraform init # set up
terraform plan # see what will change
terraform apply # actually apply changes
```
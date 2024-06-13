provider "aws" {
  region  = var.region
  profile = var.site_name
  default_tags {
    tags = {
      SiteName    = var.site_name
      Environment = var.env
      CreatedBy   = "terraform"
    }
  }
}

provider "aws" {
    alias  = "us_east_1"
    region = "us-east-1"
    profile = var.site_name
    default_tags {
    tags = {
      SiteName    = var.site_name
      Environment = var.env
      CreatedBy   = "terraform"
    }
  }
}

module "cert" {
  source = "./_modules/cert"
  hosted_zone_id = var.hosted_zone_id
  providers = {
    aws.us_east_1 = aws.us_east_1
  }
}

module "s3" {
  source = "./_modules/s3"
  env    = var.env
  site_name = var.site_name
}

module "cloudfront" {
  source = "./_modules/cloudfront"
  hosted_zone_id = var.hosted_zone_id
  site_name = var.site_name
  main_cert_arn = module.cert.main_cert_arn
  s3_bucket = module.s3.static_site_bucket
  s3_bucket_arn = module.s3.static_site_bucket_arn
  s3_bucket_website_domain = module.s3.static_site_bucket_domain_name
  depends_on = [module.s3, module.cert]
}

module "dns" {
  source = "./_modules/dns"
  hosted_zone_id = var.hosted_zone_id
  cloudfront_url = module.cloudfront.static_site_cloudfront_url
  cloudfront_zone_id = module.cloudfront.static_site_cloudfront_zone_id
  depends_on = [module.cloudfront]
  providers = {
      aws.us_east_1 = aws.us_east_1
  }
}
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

module "cert" {
  source = "./_modules/cert"
  hosted_zone_id = var.hosted_zone_id
}

module "s3" {
  source = "./_modules/s3"
  env    = var.env
  site_name = var.site_name
}

module "cloudfront" {
  source = "./_modules/cloudfront"
  hosted_zone_id = var.hosted_zone_id
  main_cert_arn = module.cert.main_cert_arn
  s3_bucket_website_domain = module.s3.static_site_bucket_domain_name
  depends_on = [module.s3, module.cert]
}
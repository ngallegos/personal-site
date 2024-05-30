provider "aws" {
  region  = var.region
  profile = var.site_name
  default_tags {
    tags = {
      Environment = var.env
      CreatedBy   = "terraform"
    }
  }
}

module "s3" {
  source = "./_modules/s3"
  env    = var.env
  site_name = var.site_name
}
provider "aws" {
  region  = var.region
  profile = var.vpc_name
  default_tags {
    tags = {
      Environment = var.env
      CreatedBy   = "terraform"
    }
  }
}

module "vpc" {
  source                = "../_modules/vpc"
  region                = var.region
  env                   = var.env
  vpc_name              = var.vpc_name
  vpc_cidr              = var.vpc_cidr
  public_subnet_a_cidr  = var.public_subnet_a_cidr
  private_subnet_a_cidr = var.private_subnet_a_cidr
  public_subnet_b_cidr  = var.public_subnet_b_cidr
  private_subnet_b_cidr = var.private_subnet_b_cidr
}

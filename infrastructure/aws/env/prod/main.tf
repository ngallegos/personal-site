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

terraform {
  # If you do not want to store the state file in S3, comment out the following and leave it commented out 
  backend "s3" {}
  # First init setup if you want to store the state file in S3:
  # 1. comment out the above block 
  # 1. run `terraform apply` first to create the s3 bucket
  # 2. uncomment the following block
  # 3. create a backend.conf file with the following content:
  #     region         = "your-aws-region"
  #     bucket         = "your-terraform-state-bucket"
  #     key            = "states/app/terraform.tfstate"
  #     profile        = "<your-aws-cli-profile>"
  # 4. run `terraform init -migrate-state -backend-config=backend.conf`
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

module "ecs" {
  source = "../_modules/ecs"
  env    = var.env
}

module "s3" {
    source = "../_modules/s3"
    env    = var.env
    vpc_name = var.vpc_name
}

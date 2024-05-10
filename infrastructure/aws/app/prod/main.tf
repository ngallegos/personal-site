provider "aws" {
  region  = var.region
  profile = var.env
  default_tags {
    tags = {
      Environment = var.env
      CreatedBy   = "terraform"
    }
  }
}

module "codepipeline" {
  source               = "../_modules/codepipeline"
  account              = data.aws_vpc.main.owner_id
  region               = var.region
  app                  = var.app
  env                  = var.env
  vpc_id               = data.aws_vpc.main.id
  private_a            = data.aws_subnet.private_a.id
  private_b            = data.aws_subnet.private_b.id
  private_c            = data.aws_subnet.private_c.id
  repo                 = var.repo
  github_connection    = var.github_connection
  build_image_standard = var.build_image_standard
  service_id           = module.fargate.service_id
}
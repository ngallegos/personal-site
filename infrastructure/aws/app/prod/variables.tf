variable "region" {}
variable "app" {}
variable "env" {}
variable "repo" {}
variable "github_connection" {}
variable "build_image_standard" {}
variable "cpu" {}
variable "mem" {}
variable "desired_count" {}
variable "min_container_capacity" {}
variable "max_container_capacity" {}
variable "deregistration_delay" {}
variable "vpc_name" {}

data "aws_vpc" "main" {
  filter {
    name   = "tag:Name"
    values = ["${var.vpc_name}-vpc-*"]
  }
}

data "aws_subnet" "public_a" {
  filter {
    name   = "tag:Name"
    values = ["AZ A - Public"]
  }
}

data "aws_subnet" "private_a" {
  filter {
    name   = "tag:Name"
    values = ["AZ A - Private"]
  }
}

data "aws_subnet" "public_b" {
  filter {
    name   = "tag:Name"
    values = ["AZ B - Public"]
  }
}

data "aws_subnet" "private_b" {
  filter {
    name   = "tag:Name"
    values = ["AZ B - Private"]
  }
}

data "aws_subnet" "public_c" {
  filter {
    name   = "tag:Name"
    values = ["AZ C - Public"]
  }
}

data "aws_subnet" "private_c" {
  filter {
    name   = "tag:Name"
    values = ["AZ C - Private"]
  }
}


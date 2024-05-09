#create an S3 bucket for pipeline artifacts
resource "aws_s3_bucket" "pipeline_artifacts" {
  bucket = "${var.vpc_name}-pipeline-artifacts-${var.env}"
}

#create an S3 bucket for deployment files
resource "aws_s3_bucket" "deployment_files" {
  bucket = "${var.vpc_name}-deployment-files-${var.env}"
}

#create an S3 bucket for terraform state
resource "aws_s3_bucket" "terraform_state" {
  bucket = "${var.vpc_name}-terraform-state"
}

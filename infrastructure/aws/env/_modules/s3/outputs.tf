output "pipeline_artifacts_bucket" {
  value = aws_s3_bucket.pipeline_artifacts.id
}

output "deployment_files_bucket" {
  value = aws_s3_bucket.deployment_files.id
}
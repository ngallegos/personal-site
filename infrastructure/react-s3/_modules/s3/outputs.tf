output "static_site_bucket" {
  value       = aws_s3_bucket.static_site.id
}

output "static_site_bucket_arn" {
  value       = aws_s3_bucket.static_site.arn
}   

output "static_site_bucket_domain_name"{
  value       = aws_s3_bucket.static_site.bucket_domain_name
}

output "static_site_s3_website_domain" {
  value       = aws_s3_bucket_website_configuration.static_site.website_domain
}

output "static_site_s3_website_endpoint" {
  value       = aws_s3_bucket_website_configuration.static_site.website_endpoint
}

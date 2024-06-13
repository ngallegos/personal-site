variable hosted_zone_id {}
variable main_cert_arn {}
variable s3_bucket_website_domain {}
variable s3_bucket {}
variable s3_bucket_arn {}
variable site_name {}

data "aws_route53_zone" "static_site_zone" {
  zone_id = var.hosted_zone_id
}
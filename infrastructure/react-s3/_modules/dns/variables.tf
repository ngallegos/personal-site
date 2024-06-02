variable hosted_zone_id {}
variable cloudfront_url {}
variable cloudfront_zone_id {}

data "aws_route53_zone" "static_site_zone" {
  zone_id = var.hosted_zone_id
}
variable hosted_zone_id {}

data "aws_route53_zone" "static_site_zone" {
  zone_id = var.hosted_zone_id
}
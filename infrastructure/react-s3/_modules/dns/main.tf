#create dns record for cloudfront distribution
resource "aws_route53_record" "cloudfront_distribution" {
  provider = aws.us_east_1
  zone_id  = var.hosted_zone_id
  name     = data.aws_route53_zone.static_site_zone.name
  type     = "A"
  alias {
    name                   = var.cloudfront_url
    zone_id                = var.cloudfront_zone_id
    evaluate_target_health = false
  }
}
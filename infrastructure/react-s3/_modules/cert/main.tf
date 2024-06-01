#create acm certificate
resource "aws_acm_certificate" "main" {
  domain_name               = data.aws_route53_zone.static_site_zone.name
  subject_alternative_names = ["*.${data.aws_route53_zone.static_site_zone.name}"]
  validation_method         = "DNS"

  lifecycle {
    create_before_destroy = true
  }
  
  provider = aws.us_east_1
}

#create route53 records for certificate validation
resource "aws_route53_record" "main_validation" {
  for_each = {
    for dvo in aws_acm_certificate.main.domain_validation_options : dvo.domain_name => {
      name   = dvo.resource_record_name
      record = dvo.resource_record_value
      type   = dvo.resource_record_type
    }
  }
  allow_overwrite = true
  name            = each.value.name
  type            = each.value.type
  records         = [each.value.record]
  zone_id         = var.hosted_zone_id
  ttl             = "300"
}

#check certificate validation
resource "aws_acm_certificate_validation" "main" {
  provider = aws.us_east_1
  certificate_arn         = aws_acm_certificate.main.arn
  validation_record_fqdns = [for record in aws_route53_record.main_validation : record.fqdn]
}
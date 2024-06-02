

resource "aws_cloudfront_distribution" "static_site" {
    origin {
        origin_access_control_id = aws_cloudfront_origin_access_control.static_site.id
        domain_name = var.s3_bucket_website_domain
        origin_id   = var.s3_bucket_website_domain
    }

    aliases = [data.aws_route53_zone.static_site_zone.name]

    enabled = true
    default_root_object = "index.html"

    default_cache_behavior {
    allowed_methods  = ["GET", "HEAD"]
    cached_methods   = ["GET", "HEAD"]
    target_origin_id = var.s3_bucket_website_domain

    viewer_protocol_policy = "redirect-to-https"

    forwarded_values {
      query_string = true

      cookies {
        forward = "none"
      }
    }
  }
  custom_error_response {
    error_code = 403
    response_code = 200
    response_page_path = "/index.html"
  }

  viewer_certificate {
    acm_certificate_arn = var.main_cert_arn
    ssl_support_method = "sni-only"
  }


  restrictions {
    geo_restriction {
      restriction_type = "none"
    }
  }

  depends_on = [aws_cloudfront_origin_access_control.static_site]
}

resource "aws_cloudfront_origin_access_control" "static_site" {
    name = "oac-${var.site_name}"
    description = "oac for ${var.site_name}"
    origin_access_control_origin_type = "s3"
    signing_behavior = "always"
    signing_protocol = "sigv4"
}


resource "aws_s3_bucket_policy" "cloudfront_oac" {
    bucket = var.s3_bucket
    policy = data.aws_iam_policy_document.static_site_bucket_policy.json
}

# #create the static site bucket policy
data "aws_iam_policy_document" "static_site_bucket_policy" {
    statement {
        sid = "AllowCloudFrontServicePrincipalReadOnly"
        actions = [
            "s3:GetObject"
        ]
        resources = [
            "${var.s3_bucket_arn}/*"
        ]
        principals {
            type = "Service"
            identifiers = [
                "cloudfront.amazonaws.com"
            ]
        }
        condition {
            test = "StringEquals"
            variable = "AWS:SourceArn"
            values = [
                aws_cloudfront_distribution.static_site.arn
            ]
        }
    }
}
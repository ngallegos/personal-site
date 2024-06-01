resource "aws_cloudfront_distribution" "static_site" {
    origin {
        domain_name = var.s3_bucket_website_domain
        origin_id   = "S3-Origin"
    }

    enabled = true
    default_root_object = "index.html"

    default_cache_behavior {
    allowed_methods  = ["GET", "HEAD"]
    cached_methods   = ["GET", "HEAD"]
    target_origin_id = "S3-Origin"

    viewer_protocol_policy = "redirect-to-https"
    min_ttl = 0
    default_ttl = 3600
    max_ttl = 86400

    forwarded_values {
      query_string = false

      cookies {
        forward = "none"
      }
    }

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
}
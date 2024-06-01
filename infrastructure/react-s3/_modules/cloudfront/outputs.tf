output static_site_cloudfront_url {
    value = aws_cloudfront_distribution.static_site.domain_name
}
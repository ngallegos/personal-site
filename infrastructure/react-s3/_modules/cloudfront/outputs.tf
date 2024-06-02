output static_site_cloudfront_zone_id {
    value = aws_cloudfront_distribution.static_site.hosted_zone_id
}

output static_site_cloudfront_url {
    value = aws_cloudfront_distribution.static_site.domain_name
}